using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Motorola
{
    public class DisplayAdapter
    {
        private Dictionary<string, string> lines = new Dictionary<string, string>();

        private bool squelchOpened;
        public string Display => $"{(squelchOpened ? "BUSY" : "RX")} {string.Join("\r\n", lines.Values)}";
        public event EventHandler<string> Updated;
        public void Subscribe(IEnumerable<byte[]> bytesSeq)
        {
            try
            {
                foreach (var bytes in bytesSeq)
                {
                    var bytesAsString = BitConverter.ToString(bytes);
                    if (OpenSquelch(bytesAsString))
                    {
                        squelchOpened = true;
                    }
                    else if (CloseSquelch(bytesAsString))
                    {
                        squelchOpened = false;
                    }
                    else if (DisplayUpdate(bytesAsString))
                    {
                        var addr = bytesAsString.Substring(DisplayPrefix.Length - 1, 5);
                        var subArray = bytes.Skip(6).Take(bytes.Length - 8).ToArray();
                        lines[addr]=  $"{Encoding.ASCII.GetString(subArray)}";
                    }
                    Updated?.Invoke(this, $"{bytesAsString} {Encoding.ASCII.GetString(bytes)}");
                }

            }
            catch { }
        }

        
        private const string DisplayPrefix = "FF-34-00-";
        private static bool DisplayUpdate(string bytes) => bytes.StartsWith(DisplayPrefix);

        private static bool CloseSquelch(string bytes) => bytes.StartsWith("F5-35-03-FF");

        private static bool OpenSquelch(string bytes) => bytes.StartsWith("F5-35-00-3F");

    }
}


