using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace RaffRegServer.Classes
{
    class Computador
    {
        public string MachineName()
        {
            string m = Environment.MachineName;
            return m;
        }

        //*** Tá aqui o pau
        /*
        public string ShowNetworkInterfaces()
        {
            string ret = "";
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {
                ret += "Não foram encontradas inferfaces de rede";
                return ret;
            }
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                if (adapter.OperationalStatus.ToString().Equals("Up") && adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    ret += adapter.Description.ToString() + ":";
                    string mac = string.Join(":", (from z in adapter.GetPhysicalAddress().GetAddressBytes() select z.ToString("X2")).ToArray());
                    ret += Environment.NewLine;
                    ret += "MAC: " + mac;
                    ret += Environment.NewLine;
                    ret += "IPV4: " + adapter.GetIPProperties().UnicastAddresses[1].Address;
                    ret += Environment.NewLine;
                    //ret += "IPV6: " + adapter.GetIPProperties().UnicastAddresses[0].Address;
                    //ret += Environment.NewLine;
                    ret += (String.Empty.PadLeft(33, '='));
                    //ret += Environment.NewLine;
                }
            }
            return ret;
        }
        */
    }
}
