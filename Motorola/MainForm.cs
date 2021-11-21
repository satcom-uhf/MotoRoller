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
        List<byte[]> arrays = new List<byte[]>();
        ImageStreamingServer server;
        SerialPort port;
        bool squelch = false;
        bool freqRead = false;
        string currentFreq = "UNKNOWN";
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
                server.Start();
                port = new SerialPort(comPorts.SelectedItem.ToString());
                port.DataReceived += Port_DataReceived;
                port.Open();
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var readCount = port.BytesToRead;
            var bytes = new byte[readCount];
            port.Read(bytes, 0, readCount);
            arrays.Add(bytes);
            var s = Encoding.ASCII.GetString(bytes);
            if (!squelch && (s.Contains("\u0012") || s.Contains("\u0014")))
            {
                squelch = true;
            }
            else if (squelch && s.Contains("\u0003"))
            {
                squelch = false;
            }

            if (!freqRead && s.Contains("\u0011"))
            {
                freqRead = true;
                currentFreq = "";
            }
            else if (freqRead && s.Contains("P"))
            {
                freqRead = false;
            }

            if (freqRead)
            {
                currentFreq += new string(s.Where(x => char.IsLetterOrDigit(x) || char.IsSeparator(x) || char.IsPunctuation(x)).ToArray());
            }


            var display = squelch ? "(BUSY)" : "RX";
            display = $"{display} : {ExtractFreq(currentFreq)}";
            MotorolaScreen.Invoke(new Action(() => MotorolaScreen.Text = display));
        }

        private string ExtractFreq(string currentFreq)
        {
            try
            {
                var match = Regex.Match(currentFreq, @"\d{3}\.\d{3}", RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2));
                return match?.Value;
            }
            catch
            {
                return "UNKNOWN";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            port.Close();
        }

        static string ToLiteral(string input)
        {
            StringBuilder literal = new StringBuilder(input.Length + 2);
            literal.Append("\"");
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        // ASCII printable character
                        if (c >= 0x20 && c <= 0x7e)
                        {
                            literal.Append(c);
                            // As UTF16 escaped character
                        }
                        else
                        {
                            literal.Append(@"\u");
                            literal.Append(((int)c).ToString("x4"));
                        }
                        break;
                }
            }
            literal.Append("\"");
            return literal.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            comPorts.Items.AddRange(SerialPort.GetPortNames());
        }
    }
}
