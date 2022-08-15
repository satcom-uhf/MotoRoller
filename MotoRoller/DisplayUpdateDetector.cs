using System.Collections.Concurrent;
using System.Text;

namespace MotoRoller
{
    public class DisplayUpdateDetector
    {
        private List<byte> _currentMessage = new();
        public event EventHandler MessageDetected;
        public ConcurrentDictionary<byte, string> DisplayRows { get; } = new();
        public void AddByte(byte b)
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
                if (_currentMessage.Count == size + 3)
                {
                    var line = Encoding.GetEncoding(866).GetString(_currentMessage.Skip(6).ToArray());
                    DisplayRows[_currentMessage[5]] = line;
                    Console.WriteLine("Dislplay update detected:" + line);
                    Notify();
                    _currentMessage.Clear();
                }
            }
        }
        private void Notify()
        {
            MessageDetected?.Invoke(this, EventArgs.Empty);
        }
    }
}
