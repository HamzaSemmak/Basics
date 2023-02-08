using System;
using System.Xml.Serialization;

namespace sorec_gamma.modules.Config.Models
{
    [Serializable]
    [XmlRoot]
    public class TerminalConfig
    {
        public string ProtocolServer { get; set; }
        public string WsProtocol { get; set; }
        public string DnsServer { get; set; }
        public string ContextServer { get; set; }
        public string WsContext { get; set; }
        public string Num_pdv { get; set; }
        public string Pos_terminal { get; set; }
        public long SeuilAlerte { get; set; }
        public string Protocol_ftp { get; set; }
        public string Host_ftp { get; set; }
        public string Port_ftp { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Source_rep { get; set; }
        public string Usb_rep { get; set; }
        public TerminalConfig()
        {
            SeuilAlerte = 99999;
            ProtocolServer = "";
            WsProtocol = "";
            DnsServer = "";
            ContextServer = "";
            Num_pdv = "";
            Pos_terminal = "";
            Protocol_ftp = "";
            Host_ftp = "";
            Port_ftp = "";
            Login = "";
            Mdp = "";
            Source_rep = "";
            Usb_rep = "";
        }

        public string GetAddress()
        {
            return string.Format("{0}://{1}/{2}", ProtocolServer, DnsServer, ContextServer);
        }
        public string GetWSAddress()
        {
            return WsProtocol + @"://" + DnsServer + @"/" + WsContext;
        }
    }
}
