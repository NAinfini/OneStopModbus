using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public class BitParameter : Parameter
    {
        public int bitNumber { get; set; }

        public override string ParameterValue
        {
            get
            {
                return ((RawValue[0] & (1 << Length)) != 0).ToString();
            }
            set
            {
                if (value == "1")
                {
                    RawValue[0] |= (ushort)(1 << Length);
                }
                else
                {
                    RawValue[0] &= (ushort)~(1 << Length);
                }
            }
        }
    }
}