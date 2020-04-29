using System;
using System.Linq;
using System.Net.NetworkInformation;

public static class MacFetcher
{
    //gets the mac address of the computer and returns the value by string
    public static string _string
    {
        get
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();
        }
    }
}
