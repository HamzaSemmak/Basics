using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Model;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

namespace sorec_gamma.modules.UTILS
{
    public class TerminalUtils
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(TerminalUtils));

        public const string LastTicketsDir = @"Images\tickets";
        public static void initFiles()
        {
            try
            {
                if (ApplicationContext.develop && !Directory.Exists(LastTicketsDir))
                {
                    _ = Directory.CreateDirectory(LastTicketsDir);
                }
                ConfigUtils.initTerminalConfigXmlFile();
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("TerminalUtils Exception : " + ex.Message + "\n" + ex.StackTrace);
            }
        }
        public static void InitAddressIP()
        {
            try
            {
                if (ApplicationContext.develop)
                {
                    ApplicationContext.IP = "192.168.6.12";
                }
                else
                {
                    try
                    {
                        NetworkInterfaceModel EthernetInterface = NetworkInterfaceUtils.GetNetworkInterfaceInfoByType(NetworkInterfaceType.Ethernet);
                        ApplicationContext.IP = EthernetInterface.AddressIP;
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("TerminalUtils Exception : " + ex.Message + "\n" + ex.StackTrace);
            }
        }

        public static void updateLastTicketInfos(Ticket ticket)
        {
            Logger.Info(string.Format("UpdateLastTicketInfos: {0}", ticket));
            ApplicationContext.LAST_TICKET_INFO = ticket;
        }

        public static void updateLastVoucherInfos(Voucher voucher)
        {
            Logger.Info(string.Format("UpdateLastVoucherInfos: {0}", voucher));
            ApplicationContext.LAST_VOUCHER_INFO = voucher;
        }

        public static void WaitForTraitement(int tentative)
        {
            int count = 0;
            while (count < tentative)
            {
                Thread.Sleep(1000);
                count++;
            }
        }
    }
}
