using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public abstract class Parameter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Address { get; set; }

        public int Length { get; set; }

        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public bool AllowWrite { get; set; }

        public abstract string ParameterValue { get; set; }
        public ushort[] RawValue { get; set; }
    }
}