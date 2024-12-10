using OneStopModbus.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for LogWindow.xaml
    /// </summary>
    public partial class LogWindow : UserControl
    {
        public ObservableCollection<LogMsg> logMsgs { get; set; }

        public LogWindow()
        {
            DataContext = this;
            InitializeComponent();
            logMsgs = SystemLogs.Instance.logMsgs;
            logMsgs.Add(new LogMsg() { TimeStamp = "lol", Lvl = "Information", Text = "Log Window Initialized" });
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            SystemLogsGrid.RefreshData();
        }
    }
}