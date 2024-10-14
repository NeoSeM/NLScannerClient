using NLScanner.Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NLScanner.Client
{
    public class NLScannerClient : IDisposable
    {
        private IntPtr _devices;
        private IntPtr _device;
        private bool _disposed;
        private ConnectionType _connectionType;


        [DllImport("N-ScanHub.dll", EntryPoint = "nl_EnumDevices", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr nl_EnumDevices(out int deviceCount, int enumType);

        [DllImport("N-ScanHub.dll", EntryPoint = "nl_ReleaseDevices", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void nl_ReleaseDevices(IntPtr hDeviceList);

        [DllImport("N-ScanHub.dll", EntryPoint = "nl_OpenDevice", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr nl_OpenDevice(IntPtr hDeviceList, int index, T_Porotocol porotocol = T_Porotocol.Nlscan);

        [DllImport("N-ScanHub.dll", EntryPoint = "nl_CloseDevice", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern bool nl_CloseDevice(ref IntPtr hDevice);

        [DllImport("N-ScanHub.dll", EntryPoint = "nl_SendCommand", CallingConvention = CallingConvention.StdCall)]
        private static extern T_CommunicationResult nl_SendCommand(IntPtr hDevice, string command, int commandLen);

        [DllImport("N-ScanHub.dll", EntryPoint = "nl_GetDeviceInfo", CallingConvention = CallingConvention.StdCall)]

        private static extern bool nl_GetDeviceInfo(IntPtr hDeviceList, int index, out DeviceInfo stNetDevInfo);


        public NLScannerClient(ConnectionType connectionType)
        {
            _connectionType = connectionType;
        }

        public void EnableDevice(Device device, bool playSound = true, bool enableLed = true)
        {
            var currentDevice = nl_OpenDevice(_devices, device.DeviceIndex);
            try
            {
                if (enableLed)
                    EnableLed(currentDevice, 1000);

                if (playSound)
                    PlayStartupSound(currentDevice);

                SendCommand(currentDevice, "SCNENA1");
            }
            finally
            {
                if (currentDevice != IntPtr.Zero)
                    nl_CloseDevice(ref currentDevice);

            }
        }

        public void DisableDevice(Device device)
        {
            var currentDevice = nl_OpenDevice(_devices, device.DeviceIndex);
            try
            {
                SendCommand(currentDevice, "SCNENA0");
            }
            finally
            {
                if (currentDevice != IntPtr.Zero)
                    nl_CloseDevice(ref currentDevice);
            }
        }


        public IEnumerable<Device> ListDevices()
        {
            _devices = nl_EnumDevices(out int deviceCount, (int)_connectionType);
            for (int i = 0; i < deviceCount; i++)
            {
                nl_GetDeviceInfo(_devices, i, out DeviceInfo st_Device_Info);
                yield return new Device { DeviceIndex = i, Info = st_Device_Info.devInfo, DeviceType = st_Device_Info.devType };
            }

        }
        private void PlayStartupSound(IntPtr device)
        {
            string playSoundCommand = "BEEPON500F50T20V";
            string playSoundCommand2 = "BEEPON2000F200T20V";

            SendCommand(device, playSoundCommand);
            Thread.Sleep(50);
            SendCommand(device, playSoundCommand);
            Thread.Sleep(50);
            SendCommand(device, playSoundCommand2);

        }

        private void EnableLed(IntPtr device, int durationMs) =>
            SendCommand(device, $"LEDONI1C{durationMs}D");

        private T_CommunicationResult SendCommand(IntPtr device, string command) =>
            nl_SendCommand(device, command, command.Length);

        private T_CommunicationResult SendCommand(IntPtr device, string command, bool consoleLogEnabled = false)
        {
            var result = nl_SendCommand(device, command, command.Length);
            if (consoleLogEnabled)
            {
                Console.WriteLine($"Command {command} result: {result}");
            }

            return result;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                nl_CloseDevice(ref _device);
                nl_ReleaseDevices(_devices);
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }
    

}

