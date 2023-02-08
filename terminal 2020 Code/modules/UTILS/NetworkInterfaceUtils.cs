using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

/**
 * Created by yelkarkouri@PCARD
 */
namespace sorec_gamma.modules.UTILS
{
    public class NetworkInterfaceUtils
    {
        /**
         * Check network status
         */
        private static readonly ILog Logger = LogManager.GetLogger(typeof(NetworkInterfaceUtils));

        public static bool IsNetworkAvailable(long minimumSpeed)
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return false;

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // discard because of standard reasons
                if ((ni.OperationalStatus == OperationalStatus.Up)
                    && (ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    && (ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    && ni.Speed >= minimumSpeed
                    && ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) < 0
                    && ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) < 0
                    && !ni.Description.Equals("Microsoft Loopback Adapter", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        /*
         * Get all network interfaces
         */
        public static NetworkInterfaceModel[] GetAllNetworkInterfaces()
        {
            NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterfaceModel[] networkInterModels = new NetworkInterfaceModel[networks.Length];
            int i = 0;
            foreach (NetworkInterface net in networks)
            {
                string ipAddress = "";
                foreach (UnicastIPAddressInformation ip in net.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddress = ip.Address.ToString();
                        NetworkInterfaceModel nim = new NetworkInterfaceModel();
                        nim.Name = net.Name;
                        nim.Type = net.NetworkInterfaceType.ToString();
                        nim.AddressIP = ipAddress;
                        networkInterModels[i] = nim;
                        i++;
                    }
                }
            }
            return networkInterModels;
        }

        /*
         * Get network interface by type
         */
        public static NetworkInterface GetNetworkInterfaceByType(NetworkInterfaceType networkInterfaceType)
        {
            NetworkInterface netInt = null;
            try
            {
                NetworkInterface[] nInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in nInterfaces)
                {
                    if (!ni.Supports(NetworkInterfaceComponent.IPv4))
                    {
                        continue;
                    }
                    if (ni.NetworkInterfaceType == networkInterfaceType)
                    {
                        netInt = ni;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
            }
            return netInt;
        }

        public static UnicastIPAddressInformation GetIpAddressInfo(NetworkInterface adapter)
        {
            UnicastIPAddressInformationCollection uca = adapter.GetIPProperties().UnicastAddresses;
            foreach (UnicastIPAddressInformation ip in uca)
            {
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return null;
        }

        /*
         * Gateway address info
         */
        public static GatewayIPAddressInformation GetGatewayAddressInfo(NetworkInterface adapter)
        {
            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
            foreach (GatewayIPAddressInformation address in addresses)
            {
                if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return address;
            }
            return null;
        }

        /*
         * Get dns addresses
         */
        public static IPAddress[] GetDnsAddress(NetworkInterface networkInterface)
        {
            IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
            IPAddressCollection dnsAddresses = ipProperties.DnsAddresses;
            IPAddress[] iPAddress = new IPAddress[10];
            int i = 0;
            foreach (IPAddress address in dnsAddresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    iPAddress[i] = address;
                    i++;
                }
            }
            return iPAddress;
        }

        /*
         * Is DHCP enabled
         */
        public static bool IsDHCPEnabled(NetworkInterface networkInterface)
        {
            try
            {
                if (networkInterface.Supports(NetworkInterfaceComponent.IPv4))
                {
                    IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();
                    if (ipProperties == null)
                    {
                        return false;
                    }
                    IPv4InterfaceProperties ipV4Properties = ipProperties.GetIPv4Properties();
                    return ipV4Properties != null && ipV4Properties.IsDhcpEnabled;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
                return false;
            }
        }

        /*
         * recuperate network interface info by type
         */
        public static NetworkInterfaceModel GetNetworkInterfaceInfoByType(NetworkInterfaceType networkInterfaceType)
        {
            NetworkInterfaceModel networkInterfaceModel = new NetworkInterfaceModel();
            NetworkInterface networkInterface = GetNetworkInterfaceByType(networkInterfaceType);
            if (networkInterface == null)
            {
                return networkInterfaceModel;
            }
            UnicastIPAddressInformation addressIp = null;
            GatewayIPAddressInformation gateway = null;
            IPAddress[] dns = null;
            try
            {
                addressIp = GetIpAddressInfo(networkInterface);
                gateway = GetGatewayAddressInfo(networkInterface);
                dns = GetDnsAddress(networkInterface);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
            }

            networkInterfaceModel.DHCPEnabled = IsDHCPEnabled(networkInterface);
            networkInterfaceModel.Name = networkInterface.Name;

            networkInterfaceModel.AddressIP = (addressIp == null || addressIp.Address == null) ? null : addressIp.Address.ToString();
            networkInterfaceModel.SubnetMask = (addressIp == null || addressIp.IPv4Mask == null) ? null : addressIp.IPv4Mask.ToString();

            networkInterfaceModel.DefaultGateway = (gateway == null || gateway.Address == null) ? null : gateway.Address.ToString();

            networkInterfaceModel.DNSServer = (dns == null || dns.Length < 0 || dns[0] == null) ? null : dns[0].ToString();
            networkInterfaceModel.AltDNSServer = (dns == null || dns.Length < 1 || dns[1] == null) ? null : dns[1].ToString();

            return networkInterfaceModel;
        }

        /*
        * Rename network interface   
        */
        public static ExecCommandResult RenameNetworkInterface(string networkInterfaceOldName, string networkInterfaceNewName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface set interface name=\"{0}\" newname=\"{1}\"", networkInterfaceOldName, networkInterfaceNewName);
          //  LogUtils.addLog("Launch Command: " + cmd, 1);

            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }

        /*
         * Set ip address static or dhcp 
         */
        public static ExecCommandResult SetAddressIP(NetworkInterfaceModel networkInterfaceModel)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd;
            if (networkInterfaceModel.DHCPEnabled)
            {
                cmd = string.Format("interface ip set address \"{0}\" dhcp", networkInterfaceModel.Name);
            }
            else
            {
                cmd = string.Format("interface ip set address \"{0}\" static {1} {2} {3} none",
                    networkInterfaceModel.Name, networkInterfaceModel.AddressIP, networkInterfaceModel.SubnetMask, networkInterfaceModel.DefaultGateway);
            }
            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }

        /*
         * set static dns 
         */
        public static ExecCommandResult SetStaticDNS(string networkInterfaceName, string prefDns)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface ip set dns \"{0}\" static {1}", networkInterfaceName, prefDns);
            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }  
        /*
         * set static dns 
         */
        public static ExecCommandResult DeleteStaticDNS(string networkInterfaceName, string prefDns)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface ip delete dns \"{0}\" {1}", networkInterfaceName, prefDns);
            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }      
        
        /*
         * set static dns 
         */
        public static ExecCommandResult DeleteAllDNSServers(string networkInterfaceName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface ip delete dnsserver \"{0}\" all", networkInterfaceName);
            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }

        /*
         * set alternate dns 
         */
        public static ExecCommandResult AddAltDNS(string networkInterfaceName, string altDNS)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface ip add dns \"{0}\" {1} index=2", networkInterfaceName, altDNS);
          //  LogUtils.addLog("Launch Command: " + cmd, 1);

            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }
        /*
         * set dynamic dns  
         */
        public static ExecCommandResult SetDNSProvidedByDHCP(string networkInterfaceName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "netsh";
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Verb = "runas";
            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(850);

            string cmd = string.Format("interface ip set dnsservers \"{0}\" source=dhcp", networkInterfaceName);
    //        LogUtils.addLog("Launch Command: " + cmd, 1);

            ExecCommandResult result = new ExecCommandResult();
            bool started = process.Start();
            if (started)
            {
                StreamWriter streamWriter = new StreamWriter(process.StandardInput.BaseStream, Encoding.GetEncoding(850));
                streamWriter.Write(cmd);
                streamWriter.Flush();
                streamWriter.Close();

                process.WaitForExit();

                result.ErrorMessage = process.StandardOutput.ReadToEnd();
                result.ErrorMessage = result.ErrorMessage.Replace("netsh>", "");
                result.Succeed = process.ExitCode == 0;
            }
            return result;
        }

        /*
         * Ping host by name or ip address
         */
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }

        /*
        * set date  
        */
        public static void SetDate(string date)
        {
            var proc = new ProcessStartInfo();
            proc.UseShellExecute = false;
            proc.CreateNoWindow = true;
            proc.FileName = "cmd.exe";
            // proc.Verb = "runas";
            proc.Arguments = "/C date " + date;
            try
            {
                _ = Process.Start(proc);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
            }
        }
        /*
        * set time  
        */
        public static void SetTime(string time)
        {
            var proc = new ProcessStartInfo();
            proc.UseShellExecute = false;
            proc.CreateNoWindow = true;
            proc.FileName = "cmd.exe";
            // proc.Verb = "runas";
            proc.Arguments = "/C time " + time;
            try
            {
                _ = Process.Start(proc);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception message :{0}, StackTrace: {1}" + ex.Message, ex.StackTrace));
            }
        }
    }
}
