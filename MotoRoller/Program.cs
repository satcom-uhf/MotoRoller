using MotoRoller;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
var serialPortName = app.Configuration.GetValue<string>("SerialPort");
var port = new SerialPort(serialPortName, 9600, Parity.None, 8, StopBits.One);
List<WebSocket> sockets = new();
List<byte> message = new List<byte>();
var detector = new DisplayUpdateDetector();
detector.MessageDetected += (s, e) =>
{
    _ = SendStringToSockets(string.Join("\r\n", detector.DisplayRows.Values));
};
port.DataReceived += (s, e) =>
 {
     while (port.BytesToRead > 0)
     {
         var b = port.ReadByte();
         if (b == 0x50)
         {
             if (message.Any())
             {
                 _ = SendBytesToSockets(message);
             }
             message.Clear();
             break;
         }
         //if (b)
         if (b == -1)
         {
             break;
         }
         detector.AddByte((byte)b);
         message.Add((byte)b);
     }


 };

async Task SendBytesToSockets(List<byte> bytes)
{
    try
    {
        foreach (var socket in sockets)
        {
            Console.WriteLine(String.Join(':', bytes.Select(x => x.ToString("X2"))));
            await socket.SendAsync(new ArraySegment<byte>(bytes.ToArray()), WebSocketMessageType.Binary, true, CancellationToken.None);
        }
    }
    catch (Exception ex)
    {

    }
}

async Task SendStringToSockets(string text)
{

    foreach (var socket in sockets)
    {
        try
        {
            var serverMsg = Encoding.UTF8.GetBytes("DSPL:" + text);
            await socket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }
        catch (Exception ex)
        {

        }
    }
}

try
{
    var life = app.Services.GetRequiredService<IHostApplicationLifetime>();
    life.ApplicationStopped.Register(() => port.Close());
    port.Open();
    var buttons = new ConcurrentDictionary<string, byte>();
    buttons["ptt"] = 0x00;
    buttons["right"] = 0x02;
    buttons["left"] = 0x03;
    buttons["up"] = 0x04;
    buttons["down"] = 0x05;
    buttons["ok"] = 0x06;
    buttons["exit"] = 0x07;
    buttons["p1"] = 0x08;
    buttons["p2"] = 0x09;
    buttons["p3"] = 0x0a;
    buttons["p4"] = 0x0b;
    for (int i = 0; i < 9; i++)
    {
        buttons[i.ToString()] = (byte)(0x30 + i);
    }
    buttons["f1"] = 0x3e;
    buttons["f2"] = 0x3d;
    buttons["f3"] = 0x3c;
    buttons["*"] = 0x3a;
    buttons["#"] = 0x3b;
    var clicksChannel = Channel.CreateBounded<ButtonCommand>(new BoundedChannelOptions(1) { SingleReader = true, SingleWriter = false });
    async Task ExecuteClickCommands()
    {
        try
        {
            while (!life.ApplicationStopping.IsCancellationRequested)
            {
                var command = await clicksChannel.Reader.ReadAsync();
                if (buttons.TryGetValue(command.ButtonName.ToLower(), out byte code))
                {
                    if (command.Click)
                    {
                        port.SendBytes(0xf4, 0x24, code, 0x01, 0x00);
                    }
                    else
                    {
                        port.SendBytes(0xf4, 0x24, code, 0x00, 0x00);
                    }
                    new System.Threading.ManualResetEvent(false).WaitOne(500);

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Command Error:" + ex);
        }
    }
    _ = ExecuteClickCommands();

    app.MapGet("/ws", async (HttpContext context) =>
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            sockets.Add(webSocket);
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var request = Encoding.UTF8.GetString(buffer.Take(result.Count).ToArray());
                const string press = "press/";
                const string free = "free/";
                if (request.StartsWith(press))
                {
                    await clicksChannel.Writer.WriteAsync(new ButtonCommand
                    {
                        ButtonName = request.Replace(press, ""),
                        Click = true
                    });
                }
                else if (request.StartsWith(free))
                {
                    await clicksChannel.Writer.WriteAsync(new ButtonCommand
                    {
                        ButtonName = request.Replace(free, ""),
                        Click = false
                    });
                }
                else if (request == "REFRESH")
                {
                    var serverMsg = Encoding.UTF8.GetBytes("DSPL:" + string.Join("\r\n", detector.DisplayRows.Values));
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            sockets.Remove(webSocket);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    });
    app.UseWebSockets();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.Run();
}
finally
{
    port?.Close();
}