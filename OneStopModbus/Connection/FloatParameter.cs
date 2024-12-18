using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public class FloatParameter : Parameter
    {
        public override string ParameterValue
        {
            get
            {
                byte[] floatBytes = new byte[4];
                floatBytes[0] = (byte)(RawValue[0] & 0xFF);
                floatBytes[1] = (byte)(RawValue[0] >> 8);
                floatBytes[2] = (byte)(RawValue[1] & 0xFF);
                floatBytes[3] = (byte)(RawValue[1] >> 8);
                float floatValue = BitConverter.ToSingle(floatBytes, 0);
                return floatValue.ToString();
            }
            set
            {
                float floatValue = float.Parse(value);
                byte[] floatBytes = BitConverter.GetBytes(floatValue);
                RawValue[0] = (ushort)(floatBytes[0] + (floatBytes[1] << 8));
                RawValue[1] = (ushort)(floatBytes[2] + (floatBytes[3] << 8));
            }
        }
    }
}