using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    internal class SintParameter : Parameter
    {
        public override string ParameterValue
        {
            get
            {
                return Convert.ToInt16(RawValue[0]).ToString();
            }
            set
            {
                RawValue[0] = Convert.ToUInt16(value);
            }
        }
    }
}