using DevExpress.Xpf.Spreadsheet.Internal;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;

namespace OneStopModbus.Connection
{
    public class UintParameter : Parameter
    {
        public override string ParameterValue
        {
            get
            {
                return RawValue[0].ToString();
            }
            set
            {
                RawValue[0] = Convert.ToUInt16(value);
            }
        }
    }
}