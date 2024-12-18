using DevExpress.Spreadsheet.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    internal class ListParameter : Parameter
    {
        public Dictionary<ushort, string> EnumList { get; set; }

        public override string ParameterValue
        {
            get
            {
                string tempStr = RawValue[0].ToString();
                EnumList.TryGetValue(RawValue[0], out tempStr);
                return tempStr;
            }
            set
            {
                foreach (var item in EnumList)
                {
                    if (item.Value == value)
                    {
                        RawValue[0] = item.Key;
                        break;
                    }
                }
            }
        }
    }
}