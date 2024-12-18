using DevExpress.Xpf.Grid;
using OneStopModbus.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OneStopModbus.Views
{
    /// <summary>
    /// Interaction logic for DeviceTreeView.xaml
    /// </summary>
    public partial class DeviceTreeView : UserControl
    {
        public ObservableCollection<Device> Devices { get; set; } = new ObservableCollection<Device>()
        {
           new Device()
    {
        SlaveAddress = 1,
        Name = "Device 1",
        ParameterGroups = new ObservableCollection<ParameterGroup>()
        {
            new ParameterGroup()
            {
                Name = "Group 1",
                Parameters = new ObservableCollection<Parameter>()
                {
                    new UintParameter() { Name = "Param 1" },
                    new UintParameter() { Name = "Param 2" },
                }
            }
        }
    },
    new Device()
    {
        SlaveAddress = 2,
        Name = "Device 2",
        ParameterGroups = new ObservableCollection<ParameterGroup>()
        {
            new ParameterGroup()
            {
                Name = "Group A",
                Parameters = new ObservableCollection<Parameter>()
                {
                    new UintParameter() { Name = "Param A1" },
                    new UintParameter() { Name = "Param A2" },
                }
            }
        }
    }
        };

        public DeviceTreeView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void TreeViewControl_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }
    }
}