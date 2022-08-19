using System.Collections;

namespace MotoRoller
{
    public class Indicators
    {
        private BitArray _bits = new BitArray(new byte[3]);
        public void Update(byte[] bytes)
        {
            _bits = new BitArray(bytes.Reverse().ToArray());
        }
        public void Clear(byte[] mask)
        {
            var bitArray = new BitArray(mask.Reverse().ToArray());
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray.Get(i))
                {
                    _bits[i] = false;
                }
            }
        }
        public bool DTMF => Bit(1);
        public bool TalkAround => Bit(2);
        public bool ExternalAlarm => Bit(3);
        public bool KeypadLock => Bit(4);
        public bool MissedCall => Bit(5);
        public bool XPAND1 => Bit(9);
        public bool LowPower => Bit(10);
        public bool HighPower => Bit(11);
        public bool Monitor => Bit(12);
        public bool Scan => Bit(13);
        public bool ScanDot => Bit(14);
        public bool Triangle => Bit(15);
        public bool VoiceRecorder => Bit(16);
        public bool Antenna => Bit(17);
        public bool S1 => Bit(18);
        public bool S2 => Bit(19);
        public bool S3 => Bit(20);
        public bool S4 => Bit(21);
        public bool S5 => Bit(22);
        public bool OptionBoard => Bit(23);
        public bool XPAND2 => Bit(24);

        private bool Bit(int bitNumber) => _bits.Get(bitNumber-1);
    }
}
