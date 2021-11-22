using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Motorola
{
    public partial class MainForm : Form
    {
        ImageStreamingServer server;
        SerialPort port;
        Task monitor;
        string display = "UNKNOWN";
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
                port = new SerialPort(comPorts.SelectedItem.ToString());
                port.Open();
                monitor = Task.Run(() =>
                  {
                      try
                      {
                          foreach (var b in GetBytes())
                          {
                              log.Invoke(new Action(() =>
                              {
                                  var text = new string(Encoding.ASCII.GetString(b)
                                  .Where(x => char.IsLetterOrDigit(x) || char.IsSeparator(x) || char.IsPunctuation(x)).ToArray());

                                  var bytes = BitConverter.ToString(b);
                                  log.AppendText($"{bytes} {text}\r\n");
                                  ProduceEvents(bytes);
                              //var literal = ToLiteral(s);
                              //var b = BitConverter.ToString(b);
                              //log.AppendText($"{literal} ({b})");
                          }));
                          }
                      }
                      catch { }
                  });
            }
        }

        private void ProduceEvents(string bytes)
        {
            /* 
             * F5-35-00-3F-14-00-82-50 SQL Open
             * F5-35-03-FF-EB-1F-C9-50 SQL Close
             * F5-35-00-3F-12-00-84-50 SQL Open
             * F5-35-03-FF-ED-1F-C7-50 SQL Close
             */
            string sql = "";
            if (OpenSquelch(bytes))
            {
                sql = "BUSY";
            }
            else if (CloseSquelch(bytes))
            {
                sql = "RX";
            }

            MotorolaScreen.Text = $"{sql} : {display}"; 
        }

        private bool CloseSquelch(string bytes) => bytes.StartsWith("F5-35-03-FF");

        private bool OpenSquelch(string bytes) => bytes.StartsWith("F5-35-00-3F");

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
