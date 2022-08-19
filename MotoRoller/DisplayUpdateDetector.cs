using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text;

namespace MotoRoller
{
    public class DisplayUpdateDetector
    {
        private List<byte> _currentMessage = new();
        private List<byte> _indicatorsUpd = new();
        public event EventHandler DisplayUpdated;
        public event EventHandler IndicatorsUpdated;
        public ConcurrentDictionary<byte, string> DisplayRows { get; } = new();
        public Indicators Indicators { get; } = new Indicators();

        public void AddByte(byte b)
        {
            if (b == 0x50)
            {
                _indicatorsUpd.Clear();
                _currentMessage.Clear();
                return;
            }
            HandleMainDisplayByte(b);
            HandleIndicatorsUpd(b);
        }

        private void HandleIndicatorsUpd(byte b)
        {
            if (_indicatorsUpd.Count == 0 && b == 0xf5)
            {
                _indicatorsUpd.Add(b);
                return;
            }
            else if (_indicatorsUpd.Count == 1 && b == 0x35)
            {
                _indicatorsUpd.Add(b);
                return;
            }
            else if (_indicatorsUpd.Count > 1 && _indicatorsUpd.Count < 7)
            {
                _indicatorsUpd.Add(b);
            }
            if (_indicatorsUpd.Count == 7)
            {
                var opcode = _indicatorsUpd[2];
                if (opcode == 0x00)
                {
                    Indicators.Update(_indicatorsUpd.Skip(3).Take(3).ToArray());                    
                }
                else if (opcode == 0x03)
                {
                    Indicators.Clear(_indicatorsUpd.Skip(3).Take(3).ToArray());
                }
                IndicatorsUpdated?.Invoke(this, EventArgs.Empty);
                _indicatorsUpd.Clear();
            }
        }

        private void HandleMainDisplayByte(byte b)
        {
            if (_currentMessage.Count == 0 && b == 0xFF)
            {
                _currentMessage.Add(b);
                return;
            }
            else if (_currentMessage.Count == 1)
            {
                if (b == 0x34)
                {
                    _currentMessage.Add(b);
                }
                else
                {
                    _currentMessage.Clear();
                }
                return;
            }
            else if (_currentMessage.Count >= 2)
            {
                _currentMessage.Add(b);
            }
            if (_currentMessage.Count > 6)
            {
                var size = _currentMessage[3];
                var finishSize = size + 3;
                if (_currentMessage.Count >= finishSize)
                {
                    var line = Encoding.GetEncoding(866).GetString(_currentMessage.Skip(6).Take(size - 3).ToArray());
                    DisplayRows[_currentMessage[5]] = line;
                    Console.WriteLine("Dislplay update detected:" + line);
                    Notify();
                    _currentMessage.Clear();
                }
            }
        }

        private void Notify()
        {
            DisplayUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
