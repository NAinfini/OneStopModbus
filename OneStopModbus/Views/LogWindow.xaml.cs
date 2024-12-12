using DevExpress.Xpf.Core.FilteringUI;
using OneStopModbus.Helper;
using Serilog.Events;
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
    public partial class LogWindow : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _showVerbose = true;

        public bool ShowVerbose
        {
            get => _showVerbose;
            set { _showVerbose = value; OnPropertyChanged(nameof(ShowVerbose)); UpdateFilterString(); }
        }

        private bool _showInfo = true;

        public bool ShowInfo
        {
            get => _showInfo;
            set { _showInfo = value; OnPropertyChanged(nameof(ShowInfo)); UpdateFilterString(); }
        }

        private bool _showDebug = true;

        public bool ShowDebug
        {
            get => _showDebug;
            set { _showDebug = value; OnPropertyChanged(nameof(ShowDebug)); UpdateFilterString(); }
        }

        private bool _showWarning = true;

        public bool ShowWarning
        {
            get => _showWarning;
            set { _showWarning = value; OnPropertyChanged(nameof(ShowWarning)); UpdateFilterString(); }
        }

        private bool _showError = true;

        public bool ShowError
        {
            get => _showError;
            set { _showError = value; OnPropertyChanged(nameof(ShowError)); UpdateFilterString(); }
        }

        private void UpdateFilterString()
        {
            var conditions = new List<string>();
            if (ShowInfo) conditions.Add("[Level] = 'Information'");
            if (ShowDebug) conditions.Add("[Level] = 'Debug'");
            if (ShowWarning) conditions.Add("[Level] = 'Warning'");
            if (ShowError) conditions.Add("[Level] = 'Error'");
            if (ShowVerbose) conditions.Add("[Level] = 'Verbose'");

            FilterString = conditions.Count > 0 ? string.Join(" OR ", conditions) : "1 = 0";
        }

        private string _filterString = string.Empty;

        public string FilterString
        {
            get => _filterString;
            set { _filterString = value; OnPropertyChanged(nameof(FilterString)); }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<LogMsg> logMsgs { get; set; }

        public LogWindow()
        {
            DataContext = this;
            InitializeComponent();
            SystemLogs.Initialize();
            logMsgs = SystemLogs.Instance.logMsgs;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(logMsgs)));
        }
    }
}