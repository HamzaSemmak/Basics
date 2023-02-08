using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.UTILS
{
    public class NetworkInterfaceModel
    {
        private string name;
        private string addressIP;
        private string subnetMask;
        private string defaultGateway;
        private string dnsServer;
        private string altDnsServer;
        private bool dhcpEnabled;
        private string type;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string AddressIP
        {
            get { return addressIP; }
            set { addressIP = value; }
        }
        public string SubnetMask
        {
            get { return subnetMask; }
            set { subnetMask = value; }
        }

        public string DefaultGateway
        {
            get { return defaultGateway; }
            set { defaultGateway = value; }
        }
        public string DNSServer
        {
            get { return dnsServer; }
            set { dnsServer = value; }
        }

        public string AltDNSServer
        {
            get { return altDnsServer; }
            set { altDnsServer = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public bool DHCPEnabled
        {
            get { return dhcpEnabled; }
            set { dhcpEnabled = value; }
        }
    }
}
