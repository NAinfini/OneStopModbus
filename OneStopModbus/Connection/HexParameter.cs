using DevExpress.CodeParser;
using DevExpress.Xpf.Grid;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public class HexParameter : Parameter
    {
        public override string ParameterValue
        {
            get
            {
                return "0x" + RawValue[0].ToString("X4");
            }
            set
            {
                if (value.StartsWith("0x"))
                {
                    value = value.Substring(2);
                }
                RawValue[0] = Convert.ToUInt16(value, 16);
            }
        }
    }
}