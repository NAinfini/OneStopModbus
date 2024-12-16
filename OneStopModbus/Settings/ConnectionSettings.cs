using System.IO.Ports;

namespace OneStopModbus.Settings
{
    public class ConnectionSettings
    {
        public ConnectionType ConnectionType { get; set; } = ConnectionType.Master;
        public ConnectionHardware ConnectionHardware { get; set; } = ConnectionHardware.Serial;
        public bool IsConnected { get; set; }
        public string SerialPortName { get; set; }
        public int BaudRate { get; set; } = 9600;
        public Parity ParityBit { get; set; } = Parity.None;
        public int DataBit { get; set; } = 8;
        public StopBits StopBit { get; set; } = StopBits.One;
        public string TcpIp { get; set; } = "192.168.0.1";
        public int TcpPort { get; set; } = 502;
        public int TcpTimeOut { get; set; } = 3000;
        public int PollDelay { get; set; } = 500;
        public int ResponseTimeOut { get; set; } = 3000;
    }

    public enum ConnectionHardware
    {
        Serial,
        Tcp
    }

    public enum ConnectionType
    {
        Master,
        Slave
    }
}