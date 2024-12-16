using NModbus;
using NModbus.Data;
using NModbus.Device;
using NModbus.Serial;
using OneStopModbus.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OneStopModbus.Connection
{
    public class ModbusConnection
    {
        private ConnectionSettings _connectionSettings;
        private ModbusFactory _modbusFactory = new ModbusFactory();
        public IModbusMaster _modbusMaster;
        public IModbusSlaveNetwork _modbusSlaveNetwork;
        private SerialPort _serialPort;

        private TcpListener tcpListener;

        public ModbusConnection()

        {
            _connectionSettings = ProjectSettings.Instance.ConnectionSettings;
        }

        public void StartConnection()
        {
            try
            {
                if (_connectionSettings.ConnectionType == ConnectionType.Master)
                {
                    StartMasterConnection();
                }
                else if (_connectionSettings.ConnectionType == ConnectionType.Slave)
                {
                    StartSlaveConnection();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error starting connection");
            }
        }

        public void StartMasterConnection()
        {
            if (_connectionSettings.ConnectionHardware == ConnectionHardware.Serial)
            {
                _serialPort = new SerialPort(_connectionSettings.SerialPortName, _connectionSettings.BaudRate, _connectionSettings.ParityBit, _connectionSettings.DataBit, _connectionSettings.StopBit);
                if (_serialPort.IsOpen)
                {
                    Log.Error($"Serial port is already open, try a different port");
                    return;
                }
                _serialPort.Open();
                _modbusMaster = _modbusFactory.CreateRtuMaster(_serialPort);
                Log.Information($"Modbus master created");
            }
            else if (_connectionSettings.ConnectionHardware == ConnectionHardware.Tcp)
            {
                TcpClient tcpClient = new TcpClient(_connectionSettings.TcpIp, _connectionSettings.TcpPort);
                _modbusMaster = _modbusFactory.CreateMaster(tcpClient);
                Log.Information($"Modbus master created");
            }
            else
            {
                Log.Error($"Connection Hardware: '{_connectionSettings.ConnectionHardware}'not supported(Not suppose to happen)");
            }
        }

        private void StartSlaveConnection()
        {
            if (_connectionSettings.ConnectionHardware == ConnectionHardware.Serial)
            {
                _serialPort = new SerialPort(_connectionSettings.SerialPortName, _connectionSettings.BaudRate, _connectionSettings.ParityBit, _connectionSettings.DataBit, _connectionSettings.StopBit);
                if (_serialPort.IsOpen)
                {
                    Log.Error("Serial port is already open, try a different port");
                    return;
                }
                _serialPort.Open();
                _modbusSlaveNetwork = _modbusFactory.CreateRtuSlaveNetwork(_serialPort);
                Log.Information($"Modbus slave network created");
            }
            else if (_connectionSettings.ConnectionHardware == ConnectionHardware.Tcp)
            {
                tcpListener = new TcpListener(IPAddress.Any, _connectionSettings.TcpPort);
                tcpListener.Start();
                _modbusSlaveNetwork = _modbusFactory.CreateSlaveNetwork(tcpListener);
                Log.Information($"Modbus slave network created");
            }
            else
            {
                Log.Error($"Connection Hardware: '{_connectionSettings.ConnectionHardware}'not supported(Not suppose to happen)");
            }
        }

        public void StopConnection()
        {
            if (_connectionSettings.ConnectionType == ConnectionType.Master)
            {
                _modbusMaster.Dispose();
            }
            else
            {
                _modbusSlaveNetwork.Dispose();
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                }
            }
            if (_serialPort != null)
            {
                _serialPort.Close();
            }
        }
    }
}