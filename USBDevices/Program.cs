using System.Collections.ObjectModel;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;

namespace USBDevices
{
    internal static class Program
    {
        private static UsbDevice? _myUsbDevice;
        
        private static void Main(string[] args)
        {
            // Dump all devices and descriptor information to console output.
            var allDevices = UsbDevice.AllDevices;
            
            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (!usbRegistry.Open(out _myUsbDevice)) continue;
                
                Console.WriteLine(_myUsbDevice.Info.ToString());
                    
                foreach (var configInfo in _myUsbDevice.Configs)
                {
                    Console.WriteLine(configInfo.ToString());

                    ReadOnlyCollection<UsbInterfaceInfo> interfaceList = configInfo.InterfaceInfoList;
                    
                    foreach (var interfaceInfo in interfaceList)
                    {
                        Console.WriteLine(interfaceInfo.ToString());

                        ReadOnlyCollection<UsbEndpointInfo> endpointList = interfaceInfo.EndpointInfoList;
                        
                        foreach (var iEndpoint in endpointList)
                        {
                            Console.WriteLine(iEndpoint.ToString());
                        }
                    }
                }
            }
            
            // Free usb resources.
            // This is necessary for libusb-1.0 and Linux compatibility.
            UsbDevice.Exit();
            
            Console.ReadKey();
        }
    }
}