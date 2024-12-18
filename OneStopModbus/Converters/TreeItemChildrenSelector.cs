using DevExpress.Xpf.Grid;
using OneStopModbus.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Converters
{
    public class TreeItemChildrenSelector : IChildNodesSelector
    {
        public IEnumerable SelectChildren(object item)
        {
            if (item is Device)
                return (item as Device).ParameterGroups;
            return null;
        }
    }
}