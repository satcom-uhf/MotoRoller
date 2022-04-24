using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public partial class MainForm : Form
    {
        ImageStreamingServer server;
        SerialPort port;
        Task monitor;
        DisplayAdapter adapter = new DisplayAdapter();
        public MainForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (server == null)
            {
                StartButton.Enabled = false;
                server = new ImageStreamingServer(MotorolaScreen);
                server.Start(Convert.ToInt32(httpPortNumber.Value));
                port = new SerialPort(comPorts.SelectedItem.ToString(), 115200);
                port.Open();
                adapter.Updated += (s, e) => log.Invoke(new Action(() =>
                {
                    log.AppendText($"{e}\r\n");
                    MotorolaScreen.Text = adapter.Display;
                }));
                monitor = Task.Run(() =>
                  {
                      adapter.Subscribe(GetBytes());
                  });
            }
        }


        private IEnumerable<byte[]> GetBytes()
        {
            List<byte> batch = new();
            while (port.IsOpen)
            {
                var b = port.BaseStream.ReadByte();
                var byteVal = (byte)b;
                batch.Add(byteVal);
                if (b == -1) { yield break; }
                if (byteVal == 0x50)
                {
                    yield return batch.ToArray();
                    batch.Clear();
                }
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            port?.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comPorts.Items.AddRange(SerialPort.GetPortNames());
        }
    }
}
