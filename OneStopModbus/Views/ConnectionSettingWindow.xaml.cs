using OneStopModbus.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using NModbus;
using NModbus.Serial;

namespace OneStopModbus.Views
{
    /// <summary>
    /// Interaction logic for ConnectionSettingWindow.xaml
    /// </summary>
    public partial class ConnectionSettingWindow : UserControl, INotifyPropertyChanged
    {
        private Timer _timer;
        public List<int> BaudRateList => new List<int> { 9600, 19200, 38400, 57600, 115200 };

        public List<Parity> ParityBitList => new List<Parity> { Parity.None, Parity.Odd, Parity.Even, Parity.Mark, Parity.Space };
        public List<StopBits> StopBitList => new List<StopBits> { StopBits.None, StopBits.One, StopBits.OnePointFive, StopBits.Two };

        public List<int> DataBitList => new List<int> { 5, 6, 7, 8 };

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ConnectionSettings _connectionSettings;

        public ConnectionSettings ConnectionSettings
        {
            get => _connectionSettings;
            set { _connectionSettings = value; OnPropertyChanged(nameof(ConnectionSettings)); }
        }

        private List<string> _serialPorts = new List<string>();

        public List<string> SerialPortList
        {
            get
            {
                return _serialPorts;
            }
            set { _serialPorts = value; OnPropertyChanged(nameof(SerialPortList)); }
        }

        public ConnectionSettingWindow()
        {
            DataContext = this;
            ConnectionSettings = ProjectSettings.Instance.ConnectionSettings;
            StartSerialPortMonitor();
            InitializeComponent();
        }

        private void StartSerialPortMonitor()
        {
            // Initialize the serial port list
            UpdateSerialPorts();

            // Set up a timer to poll for changes every 1 second
            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void StopSerialPortMonitor()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateSerialPorts();
        }

        private void UpdateSerialPorts()
        {
            // Get the current list of available serial ports
            SerialPortList = SerialPort.GetPortNames().ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}