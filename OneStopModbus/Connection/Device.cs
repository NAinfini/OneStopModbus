using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public class Device
    {
        public int SlaveAddress { get; set; }
        public string Name { get; set; }
        public ObservableCollection<ParameterGroup> ParameterGroups { get; set; }
    }
}