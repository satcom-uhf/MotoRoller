using System.IO.Ports;
static class SerialExtensions
{
    public static void SendBytes(this SerialPort port, params byte[] bytesExceptCRC)
    {
        var sum = bytesExceptCRC.Aggregate(0, (a, b) => a + b);
        var modded = sum % 256;
        var crc = 255 - modded;
        var bytes = new List<byte>(bytesExceptCRC)
        {
            (byte)crc
        }.ToArray();
        Console.WriteLine(String.Join(':', bytes.Select(x => x.ToString("X2"))));
        port.Write(bytes, 0, bytes.Length);
    }
}