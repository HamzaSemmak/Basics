using calipso2020;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleMAJ;
using sorec_gamma.modules.UTILS;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace sorec_gamma.IHMs
{
    public partial class ReglagesForm : BaseForm
    {
        private string textDnsServer;
        private string textContextServer;
        private string textContext2Server;
        private string textip1;
        private string textip2;
        private string textip3;
        private string textip4;
        private string textMasque1;
        private string textMasque2;
        private string textMasque3;
        private string textMasque4;
        private string textPassr1;
        private string textPassr2;
        private string textPassr3;
        private string textPassr4;
        private string textPing1;
        private string textPing2;
        private string textPing3;
        private string textPing4;

        private string textMdp;
        private string textCle;

        private string textDnsPref1;
        private string textDnsPref2;
        private string textDnsPref3;
        private string textDnsPref4;
        private string textDnsAlt1;
        private string textDnsAlt2;
        private string textDnsAlt3;
        private string textDnsAlt4;

        private bool isDhcp = false;
        private int flagPing = 0;
        private int flagConfig = 0;
        private int flagServeur = 0;
        private int flagMaj = 0;
        private int flag = 1;
        private string oldWebSocketUri = "";

        private Timer _dateTimeTimer;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (_dateTimeTimer != null)
                    _dateTimeTimer.Dispose();
            }

            base.Dispose(disposing);
        }
        private void UpdateGuichetInfo()
        {
            if (ConfigUtils.ConfigData != null)
            {
                string newWebSocketUri = ConfigUtils.ConfigData.GetWSAddress();
                if (oldWebSocketUri != newWebSocketUri)
                {
                    oldWebSocketUri = newWebSocketUri;
                    ApplicationContext.InitWebSocketClient();
                }
            }
            TerminalUtils.InitAddressIP();
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                 + ApplicationContext.SOREC_DATA_ENV;
            lblCaisse.Text = "Guichet  " + ConfigUtils.ConfigData.Num_pdv
                + " - " + ConfigUtils.ConfigData.Pos_terminal;
            Serveur.Text = ConfigUtils.ConfigData.GetAddress();
            flag = 0;
            lblIp.Text = "IP " + ApplicationContext.IP;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }
        public ReglagesForm()
        {
            InitializeComponent();
            etatConnNokPb.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOkPB.Visible = ApplicationContext.IsNetworkOnline;
            Initilize();
            UpdateMaj();
            _dateTimeTimer = new Timer();
            _dateTimeTimer.Enabled = true;
            _dateTimeTimer.Interval = 1;
            _dateTimeTimer.Tick += new EventHandler(this._dateTimeTick);
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            if (ConfigUtils.ConfigData != null)
            {
                oldWebSocketUri = ConfigUtils.ConfigData.GetWSAddress();
            }
            Initilize();
        }

        public void Initilize()
        {
            etatConnNokPb.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOkPB.Visible = ApplicationContext.IsNetworkOnline;
            _dateTimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _dateTimeTimer.Tick += new EventHandler(_dateTimeTick);
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;

            isDhcp = false;
            this.flagPing = 0;
            this.flagConfig = 0;
            this.flag = 1;
            this.flagServeur = 1;
            flagMaj = 0;
            UpdateGuichetInfo();
            this.btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;

            if (TerminalMonitor.IsUserAnAdmin())
            {
                updateBtn.Show();
                refreshUpdateView(true);
            }
            else
            {
                updateBtn.Hide();
            }
            panelAutres.Hide();
            pingPanel.Hide();
            majPanel.Hide();
            networkPanel.Hide();
            panelCaisse2.Hide();
            serverPanel.Show();
            panelKeyboard.Show();

            UpdateServer();
            UpdateMaj();

            updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
        }

        private void conn_HealthCheck(object sender, EventArgs e)
        {
            try
            {
                ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
                bool _isNetworkOnline = (bool)connEventArgs.UserState;
                etatConnNokPb.Invoke(new MethodInvoker(delegate { etatConnNokPb.Visible = !_isNetworkOnline; }));
                etatConnOkPB.Invoke(new MethodInvoker(delegate { etatConnOkPB.Visible = _isNetworkOnline; }));
            }
            catch { }
        }

        private void UpdateServer()
        {
            string _protocoleServer = string.IsNullOrEmpty(ConfigUtils.ConfigData.ProtocolServer) ? "http" : ConfigUtils.ConfigData.ProtocolServer;
            string _wsProtocoleServer = string.IsNullOrEmpty(ConfigUtils.ConfigData.WsProtocol) ? "ws" : ConfigUtils.ConfigData.WsProtocol;
            string _contextServer = ConfigUtils.ConfigData.ContextServer;
            string _wscontextServer = ConfigUtils.ConfigData.WsContext;

            Serveur.Text = string.Format("{0}://{1}/{2}", _protocoleServer, ConfigUtils.ConfigData.DnsServer, _contextServer);
            bool https = _protocoleServer == "https";
            bool wss = _wsProtocoleServer == "wss";
            httpsRB.Checked = https;
            httpRB.Checked = !https;
            wssRB.Checked = wss;
            wsRB.Checked = !wss;

            textBoxProtocole.Text = _protocoleServer + "://";
            textBoxXSProtocole.Text = _wsProtocoleServer + "://";
            textDnsServer = textBoxDns.Text = ConfigUtils.ConfigData.DnsServer;
            textBoxDns2.Text = ConfigUtils.ConfigData.DnsServer + "/";
            textBoxDns3.Text = ConfigUtils.ConfigData.DnsServer + "/";
            textContextServer = textBoxContext.Text = _contextServer;
            textContext2Server = textBoxContext2.Text = _wscontextServer;

            unSelectAll();
        }
        private void UpdateMaj()
        {
            string _host_maj = ConfigUtils.ConfigData.Host_ftp;
            string _port_maj = ConfigUtils.ConfigData.Port_ftp;
            string _user_maj = ConfigUtils.ConfigData.Login;
            string _repertoire_maj = ConfigUtils.ConfigData.Usb_rep;
            string decryptedMdpFtp = Cryptography.Decrypt(ConfigUtils.ConfigData.Mdp, "sorecgamma123");
            textMdp = decryptedMdpFtp;
            if (decryptedMdpFtp != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('*', decryptedMdpFtp.Length);
                this.textBoxMdp.Text = sb.ToString();
            }
            string _repertoireFtp_maj = ConfigUtils.ConfigData.Source_rep;
            ftpProtocolCombobox.SelectedItem = ConfigUtils.ConfigData.Protocol_ftp.ToUpper();
            textBoxHote.Text = _host_maj;
            textBoxPort.Text = _port_maj;
            textBoxUtilisateur.Text = _user_maj;
            textBoxRepertoire.Text = _repertoire_maj;
            textBoxRepertoireFtp.Text = _repertoireFtp_maj;
        }

        private void refreshUpdateView(bool updateMajRes = false)
        {
            bool usbEnabled = TerminalMonitor.AreUsbPortsEnabled();
            updateUsbBtn.Enabled = usbEnabled;
            enableUsbBtn.Enabled = true;
            if (usbEnabled)
            {
                if (updateMajRes)
                    updateMajResultText("Veuillez insérer la clé de mise à jour (USB 3.0) et cliquez sur le bouton <Mise à jour>", Color.Green);
                enableUsbBtn.Text = "Fermer les ports";

            }
            else
            {
                updateMajResultText("", Color.Transparent);
                enableUsbBtn.Text = "Ouvrir les ports";
            }
        }
        private void write_caractere(string c)
        {
            switch (flag)
            {
                case 1:
                    textBoxDns.Text = textBoxDns2.Text = textBoxDns3.Text = textDnsServer += c;
                    textBoxDns2.Text = textBoxDns3.Text = textDnsServer + "/";
                    this.textBoxDns.Focus();
                    this.textBoxDns.SelectionStart = this.textDnsServer.Length;
                    break;
                case 2:
                    textBoxContext.Text = textContextServer += c;
                    this.textBoxContext.Focus();
                    this.textBoxContext.SelectionStart = this.textContextServer.Length;
                    break;
                case 11:
                    addIpCaract(ref textBox1, ref textip1, c);
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.textip1.Length;
                    break;
                case 12:
                    addIpCaract(ref textBox2, ref textip2, c);
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.textip2.Length;
                    break;
                case 13:
                    addIpCaract(ref textBox3, ref textip3, c);
                    this.textBox3.Focus();
                    this.textBox3.SelectionStart = this.textip3.Length;
                    break;
                case 14:
                    addIpCaract(ref textBox4, ref textip4, c);
                    this.textBox4.Focus();
                    this.textBox4.SelectionStart = this.textip4.Length;
                    break;
                case 15:
                    addIpCaract(ref textBox5, ref textMasque1, c);
                    this.textBox5.Focus();
                    this.textBox5.SelectionStart = this.textMasque1.Length;
                    break;
                case 16:
                    addIpCaract(ref textBox6, ref textMasque2, c);
                    this.textBox6.Focus();
                    this.textBox6.SelectionStart = this.textMasque2.Length;
                    break;
                case 17:
                    addIpCaract(ref textBox7, ref textMasque3, c);
                    this.textBox7.Focus();
                    this.textBox7.SelectionStart = this.textMasque3.Length;
                    break;
                case 18:
                    addIpCaract(ref textBox8, ref textMasque4, c);
                    this.textBox8.Focus();
                    this.textBox8.SelectionStart = this.textMasque4.Length;
                    break;
                case 19:
                    addIpCaract(ref textBox9, ref textPassr1, c);
                    this.textBox9.Focus();
                    this.textBox9.SelectionStart = this.textPassr1.Length;
                    break;
                case 20:
                    addIpCaract(ref textBox10, ref textPassr2, c);
                    this.textBox10.Focus();
                    this.textBox10.SelectionStart = this.textPassr2.Length;
                    break;
                case 21:
                    addIpCaract(ref textBox11, ref textPassr3, c);
                    this.textBox11.Focus();
                    this.textBox11.SelectionStart = this.textPassr3.Length;
                    break;
                case 22:
                    addIpCaract(ref textBox12, ref textPassr4, c);
                    this.textBox12.Focus();
                    this.textBox12.SelectionStart = this.textPassr4.Length;
                    break;
                case 23:
                    addIpCaract(ref textboxPing1, ref textPing1, c);
                    this.textboxPing1.Focus();
                    this.textboxPing1.SelectionStart = this.textPing1.Length;
                    break;
                case 24:
                    addIpCaract(ref textboxPing2, ref textPing2, c);
                    this.textboxPing2.Focus();
                    this.textboxPing2.SelectionStart = this.textPing2.Length;
                    break;
                case 25:
                    addIpCaract(ref textboxPing3, ref textPing3, c);
                    this.textboxPing3.Focus();
                    this.textboxPing3.SelectionStart = this.textPing3.Length;
                    break;
                case 26:
                    addIpCaract(ref textboxPing4, ref textPing4, c);
                    this.textboxPing4.Focus();
                    this.textboxPing4.SelectionStart = this.textPing4.Length;
                    break;
                case 27:
                    addIpCaract(ref dnsPref1, ref textDnsPref1, c);
                    this.dnsPref1.Focus();
                    this.dnsPref1.SelectionStart = this.textDnsPref1.Length;
                    break;
                case 28:
                    addIpCaract(ref dnsPref2, ref textDnsPref2, c);
                    this.dnsPref2.Focus();
                    this.dnsPref2.SelectionStart = this.textDnsPref2.Length;
                    break;
                case 29:
                    addIpCaract(ref dnsPref3, ref textDnsPref3, c);
                    this.dnsPref3.Focus();
                    this.dnsPref3.SelectionStart = this.textDnsPref3.Length;
                    break;
                case 30:
                    addIpCaract(ref dnsPref4, ref textDnsPref4, c);
                    this.dnsPref4.Focus();
                    this.dnsPref4.SelectionStart = this.textDnsPref4.Length;
                    break;
                case 31:
                    addIpCaract(ref dnsAlt1, ref textDnsAlt1, c);
                    this.dnsAlt1.Focus();
                    this.dnsAlt1.SelectionStart = this.textDnsAlt1.Length;
                    break;
                case 32:
                    addIpCaract(ref dnsAlt2, ref textDnsAlt2, c);
                    this.dnsAlt2.Focus();
                    this.dnsAlt2.SelectionStart = this.textDnsAlt2.Length;
                    break;
                case 33:
                    addIpCaract(ref dnsAlt3, ref textDnsAlt3, c);
                    this.dnsAlt3.Focus();
                    this.dnsAlt3.SelectionStart = this.textDnsAlt3.Length;
                    break;
                case 34:
                    addIpCaract(ref dnsAlt4, ref textDnsAlt4, c);
                    this.dnsAlt4.Focus();
                    this.dnsAlt4.SelectionStart = this.textDnsAlt4.Length;
                    break;
                case 40:
                    textBoxHote.Text += c;
                    this.textBoxHote.Focus();
                    this.textBoxHote.SelectionStart = this.textBoxHote.Text.Length;
                    break;
                case 41:
                    textBoxUtilisateur.Text += c;
                    this.textBoxUtilisateur.Focus();
                    this.textBoxUtilisateur.SelectionStart = this.textBoxUtilisateur.Text.Length;
                    break;
                case 42:
                    textBoxPort.Text += c;
                    this.textBoxPort.Focus();
                    this.textBoxPort.SelectionStart = this.textBoxPort.Text.Length;
                    break;
                case 43:
                    this.textMdp += c;
                    this.textBoxMdp.Text += "*";
                    this.textBoxMdp.Focus();
                    this.textBoxMdp.SelectionStart = this.textMdp.Length;
                    break;
                case 44:
                    textBoxRepertoire.Text += c;
                    this.textBoxRepertoire.Focus();
                    this.textBoxRepertoire.SelectionStart = this.textBoxRepertoire.Text.Length;
                    break;
                case 45:
                    textBoxVersion.Text += c;
                    this.textBoxVersion.Focus();
                    this.textBoxVersion.SelectionStart = this.textBoxVersion.Text.Length;
                    break;
                case 46:
                    this.textCle += c;
                    this.textBoxCle.Text += "*";
                    this.textBoxCle.Focus();
                    this.textBoxCle.SelectionStart = this.textCle.Length;
                    break;
                case 47:
                    textBoxRepertoireFtp.Text += c;
                    this.textBoxRepertoireFtp.Focus();
                    this.textBoxRepertoireFtp.SelectionStart = this.textBoxRepertoireFtp.Text.Length;
                    break;
                case 50:
                    textBoxContext2.Text = textContext2Server += c;
                    this.textBoxContext2.Focus();
                    this.textBoxContext2.SelectionStart = this.textContext2Server.Length;
                    break;
                default:
                    break;
            }
        }

        private void addIpCaract(ref TextBox tb, ref string text, string c)
        {
            if (tb != null && (text == null || text.Length < 3))
            {
                text += c;
                tb.Text = text;
            }
        }

        private void delete_caractere()
        {
            try
            {
                switch (flag)
                {
                    case 1:
                        if (this.textDnsServer.Length > 0)
                        {
                            this.textDnsServer = textDnsServer.Substring(0, textDnsServer.Length - 1);
                            this.textBoxDns.Text = textDnsServer;
                            this.textBoxDns2.Text = this.textBoxDns3.Text = textDnsServer + (textDnsServer.Length > 0 ? "/" : "");
                            this.textBoxDns.Focus();
                            this.textBoxDns.SelectionStart = this.textDnsServer.Length;
                        }
                        break;
                    case 2:
                        if (this.textContextServer.Length > 0)
                        {
                            this.textContextServer = textContextServer.Substring(0, textContextServer.Length - 1);
                            this.textBoxContext.Text = textContextServer;
                            this.textBoxContext.Focus();
                            this.textBoxContext.SelectionStart = this.textContextServer.Length;
                        }
                        break;
                    case 11:
                        if (this.textip1.Length > 0)
                        {
                            this.textip1 = textip1.Substring(0, textip1.Length - 1);
                        }
                        this.textBox1.Text = textip1;
                        this.textBox1.Focus();
                        this.textBox1.SelectionStart = this.textip1.Length;
                        break;
                    case 12:
                        if (this.textip2.Length > 0)
                        {
                            this.textip2 = textip2.Substring(0, textip2.Length - 1);
                        }
                        this.textBox2.Text = textip2;
                        this.textBox2.Focus();
                        this.textBox2.SelectionStart = this.textip2.Length;

                        break;
                    case 13:
                        if (this.textip3.Length > 0)
                        {
                            this.textip3 = textip3.Substring(0, textip3.Length - 1);

                        }
                        this.textBox3.Text = textip3;
                        this.textBox3.Focus();
                        this.textBox3.SelectionStart = this.textip3.Length;
                        break;
                    case 14:
                        if (this.textip4.Length > 0)
                        {
                            this.textip4 = textip4.Substring(0, textip4.Length - 1);
                        }
                        this.textBox4.Text = textip4;
                        this.textBox4.Focus();
                        this.textBox4.SelectionStart = this.textip4.Length;
                        break;
                    case 15:
                        if (this.textMasque1.Length > 0)
                        {
                            this.textMasque1 = textMasque1.Substring(0, textMasque1.Length - 1);
                        }
                        this.textBox5.Text = textMasque1;
                        this.textBox5.Focus();
                        this.textBox5.SelectionStart = this.textMasque1.Length;
                        break;
                    case 16:
                        if (this.textMasque2.Length > 0)
                        {
                            this.textMasque2 = textMasque2.Substring(0, textMasque2.Length - 1);
                        }
                        this.textBox6.Text = textMasque2;
                        this.textBox6.Focus();
                        this.textBox6.SelectionStart = this.textMasque2.Length;
                        break;
                    case 17:
                        if (this.textMasque3.Length > 0)
                        {
                            this.textMasque3 = textMasque3.Substring(0, textMasque3.Length - 1);
                        }
                        this.textBox7.Text = textMasque3;
                        this.textBox7.Focus();
                        this.textBox7.SelectionStart = this.textMasque3.Length;
                        break;
                    case 18:
                        if (this.textMasque4.Length > 0)
                        {
                            this.textMasque4 = textMasque4.Substring(0, textMasque4.Length - 1);
                        }
                        this.textBox8.Text = textMasque4;
                        this.textBox8.Focus();
                        this.textBox8.SelectionStart = this.textMasque4.Length;
                        break;
                    case 19:
                        if (this.textPassr1.Length > 0)
                        {
                            this.textPassr1 = textPassr1.Substring(0, textPassr1.Length - 1);
                        }
                        this.textBox9.Text = textPassr1;
                        this.textBox9.Focus();
                        this.textBox9.SelectionStart = this.textPassr1.Length;
                        break;
                    case 20:
                        if (this.textPassr2.Length > 0)
                        {
                            this.textPassr2 = textPassr2.Substring(0, textPassr2.Length - 1);
                        }
                        this.textBox10.Text = textPassr2;
                        this.textBox10.Focus();
                        this.textBox10.SelectionStart = this.textPassr2.Length;
                        break;
                    case 21:
                        if (this.textPassr3.Length > 0)
                        {
                            this.textPassr3 = textPassr3.Substring(0, textPassr3.Length - 1);
                        }
                        this.textBox11.Text = textPassr3;
                        this.textBox11.Focus();
                        this.textBox11.SelectionStart = this.textPassr3.Length;
                        break;
                    case 22:
                        if (this.textPassr4.Length > 0)
                        {
                            this.textPassr4 = textPassr4.Substring(0, textPassr4.Length - 1);
                        }
                        this.textBox12.Text = textPassr4;
                        this.textBox12.Focus();
                        this.textBox12.SelectionStart = this.textPassr4.Length;
                        break;
                    case 23:
                        if (this.textPing1.Length > 0)
                        {
                            this.textPing1 = textPing1.Substring(0, textPing1.Length - 1);
                        }
                        this.textboxPing1.Text = textPing1;
                        this.textboxPing1.Focus();
                        this.textboxPing1.SelectionStart = this.textPing1.Length;
                        break;
                    case 24:
                        if (this.textPing2.Length > 0)
                        {
                            this.textPing2 = textPing2.Substring(0, textPing2.Length - 1);
                        }
                        this.textboxPing2.Text = textPing2;
                        this.textboxPing2.Focus();
                        this.textboxPing2.SelectionStart = this.textPing2.Length;
                        break;
                    case 25:
                        if (this.textPing3.Length > 0)
                        {
                            this.textPing3 = textPing3.Substring(0, textPing3.Length - 1);
                        }
                        this.textboxPing3.Text = textPing3;
                        this.textboxPing3.Focus();
                        this.textboxPing3.SelectionStart = this.textPing3.Length;
                        break;
                    case 26:
                        if (this.textPing4.Length > 0)
                        {
                            this.textPing4 = textPing4.Substring(0, textPing4.Length - 1);
                        }
                        this.textboxPing4.Text = textPing4;
                        this.textboxPing4.Focus();
                        this.textboxPing4.SelectionStart = this.textPing4.Length;
                        break;

                    case 27:
                        if (this.textDnsPref1.Length > 0)
                        {
                            this.textDnsPref1 = textDnsPref1.Substring(0, textDnsPref1.Length - 1);
                        }
                        this.dnsPref1.Text = textDnsPref1;
                        this.dnsPref1.Focus();
                        this.dnsPref1.SelectionStart = this.textDnsPref1.Length;
                        break;
                    case 28:
                        if (this.textDnsPref2.Length > 0)
                        {
                            this.textDnsPref2 = textDnsPref2.Substring(0, textDnsPref2.Length - 1);
                        }
                        this.dnsPref2.Text = textDnsPref2;
                        this.dnsPref2.Focus();
                        this.dnsPref2.SelectionStart = this.textDnsPref2.Length;
                        break;
                    case 29:
                        if (this.textDnsPref3.Length > 0)
                        {
                            this.textDnsPref3 = textDnsPref3.Substring(0, textDnsPref3.Length - 1);
                        }
                        this.dnsPref3.Text = textDnsPref3;
                        this.dnsPref3.Focus();
                        this.dnsPref3.SelectionStart = this.textDnsPref3.Length;
                        break;
                    case 30:
                        if (this.textDnsPref4.Length > 0)
                        {
                            this.textDnsPref4 = textDnsPref4.Substring(0, textDnsPref4.Length - 1);
                        }
                        this.dnsPref4.Text = textDnsPref4;
                        this.dnsPref4.Focus();
                        this.dnsPref4.SelectionStart = this.textDnsPref4.Length;
                        break;

                    case 31:
                        if (this.textDnsAlt1.Length > 0)
                        {
                            this.textDnsAlt1 = textDnsAlt1.Substring(0, textDnsAlt1.Length - 1);
                        }
                        this.dnsAlt1.Text = textDnsAlt1;
                        this.dnsAlt1.Focus();
                        this.dnsAlt1.SelectionStart = this.textDnsAlt1.Length;
                        break;
                    case 32:
                        if (this.textDnsAlt2.Length > 0)
                        {
                            this.textDnsAlt2 = textDnsAlt2.Substring(0, textDnsAlt2.Length - 1);
                        }
                        this.dnsAlt2.Text = textDnsAlt2;
                        this.dnsAlt2.Focus();
                        this.dnsAlt2.SelectionStart = this.textDnsAlt2.Length;
                        break;
                    case 33:
                        if (this.textDnsAlt3.Length > 0)
                        {
                            this.textDnsAlt3 = textDnsAlt3.Substring(0, textDnsAlt3.Length - 1);
                        }
                        this.dnsAlt3.Text = textDnsAlt3;
                        this.dnsAlt3.Focus();
                        this.dnsAlt3.SelectionStart = this.textDnsAlt3.Length;
                        break;
                    case 34:
                        if (this.textDnsAlt4.Length > 0)
                        {
                            this.textDnsAlt4 = textDnsAlt4.Substring(0, textDnsAlt4.Length - 1);
                        }
                        this.dnsAlt4.Text = textDnsAlt4;
                        this.dnsAlt4.Focus();
                        this.dnsAlt4.SelectionStart = this.textDnsAlt4.Length;
                        break;
                    case 40:
                        if (textBoxHote.Text.Length > 0)
                        {
                            textBoxHote.Text = textBoxHote.Text.Substring(0, textBoxHote.Text.Length - 1);
                            this.textBoxHote.Focus();
                            this.textBoxHote.SelectionStart = this.textBoxHote.Text.Length;
                        }
                        break;
                    case 41:
                        if (textBoxUtilisateur.Text.Length > 0)
                        {
                            textBoxUtilisateur.Text = textBoxUtilisateur.Text.Substring(0, textBoxUtilisateur.Text.Length - 1);
                            this.textBoxUtilisateur.Focus();
                            this.textBoxUtilisateur.SelectionStart = this.textBoxUtilisateur.Text.Length;
                        }
                        break;
                    case 42:
                        if (textBoxPort.Text.Length > 0)
                        {
                            textBoxPort.Text = textBoxPort.Text.Substring(0, textBoxPort.Text.Length - 1);
                            this.textBoxPort.Focus();
                            this.textBoxPort.SelectionStart = this.textBoxPort.Text.Length;
                        }
                        break;
                    case 43:
                        if (textMdp.Length > 0)
                        {
                            textMdp = textMdp.Substring(0, textMdp.Length - 1);
                        }
                        if (textBoxMdp.Text.Length > 0)
                        {
                            textBoxMdp.Text = textBoxMdp.Text.Substring(0, textBoxMdp.Text.Length - 1);
                        }
                        this.textBoxMdp.Focus();
                        this.textBoxMdp.SelectionStart = this.textMdp.Length;
                        break;
                    case 44:
                        if (textBoxRepertoire.Text.Length > 0)
                        {
                            textBoxRepertoire.Text = textBoxRepertoire.Text.Substring(0, textBoxRepertoire.Text.Length - 1);
                            this.textBoxRepertoire.Focus();
                            this.textBoxRepertoire.SelectionStart = this.textBoxRepertoire.Text.Length;
                        }
                        break;
                    case 45:
                        if (textBoxVersion.Text.Length > 0)
                        {
                            textBoxVersion.Text = textBoxVersion.Text.Substring(0, textBoxVersion.Text.Length - 1);
                            this.textBoxVersion.Focus();
                            this.textBoxVersion.SelectionStart = this.textBoxVersion.Text.Length;
                        }
                        break;
                    case 46:
                        if (textCle.Length > 0)
                        {
                            textCle = textCle.Substring(0, textCle.Length - 1);
                        }
                        if (textBoxCle.Text.Length > 0)
                            textBoxCle.Text = textBoxCle.Text.Substring(0, textBoxCle.Text.Length - 1);
                        this.textBoxCle.Focus();
                        this.textBoxCle.SelectionStart = this.textBoxCle.Text.Length;
                        break;
                    case 47:
                        if (textBoxRepertoireFtp.Text.Length > 0)
                        {
                            textBoxRepertoireFtp.Text = textBoxRepertoireFtp.Text.Substring(0, textBoxRepertoireFtp.Text.Length - 1);
                            this.textBoxRepertoireFtp.Focus();
                            this.textBoxRepertoireFtp.SelectionStart = this.textBoxRepertoireFtp.Text.Length;
                        }
                        break;
                    case 50:
                        if (this.textContext2Server.Length > 0)
                        {
                            this.textContext2Server = textContext2Server.Substring(0, textContext2Server.Length - 1);
                            this.textBoxContext2.Text = textContext2Server;
                            this.textBoxContext2.Focus();
                            this.textBoxContext2.SelectionStart = this.textContext2Server.Length;
                        }
                        break;
                }
            }
            catch
            {

            }

        }

        private void buttonZ29_Click(object sender, EventArgs e)
        {
            write_caractere("1");
        }

        private void buttonZ30_Click(object sender, EventArgs e)
        {
            write_caractere("2");

        }

        private void buttonZ40_Click(object sender, EventArgs e)
        {
            write_caractere("3");

        }

        private void buttonZ41_Click(object sender, EventArgs e)
        {
            write_caractere("4");
        }

        private void buttonZ42_Click(object sender, EventArgs e)
        {
            write_caractere("5");
        }

        private void buttonZ43_Click(object sender, EventArgs e)
        {
            write_caractere("6");

        }

        private void buttonZ44_Click(object sender, EventArgs e)
        {
            write_caractere("7");

        }

        private void buttonZ45_Click(object sender, EventArgs e)
        {
            write_caractere("8");

        }

        private void buttonZ46_Click(object sender, EventArgs e)
        {
            write_caractere("9");

        }

        private void buttonZ48_Click(object sender, EventArgs e)
        {
            write_caractere("0");
        }

        private void buttonZ47_Click(object sender, EventArgs e)
        {
            delete_caractere();
        }

        private void buttonZ49_Click(object sender, EventArgs e)
        {

        }

        private void btnJournal_Click(object sender, EventArgs e)
        {
            unSelectAll();
            UpdateServer();
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            lblErrorMsg.Refresh();

            panelAutres.Hide(); 
            panelCaisse2.Hide();
            pingPanel.Hide();
            networkPanel.Hide();
            majPanel.Hide();
            panelKeyboard.Show();
            serverPanel.Show();
            this.btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            this.btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.flagPing = 0;
            this.flagConfig = 0;
            flagMaj = 0;
            this.flagServeur = 1;
            //   this.btnServeur.BackColor = Color.Gold;
            //  this.btnConfig.BackColor = Color.Navy;
        }

        private void btnPrepose_Click(object sender, EventArgs e)
        {
            unSelectAll();
            UpdateNetworkInfo();
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            lblErrorMsg.Refresh();
            if (!panelCaisse2.Visible)
                panelCaisse2.Show();
            panelKeyboard.Hide();
            serverPanel.Hide();
            pingPanel.Hide();
            majPanel.Hide();
            networkPanel.Show();
            panelAutres.Hide();

            this.btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            this.updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.flagPing = 0;
            flagMaj = 0;
            this.flagConfig = 1;
            this.flagServeur = 0;
            flag = 0;
        }

        private void UpdateNetworkInfo()
        {
            NetworkInterfaceModel EthernetInterface = NetworkInterfaceUtils.GetNetworkInterfaceInfoByType(NetworkInterfaceType.Ethernet);

            string[] addressIP = splitAddress(EthernetInterface.AddressIP);
            string[] mask = splitAddress(EthernetInterface.SubnetMask);
            string[] gateway = splitAddress(EthernetInterface.DefaultGateway);
            string[] dnsPref = splitAddress(EthernetInterface.DNSServer);
            string[] dnsAlt = splitAddress(EthernetInterface.AltDNSServer);

            if (addressIP != null && addressIP.Length > 3)
            {
                textBox1.Text = textip1 = addressIP[0];
                textBox2.Text = textip2 = addressIP[1];
                textBox3.Text = textip3 = addressIP[2];
                textBox4.Text = textip4 = addressIP[3];

                Ip.Text = textip1 + "." + textip2 + "." + textip3 + "." + textip4;
                if (!ApplicationContext.develop)
                {
                    ApplicationContext.IP = EthernetInterface.AddressIP;
                }
            }
            else
            {
                Ip.Text = "-";
                resetIP();
            }
            if (mask != null && mask.Length > 3)
            {
                textBox5.Text = textMasque1 = mask[0];
                textBox6.Text = textMasque2 = mask[1];
                textBox7.Text = textMasque3 = mask[2];
                textBox8.Text = textMasque4 = mask[3];

                Masque.Text = textMasque1 + "." + textMasque2 + "." + textMasque3 + "." + textMasque4;
            }
            else
            {
                Masque.Text = "-";
                resetMask();
            }

            if (gateway != null && gateway.Length > 3)
            {
                textBox9.Text = textPassr1 = gateway[0];
                textBox10.Text = textPassr2 = gateway[1];
                textBox11.Text = textPassr3 = gateway[2];
                textBox12.Text = textPassr4 = gateway[3];

                Passerelle.Text = textPassr1 + "." + textPassr2 + "." + textPassr3 + "." + textPassr4;
            }
            else
            {
                Passerelle.Text = "-";
                resetGateway();
            }
            if (dnsPref != null && dnsPref.Length > 3)
            {
                dnsPref1.Text = textDnsPref1 = dnsPref[0];
                dnsPref2.Text = textDnsPref2 = dnsPref[1];
                dnsPref3.Text = textDnsPref3 = dnsPref[2];
                dnsPref4.Text = textDnsPref4 = dnsPref[3];
                prefDns.Text = textDnsPref1 + "." + textDnsPref2 + "." + textDnsPref3 + "." + textDnsPref4;
            }
            else
            {
                prefDns.Text = "-";
                resetDnsPref();
            }
            if (dnsAlt != null && dnsAlt.Length > 3)
            {
                dnsAlt1.Text = textDnsAlt1 = dnsAlt[0];
                dnsAlt2.Text = textDnsAlt2 = dnsAlt[1];
                dnsAlt3.Text = textDnsAlt3 = dnsAlt[2];
                dnsAlt4.Text = textDnsAlt4 = dnsAlt[3];

                altDns.Text = textDnsAlt1 + "." + textDnsAlt2 + "." + textDnsAlt3 + "." + textDnsAlt4;
            }
            else
            {
                altDns.Text = "-";
                resetDnsAlt();
            }

            isDhcp = EthernetInterface.DHCPEnabled;
            checkBox2.Checked = !EthernetInterface.DHCPEnabled;
            checkBox3.Checked = EthernetInterface.DHCPEnabled;

            textBox1.Enabled = !EthernetInterface.DHCPEnabled;
            textBox2.Enabled = !EthernetInterface.DHCPEnabled;
            textBox3.Enabled = !EthernetInterface.DHCPEnabled;
            textBox4.Enabled = !EthernetInterface.DHCPEnabled;

            textBox5.Enabled = !EthernetInterface.DHCPEnabled;
            textBox6.Enabled = !EthernetInterface.DHCPEnabled;
            textBox7.Enabled = !EthernetInterface.DHCPEnabled;
            textBox8.Enabled = !EthernetInterface.DHCPEnabled;

            textBox9.Enabled = !EthernetInterface.DHCPEnabled;
            textBox10.Enabled = !EthernetInterface.DHCPEnabled;
            textBox11.Enabled = !EthernetInterface.DHCPEnabled;
            textBox12.Enabled = !EthernetInterface.DHCPEnabled;

            autoDns.Enabled = EthernetInterface.DHCPEnabled;

            dnsPref1.Enabled = !autoDns.Checked;
            dnsPref3.Enabled = !autoDns.Checked;
            dnsPref2.Enabled = !autoDns.Checked;
            dnsPref4.Enabled = !autoDns.Checked;

            dnsAlt1.Enabled = !autoDns.Checked;
            dnsAlt2.Enabled = !autoDns.Checked;
            dnsAlt3.Enabled = !autoDns.Checked;
            dnsAlt4.Enabled = !autoDns.Checked;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchAuthenticationForm("");
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            flag = 9;
        }

        private void textBoxP2_Click(object sender, EventArgs e)
        {
            flag = 10;

        }

        private void dnsPref1_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 27;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.Navy;
            dnsPref1.ForeColor = Color.White;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void dnsPref2_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 28;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.Navy;
            dnsPref2.ForeColor = Color.White;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void dnsPref3_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 29;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.Navy;
            dnsPref3.ForeColor = Color.White;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }


        private void dnsPref4_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 30;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.Navy;
            dnsPref4.ForeColor = Color.White;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }


        private void dnsAlt1_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 31;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.Navy;
            dnsAlt1.ForeColor = Color.White;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void dnsAlt2_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 32;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.Navy;
            dnsAlt2.ForeColor = Color.White;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void dnsAlt3_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 33;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.Navy;
            dnsAlt3.ForeColor = Color.White;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }
        private void dnsAlt4_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 34;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.Navy;
            dnsAlt4.ForeColor = Color.White;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 12;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.Navy;
            textBox2.ForeColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 13;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.Navy;
            textBox3.ForeColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 14;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.Navy;
            textBox4.ForeColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 15;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.Navy;
            textBox5.ForeColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 16;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.Navy;
            textBox6.ForeColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 17;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.Navy;
            textBox7.ForeColor = Color.White;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox8_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 18;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.Navy;
            textBox8.ForeColor = Color.White;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox9_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 19;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.Navy;
            textBox9.ForeColor = Color.White;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox10_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 20;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.Navy;
            textBox10.ForeColor = Color.White;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }


        private void textBox12_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 22;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.Navy;
            textBox12.ForeColor = Color.White;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox11_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 21;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.Navy;
            textBox11.ForeColor = Color.White;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 11;
            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;
            dnsAlt4.BackColor = Color.White;
            dnsAlt4.ForeColor = Color.Black;
        }

        private void textBoxDns_Click(object sender, EventArgs e)
        {
            flag = 1;
            textBoxContext.BackColor = Color.White;
            textBoxContext.ForeColor = Color.Black;
            textBoxContext2.BackColor = Color.White;
            textBoxContext2.ForeColor = Color.Black;
            textBoxDns.BackColor = Color.Navy;
            textBoxDns.ForeColor = Color.White;
        }
        private void textBoxContextServer_Click(object sender, EventArgs e)
        {
            flag = 2;
            textBoxDns.BackColor = Color.White;
            textBoxDns.ForeColor = Color.Black;
            textBoxContext2.BackColor = Color.White;
            textBoxContext2.ForeColor = Color.Black;
            textBoxContext.BackColor = Color.Navy;
            textBoxContext.ForeColor = Color.White;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            checkBox2.Checked = true;
            if (!checkBox3.Checked)
                return;
            isDhcp = false;
            unSelectAll();
            autoDns.Checked = false;
            autoDns.Enabled = false;
            checkBox3.Checked = false;
            isDhcp = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;

            dnsAlt1.Enabled = true;
            dnsAlt2.Enabled = true;
            dnsAlt3.Enabled = true;
            dnsAlt4.Enabled = true;

            dnsPref1.Enabled = true;
            dnsPref3.Enabled = true;
            dnsPref2.Enabled = true;
            dnsPref4.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            checkBox3.Checked = true;
            if (!checkBox2.Checked)
                return;
            isDhcp = true;
            autoDns.Enabled = true;
            checkBox2.Checked = false;

            textBox1.Text = textip1 = "";
            textBox2.Text = textip2 = "";
            textBox3.Text = textip3 = "";
            textBox4.Text = textip4 = "";

            textBox5.Text = textMasque1 = "";
            textBox6.Text = textMasque2 = "";
            textBox7.Text = textMasque3 = "";
            textBox8.Text = textMasque4 = "";

            textBox9.Text = textPassr1 = "";
            textBox10.Text = textPassr2 = "";
            textBox11.Text = textPassr3 = "";
            textBox12.Text = textPassr4 = "";

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            unSelectAll();
        }


        private string[] splitAddress(string address)
        {
            if (address == null)
                return null;
            char[] delimiters = { '.' };
            return address.Split(delimiters);
        }

        private void _dateTimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private string GetPingIP()
        {
            string pingIp = textPing1 + "." + textPing2 + "." + textPing3 + "." + textPing4;
            return ValidateIPv4(pingIp) ? pingIp : null;
        }

        private string GetMask()
        {
            string masque = textMasque1 + "." + textMasque2 + "." + textMasque3 + "." + textMasque4;
            return ValidateIPv4(masque) ? masque : null;
        }
        private string GetGateway()
        {
            string passerelle = textPassr1 + "." + textPassr2 + "." + textPassr3 + "." + textPassr4;
            return ValidateIPv4(passerelle) ? passerelle : null;
        }
        private string GetIP()
        {
            string ip = textip1 + "." + textip2 + "." + textip3 + "." + textip4;
            return ValidateIPv4(ip) ? ip : null;
        }
        private string GetAltDns()
        {
            string dnsAlt = textDnsAlt1 + "." + textDnsAlt2 + "." + textDnsAlt3 + "." + textDnsAlt4;
            return ValidateIPv4(dnsAlt) ? dnsAlt : null;
        }

        private string GetPrefDns()
        {
            string dnsPref = textDnsPref1 + "." + textDnsPref2 + "." + textDnsPref3 + "." + textDnsPref4;
            return ValidateIPv4(dnsPref) ? dnsPref : null;
        }

        public static bool ValidateIPv4(string ipString)
        {
            IPAddress address;
            return IPAddress.TryParse(ipString, out address);
        }

        private void buttonZ3_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "Traitement en cours...";
            lblErrorMsg.BackColor = Color.FromArgb(76, 127, 228);
            bool succeed = true;
            lblErrorMsg.Refresh();
            if (flagConfig == 1)
            {
                NetworkInterfaceModel ethernetInterface = NetworkInterfaceUtils.GetNetworkInterfaceInfoByType(NetworkInterfaceType.Ethernet);
                NetworkInterfaceModel ethernetInterfaceConfig = new NetworkInterfaceModel();
                ExecCommandResult result = NetworkInterfaceUtils.DeleteAllDNSServers(ethernetInterface.Name);
                if (!autoDns.Checked)
                {
                    string dnsPref = GetPrefDns();
                    if (dnsPref != null)
                    {
                        result = NetworkInterfaceUtils.SetStaticDNS(ethernetInterface.Name, dnsPref);
                        succeed = result.Succeed;
                        if (!result.Succeed)
                        {
                            string error;
                            if (result.ErrorMessage != null && result.ErrorMessage.Length > 80)
                                error = result.ErrorMessage.Substring(0, 80) + "...";
                            else
                                error = result.ErrorMessage;
                            lblErrorMsg.Text = "DNS préféré: " + error;
                            lblErrorMsg.BackColor = Color.Red;
                        }
                    }

                    string dnsAlt = GetAltDns();
                    if (succeed && dnsAlt != null)
                    {
                        result = NetworkInterfaceUtils.AddAltDNS(ethernetInterface.Name, dnsAlt);
                        succeed = result.Succeed;
                        if (!result.Succeed)
                        {
                            string error;
                            if (result.ErrorMessage != null && result.ErrorMessage.Length > 80)
                                error = result.ErrorMessage.Substring(0, 80) + "...";
                            else
                                error = result.ErrorMessage;
                            lblErrorMsg.Text = "DNS auxiliaire: " + error;
                            lblErrorMsg.BackColor = Color.Red;
                        }
                    }
                }
                else if (succeed && autoDns.Checked)
                {
                    result = NetworkInterfaceUtils.SetDNSProvidedByDHCP(ethernetInterface.Name);
                    succeed = result.Succeed;
                    if (!result.Succeed)
                    {
                        string error;
                        if (result.ErrorMessage != null && result.ErrorMessage.Length > 80)
                            error = result.ErrorMessage.Substring(0, 80) + "...";
                        else
                            error = result.ErrorMessage;
                        lblErrorMsg.Text = "DHCP DNS: " + error;
                        lblErrorMsg.BackColor = Color.Red;
                    }
                }
                if (succeed && isDhcp)
                {
                    ethernetInterfaceConfig.DHCPEnabled = true;
                    ethernetInterfaceConfig.Name = ethernetInterface.Name;
                    try
                    {

                        result = NetworkInterfaceUtils.SetAddressIP(ethernetInterfaceConfig);
                        succeed = result.Succeed;
                        if (!result.Succeed)
                        {
                            string error;
                            if (result.ErrorMessage != null && result.ErrorMessage.Length > 80)
                                error = result.ErrorMessage.Substring(0, 80) + "...";
                            else
                                error = result.ErrorMessage;
                            lblErrorMsg.Text = "DHCP: " + error;
                            lblErrorMsg.BackColor = Color.Red;
                        }
                    }
                    catch
                    {
                        lblErrorMsg.Text = "Erreur de configuration. Merci de réessayer. ";
                        lblErrorMsg.BackColor = Color.Red;
                        succeed = false;
                    }

                }
                else if (succeed)
                {
                    string ip = GetIP();
                    string masque = GetMask();
                    string passerelle = GetGateway();

                    if (ip != null && masque != null && passerelle != null)
                    {
                        ethernetInterfaceConfig.DHCPEnabled = false;
                        ethernetInterfaceConfig.AddressIP = ip;
                        ethernetInterfaceConfig.SubnetMask = masque;
                        ethernetInterfaceConfig.DefaultGateway = passerelle;
                        ethernetInterfaceConfig.Name = ethernetInterface.Name;

                        try
                        {
                            result = NetworkInterfaceUtils.SetAddressIP(ethernetInterfaceConfig);
                            succeed = result.Succeed;
                            if (!result.Succeed)
                            {
                                string error;
                                if (result.ErrorMessage != null && result.ErrorMessage.Length > 80)
                                    error = result.ErrorMessage.Substring(0, 80) + "...";
                                else
                                    error = result.ErrorMessage;
                                lblErrorMsg.Text = "Statique IP: " + error;
                                lblErrorMsg.BackColor = Color.Red;
                            }
                        }
                        catch
                        {
                            lblErrorMsg.Text = "Échec de la configuration du réseau. Merci de réessayer.";
                            lblErrorMsg.BackColor = Color.Red;
                            succeed = false;
                        }

                    }
                    else
                    {
                        lblErrorMsg.Text = "Merci de remplir tous les champs obligatoires (Adresse, Masque et Passerelle).";
                        lblErrorMsg.BackColor = Color.Red;
                        succeed = false;
                    }
                }
                if (succeed)
                {
                    TerminalUtils.WaitForTraitement(4);
                    lblErrorMsg.Text = "Réseau configuré avec succès.";
                    lblErrorMsg.BackColor = Color.Green;
                    if (ethernetInterface.AddressIP != ethernetInterfaceConfig.AddressIP)
                    {
                        TerminalUtils.updateLastTicketInfos(null);
                        TerminalUtils.updateLastVoucherInfos(null);
                    }
                }
                UpdateNetworkInfo();
            }
            else if (flagServeur == 1)
            {
                if (!string.IsNullOrEmpty(textDnsServer)
                    && !string.IsNullOrEmpty(textContextServer)
                    && !string.IsNullOrEmpty(textContext2Server))
                {
                    ConfigUtils.ConfigData.ProtocolServer = httpsRB.Checked ? "https" : "http";
                    ConfigUtils.ConfigData.WsProtocol = wssRB.Checked ? "wss" : "ws";
                    ConfigUtils.ConfigData.DnsServer = textDnsServer;
                    ConfigUtils.ConfigData.ContextServer = textContextServer;
                    ConfigUtils.ConfigData.WsContext = textContext2Server;
                    lblErrorMsg.Text = "Serveur configuré avec succès.";
                    lblErrorMsg.BackColor = Color.Green;
                }
                else
                {
                    lblErrorMsg.Text = "Merci de saisir tous les champs.";
                    lblErrorMsg.BackColor = Color.Red;
                    succeed = false;
                }
            }
            else if (flagMaj == 1)
            {
                if (!string.IsNullOrEmpty(textBoxHote.Text)
                    && !string.IsNullOrEmpty(textBoxUtilisateur.Text)
                    && !string.IsNullOrEmpty(textBoxPort.Text)
                    && !string.IsNullOrEmpty(textMdp)
                    && !string.IsNullOrEmpty(textBoxRepertoire.Text)
                    && !string.IsNullOrEmpty(textBoxRepertoireFtp.Text)
                    && !string.IsNullOrEmpty(ftpProtocolCombobox.Text))
                {
                    string cryptedMdpFtp = Cryptography.Encrypt(textMdp, "sorecgamma123");
                    ConfigUtils.ConfigData.Host_ftp = textBoxHote.Text;
                    ConfigUtils.ConfigData.Login = textBoxUtilisateur.Text;
                    ConfigUtils.ConfigData.Port_ftp = textBoxPort.Text;
                    ConfigUtils.ConfigData.Usb_rep = textBoxRepertoire.Text;
                    ConfigUtils.ConfigData.Mdp = cryptedMdpFtp;
                    ConfigUtils.ConfigData.Source_rep = textBoxRepertoireFtp.Text;
                    ConfigUtils.ConfigData.Protocol_ftp = ftpProtocolCombobox.Text.ToLower();
                    updateMajResultText("Enregistrement effectuée avec succès", Color.Green);
                }
                else
                {
                    updateMajResultText("Merci de saisir tous les champs.", Color.Red);
                    succeed = false;
                }
            }
            else if (flagPing == 1)
            {
                label30.Text = "";
                string host = GetPingIP();
                if (host == null)
                {
                    lblErrorMsg.Text = "Merci de saisir une adresse IP valide.";
                    lblErrorMsg.BackColor = Color.Red;
                }
                else
                {
                    try
                    {
                        bool pingeable = NetworkInterfaceUtils.PingHost(host);
                        label30.Text = "Ping";
                        if (pingeable)
                        {
                            label30.ForeColor = Color.Green;
                        }
                        else
                        {
                            label30.ForeColor = Color.Red;
                        }
                    }
                    catch
                    {
                        label30.Text = "Ping";
                        label30.ForeColor = Color.Red;
                    }
                    finally
                    {
                        lblErrorMsg.Text = "";
                        lblErrorMsg.BackColor = Color.Transparent;
                    }
                }

            }
            lblErrorMsg.Refresh();
            if (succeed)
            {
                ConfigUtils.createOrUpdateConfigFile(ConfigUtils.ConfigData);
                UpdateGuichetInfo();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textPing1 = textPing2 = textPing3 = textPing4 = "";
            textboxPing1.Text = "";
            textboxPing2.Text = "";
            textboxPing3.Text = "";
            textboxPing4.Text = "";
            unSelectAll();

            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            lblErrorMsg.Refresh();
            if (!panelCaisse2.Visible)
                panelCaisse2.Show();
            panelKeyboard.Hide();
            serverPanel.Hide();
            networkPanel.Hide();
            majPanel.Hide();
            panelAutres.Hide();
            pingPanel.Show();
            this.btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            this.updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.flagPing = 1;
            this.flagConfig = 0;
            this.flagServeur = 0;
            flagMaj = 0;
            label30.Text = "";
            flag = 0;
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPing1_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 23;
            textboxPing1.BackColor = Color.Navy;
            textboxPing1.ForeColor = Color.White;
            textboxPing2.BackColor = Color.White;
            textboxPing2.ForeColor = Color.Black;
            textboxPing3.BackColor = Color.White;
            textboxPing3.ForeColor = Color.Black;
            textboxPing4.BackColor = Color.White;
            textboxPing4.ForeColor = Color.Black;
        }

        private void textPing2_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 24;
            textboxPing1.BackColor = Color.White;
            textboxPing1.ForeColor = Color.Black;
            textboxPing2.BackColor = Color.Navy;
            textboxPing2.ForeColor = Color.White;
            textboxPing3.BackColor = Color.White;
            textboxPing3.ForeColor = Color.Black;
            textboxPing4.BackColor = Color.White;
            textboxPing4.ForeColor = Color.Black;
        }

        private void textPing3_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 25;
            textboxPing1.BackColor = Color.White;
            textboxPing1.ForeColor = Color.Black;
            textboxPing2.BackColor = Color.White;
            textboxPing2.ForeColor = Color.Black;
            textboxPing3.BackColor = Color.Navy;
            textboxPing3.ForeColor = Color.White;
            textboxPing4.BackColor = Color.White;
            textboxPing4.ForeColor = Color.Black;
        }

        private void textPing4_MouseClick(object sender, MouseEventArgs e)
        {
            flag = 26;
            textboxPing1.BackColor = Color.White;
            textboxPing1.ForeColor = Color.Black;
            textboxPing2.BackColor = Color.White;
            textboxPing2.ForeColor = Color.Black;
            textboxPing3.BackColor = Color.White;
            textboxPing3.ForeColor = Color.Black;
            textboxPing4.BackColor = Color.Navy;
            textboxPing4.ForeColor = Color.White;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            unSelectAll();
            unselectAllMaj();
            lblErrorMsg.Text = "";
            lblErrorMsg.BackColor = Color.Transparent;
            lblErrorMsg.Refresh();

            panelCaisse2.Hide();
            panelKeyboard.Show();
            serverPanel.Hide();
            networkPanel.Hide();
            pingPanel.Hide();
            majPanel.Show();
            panelAutres.Hide();
            flag = 0;
            refreshUpdateView();

            this.updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            this.btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            this.flagMaj = 1;
            flagServeur = 0;
            flagPing = 0;
            flagConfig = 0;
            updateMajResultText("", Color.Transparent);
            UpdateMaj();
            textBoxCle.Text = "";
            textBoxVersion.Text = "";

        }

        private void enableUsbBtn_Click(object sender, EventArgs e)
        {
            enableUsbBtn.Enabled = false;
            bool usbEnabled = TerminalMonitor.AreUsbPortsEnabled();
            updateMajResultText("Traitement en cours...", Color.Tomato);

            if (!TerminalMonitor.EnableUsbPorts(!usbEnabled))
            {
                updateMajResultText("Opération echouée. Merci de contacter l'administration.", Color.Red);
            }
            else if (!usbEnabled)
            {
                updateMajResultText("Veuillez insérer la clé de mise à jour (USB 3.0) et cliquez sur le bouton <Mise à jour>", Color.Green);
            }
            else
            {
                updateMajResultText("", Color.Transparent);
            }

            refreshUpdateView();

        }

        private void updateMajResultText(string msg, Color color)
        {
            maj_result.BackColor = color;
            maj_result.Text = msg;
            maj_result.Refresh();

        }
        private void updateUsbBtn_Click(object sender, EventArgs e)
        {
            if (!textBoxRepertoire.Text.Equals("") && !textBoxVersion.Text.Equals("") && !textBoxCle.Text.Equals(""))
            {
                updateMajResultText("Traitement en cours...", Color.Tomato);
                updateUsbBtn.Enabled = false;
                string usbRepertoire = textBoxRepertoire.Text;
                string version = textBoxVersion.Text;
                string usbMajFilepath = usbRepertoire + "T2020_" + version + ".zip";
                string usbMajChecksumFilepath = usbRepertoire + "T2020_" + version + "_CHECKSUM.txt";
                TerminalUtils.WaitForTraitement(4);
                if (File.Exists(usbMajFilepath) && File.Exists(usbMajChecksumFilepath))
                {
                    string checksumFile = ChecksumUtils.GetChecksum(HashingAlgoTypes.MD5, usbMajFilepath);

                    string checksumFileCrypted = Cryptography.Encrypt(checksumFile, textCle);
                    string majFileChecksumCrypted = FileUtils.ReadFileContent(usbMajChecksumFilepath);
                    if (majFileChecksumCrypted == null)
                    {
                        updateMajResultText("Le contenu du fichier checksum est vide. Veuillez réessayer ou contacter l'administration.", Color.Red);
                    }
                    else if (checksumFile == null)
                    {
                        updateMajResultText("Une erreur s'est produite lors de calcul de checksum. Veuillez réessayer ou contacter l'administration.", Color.Red);
                    }
                    else if (majFileChecksumCrypted != checksumFileCrypted)
                    {
                        updateMajResultText("La clé est incorrecte ou les fichies de mise à jour sont endommagés.\nVeuillez réessayer ou contacter l'administration.", Color.Red);
                        ApplicationContext.Logger.Error(string.Format("Les fichies de mise à jour sont endommagés: MAJ CHECKSUM {0}, CALCULATED FILE CHECKSUM: {1}, Crypted {2}", majFileChecksumCrypted, checksumFile, checksumFileCrypted));
                    }
                    else
                    {
                        string datePrevu = DateTime.Now.ToString("yyyyMMdd");

                        string destination = @"D:\GAMMA\\MAJ\" + datePrevu;
                        if (Directory.Exists(destination))
                        {
                            Directory.Delete(destination, true);
                        }
                        Directory.CreateDirectory(destination);
                        if (FileUtils.CopyFile(usbMajFilepath, "D:\\GAMMA\\MAJ\\" + datePrevu + "\\T2020_" + version + ".zip", true))
                        {
                            updateMajResultText("Traitement terminé avec succès. Le terminal redémarrera dans quelques instants...", Color.Green);
                            TerminalMonitor.EnableUsbPorts(false);
                            TerminalUtils.WaitForTraitement(2);
                            if (!ApplicationContext.develop) TerminalMonitor.Reboot();
                        }
                        else
                        {
                            string msg = "Erreur lors de déplacement des fichiers de mise à jour vers: " + destination + ".\nVeuillez contacter l'administration!";
                            updateMajResultText(msg, Color.Red);
                            ApplicationContext.Logger.Error(msg);
                        }
                    }
                }
                else
                {
                    string msg = "Les fichiers de mise à jour non trouvés: \n" + usbMajFilepath + "\n" + usbMajChecksumFilepath + ".\nVeuillez vérifier la clé USB!";
                    updateMajResultText(msg, Color.Red);
                    ApplicationContext.Logger.Error(msg);
                }
                refreshUpdateView();
            }
            else
            {
                updateMajResultText("Merci de saisir tous les champs.", Color.Red);
            }
        }

        private void resetDnsPref()
        {
            dnsPref1.Text = textDnsPref1 = "";
            dnsPref2.Text = textDnsPref2 = "";
            dnsPref3.Text = textDnsPref3 = "";
            dnsPref4.Text = textDnsPref4 = "";
        }
        private void resetIP()
        {
            textBox1.Text = textip1 = "";
            textBox2.Text = textip2 = "";
            textBox3.Text = textip3 = "";
            textBox4.Text = textip4 = "";
        }

        private void resetMask()
        {
            textBox5.Text = textMasque1 = "";
            textBox6.Text = textMasque2 = "";
            textBox7.Text = textMasque3 = "";
            textBox8.Text = textMasque4 = "";
        }
        private void resetGateway()
        {
            textBox9.Text = textPassr1 = "";
            textBox10.Text = textPassr2 = "";
            textBox11.Text = textPassr3 = "";
            textBox12.Text = textPassr4 = "";
        }
        private void resetDnsAlt()
        {
            dnsAlt1.Text = textDnsAlt1 = "";
            dnsAlt2.Text = textDnsAlt2 = "";
            dnsAlt3.Text = textDnsAlt3 = "";
            dnsAlt4.Text = textDnsAlt4 = "";
        }
        private void autoDns_CheckedChanged(object sender, EventArgs e)
        {
            dnsAlt1.Enabled = !autoDns.Checked;
            dnsAlt2.Enabled = !autoDns.Checked;
            dnsAlt3.Enabled = !autoDns.Checked;
            dnsAlt4.Enabled = !autoDns.Checked;
            dnsPref1.Enabled = !autoDns.Checked;
            dnsPref2.Enabled = !autoDns.Checked;
            dnsPref3.Enabled = !autoDns.Checked;
            dnsPref4.Enabled = !autoDns.Checked;

            if (autoDns.Checked)
            {
                dnsPref1.Text = textDnsPref1 = "";
                dnsPref2.Text = textDnsPref2 = "";
                dnsPref3.Text = textDnsPref3 = "";
                dnsPref4.Text = textDnsPref4 = "";
                dnsAlt1.Text = textDnsAlt1 = "";
                dnsAlt2.Text = textDnsAlt2 = "";
                dnsAlt3.Text = textDnsAlt3 = "";
                dnsAlt4.Text = textDnsAlt4 = "";
                unSelectAll();
            }
        }

        private void unSelectAll()
        {
            flag = 0;
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            textBox5.BackColor = Color.White;
            textBox5.ForeColor = Color.Black;
            textBox6.BackColor = Color.White;
            textBox6.ForeColor = Color.Black;
            textBox7.BackColor = Color.White;
            textBox7.ForeColor = Color.Black;
            textBox8.BackColor = Color.White;
            textBox8.ForeColor = Color.Black;
            textBox9.BackColor = Color.White;
            textBox9.ForeColor = Color.Black;
            textBox10.BackColor = Color.White;
            textBox10.ForeColor = Color.Black;
            textBox11.BackColor = Color.White;
            textBox11.ForeColor = Color.Black;
            textBox12.BackColor = Color.White;
            textBox12.ForeColor = Color.Black;

            dnsPref1.BackColor = Color.White;
            dnsPref1.ForeColor = Color.Black;
            dnsPref2.BackColor = Color.White;
            dnsPref2.ForeColor = Color.Black;
            dnsPref3.BackColor = Color.White;
            dnsPref3.ForeColor = Color.Black;
            dnsPref4.BackColor = Color.White;
            dnsPref4.ForeColor = Color.Black;

            dnsAlt1.BackColor = Color.White;
            dnsAlt1.ForeColor = Color.Black;
            dnsAlt2.BackColor = Color.White;
            dnsAlt2.ForeColor = Color.Black;
            dnsAlt3.BackColor = Color.White;
            dnsAlt3.ForeColor = Color.Black;

            textBoxProtocole.BackColor = Color.White;
            textBoxProtocole.ForeColor = Color.Black;
            textBoxDns.BackColor = Color.White;
            textBoxDns.ForeColor = Color.Black;
            textBoxXSProtocole.BackColor = Color.White;
            textBoxXSProtocole.ForeColor = Color.Black;

            textBoxDns2.BackColor = Color.White;
            textBoxDns2.ForeColor = Color.Black;
            textBoxDns3.BackColor = Color.White;
            textBoxDns3.ForeColor = Color.Black;
            textBoxContext.BackColor = Color.White;
            textBoxContext.ForeColor = Color.Black;
            textBoxContext2.BackColor = Color.White;
            textBoxContext2.ForeColor = Color.Black;

            textboxPing1.BackColor = Color.White;
            textboxPing1.ForeColor = Color.Black;
            textboxPing2.BackColor = Color.White;
            textboxPing2.ForeColor = Color.Black;
            textboxPing3.BackColor = Color.White;
            textboxPing3.ForeColor = Color.Black;
            textboxPing4.BackColor = Color.White;
            textboxPing4.ForeColor = Color.Black;
        }

        private void buttonZ2_Click_2(object sender, EventArgs e)
        {
            lblErrorMsg.Text = "";
            lblErrorMsg.ForeColor = Color.White;
            lblErrorMsg.BackColor = Color.Transparent;
            maj_result.Text = "";
            maj_result.ForeColor = Color.White;
            maj_result.BackColor = Color.Transparent;

            if (flagServeur == 1)
                UpdateServer();
            if (flagConfig == 1)
                UpdateNetworkInfo();
            if (flagPing == 1)
            {
                textPing1 = textPing2 = textPing3 = textPing4 = "";
                textboxPing1.Text = "";
                textboxPing2.Text = "";
                textboxPing3.Text = "";
                textboxPing4.Text = "";
                lblErrorMsg.Text = "";
                lblErrorMsg.BackColor = Color.Transparent;
            }
            unSelectAll();
        }

        private void httpRB_CheckedChanged(object sender, EventArgs e)
        {
            //TerminalUtils.updateNodeValue("protocolServer", "http");
            httpsRB.Checked = !httpRB.Checked;
            textBoxProtocole.Text = "http://";
        }

        private void httpsRB_CheckedChanged(object sender, EventArgs e)
        {
            //TerminalUtils.updateNodeValue("protocolServer", "https");
            httpRB.Checked = !httpsRB.Checked;
            textBoxProtocole.Text = "https://";
        }

        private void buttonZ82_Click_1(object sender, EventArgs e)
        {
            write_caractere("a");
        }

        private void buttonZ84_Click(object sender, EventArgs e)
        {
            write_caractere("z");
        }

        private void buttonZ86_Click(object sender, EventArgs e)
        {
            write_caractere("e");
        }

        private void buttonZ73_Click(object sender, EventArgs e)
        {
            write_caractere("r");
        }

        private void buttonZ75_Click(object sender, EventArgs e)
        {
            write_caractere("t");
        }

        private void buttonZ77_Click(object sender, EventArgs e)
        {
            write_caractere("y");
        }

        private void buttonZ64_Click(object sender, EventArgs e)
        {
            write_caractere("u");
        }

        private void buttonZ66_Click(object sender, EventArgs e)
        {
            write_caractere("i");
        }

        private void buttonZ68_Click(object sender, EventArgs e)
        {
            write_caractere("o");
        }

        private void buttonZ61_Click(object sender, EventArgs e)
        {
            write_caractere("p");
        }

        private void buttonZ85_Click(object sender, EventArgs e)
        {
            write_caractere("q");
        }

        private void buttonZ83_Click(object sender, EventArgs e)
        {
            write_caractere("s");
        }

        private void buttonZ81_Click(object sender, EventArgs e)
        {
            write_caractere("d");
        }

        private void buttonZ76_Click(object sender, EventArgs e)
        {
            write_caractere("f");
        }

        private void buttonZ74_Click(object sender, EventArgs e)
        {
            write_caractere("g");
        }

        private void buttonZ72_Click(object sender, EventArgs e)
        {
            write_caractere("h");
        }

        private void buttonZ67_Click(object sender, EventArgs e)
        {
            write_caractere("j");
        }

        private void buttonZ65_Click(object sender, EventArgs e)
        {
            write_caractere("k");
        }

        private void buttonZ63_Click(object sender, EventArgs e)
        {
            write_caractere("l");
        }

        private void buttonZ62_Click(object sender, EventArgs e)
        {
            write_caractere("m");
        }

        private void buttonZ80_Click(object sender, EventArgs e)
        {
            write_caractere("w");
        }

        private void buttonZ79_Click(object sender, EventArgs e)
        {
            write_caractere("x");
        }

        private void buttonZ78_Click(object sender, EventArgs e)
        {
            write_caractere("c");
        }

        private void buttonZ71_Click(object sender, EventArgs e)
        {
            write_caractere("v");
        }

        private void buttonZ70_Click(object sender, EventArgs e)
        {
            write_caractere("b");
        }

        private void buttonZ69_Click(object sender, EventArgs e)
        {
            write_caractere("n");
        }

        private void buttonZ1_Click(object sender, EventArgs e)
        {
            write_caractere(".");
        }

        private void buttonZ7_Click(object sender, EventArgs e)
        {
            write_caractere("-");

        }

        private void buttonZ8_Click(object sender, EventArgs e)
        {
            write_caractere("_");
        }

        private void buttonZ58_Click(object sender, EventArgs e)
        {
            write_caractere("1");
        }

        private void buttonZ56_Click(object sender, EventArgs e)
        {
            write_caractere("2");
        }

        private void buttonZ54_Click(object sender, EventArgs e)
        {
            write_caractere("3");
        }

        private void buttonZ52_Click(object sender, EventArgs e)
        {
            write_caractere("4");
        }

        private void buttonZ51_Click(object sender, EventArgs e)
        {
            write_caractere("5");
        }

        private void buttonZ6_Click(object sender, EventArgs e)
        {
            write_caractere("6");
        }

        private void buttonZ57_Click(object sender, EventArgs e)
        {
            write_caractere("7");
        }

        private void buttonZ55_Click(object sender, EventArgs e)
        {
            write_caractere("8");
        }

        private void buttonZ53_Click(object sender, EventArgs e)
        {
            write_caractere("9");
        }

        private void buttonZ50_Click(object sender, EventArgs e)
        {
            write_caractere("0");
        }

        private void buttonZ4_Click(object sender, EventArgs e)
        {
            delete_caractere();
        }

        private void buttonZ5_Click(object sender, EventArgs e)
        {
            write_caractere(":");

        }

        private void textBoxFtp_TextChanged(object sender, EventArgs e)
        {
            flag = 35;
            textBoxContext.BackColor = Color.White;
            textBoxContext.ForeColor = Color.Black;
            textBoxDns.BackColor = Color.White;
            textBoxDns.ForeColor = Color.Black;
        }

        private void textBoxFtpPort_TextChanged(object sender, EventArgs e)
        {
            flag = 36;
            textBoxContext.BackColor = Color.White;
            textBoxContext.ForeColor = Color.Black;
            textBoxDns.BackColor = Color.White;
            textBoxDns.ForeColor = Color.Black;
        }

        private void buttonZ9_Click(object sender, EventArgs e)
        {
            write_caractere("/");
        }

        private void textBoxHote_Click(object sender, EventArgs e)
        {
            flag = 40;
            unselectAllMaj();
            textBoxHote.BackColor = Color.Navy;
            textBoxHote.ForeColor = Color.White;
        }

        private void textBoxUtilisateur_Click(object sender, EventArgs e)
        {
            flag = 41;
            unselectAllMaj();
            textBoxUtilisateur.BackColor = Color.Navy;
            textBoxUtilisateur.ForeColor = Color.White;
        }

        private void textBoxPort_Click(object sender, EventArgs e)
        {
            flag = 42;
            unselectAllMaj();
            textBoxPort.BackColor = Color.Navy;
            textBoxPort.ForeColor = Color.White;
        }

        private void textBoxMdp_Click(object sender, EventArgs e)
        {
            flag = 43;
            unselectAllMaj();
            textBoxMdp.BackColor = Color.Navy;
            textBoxMdp.ForeColor = Color.White;
        }

        private void textBoxRepertoire_Click(object sender, EventArgs e)
        {
            flag = 44;
            unselectAllMaj();
            textBoxRepertoire.BackColor = Color.Navy;
            textBoxRepertoire.ForeColor = Color.White;
        }
        private void unselectAllMaj()
        {
            textBoxHote.BackColor = Color.White;
            textBoxHote.ForeColor = Color.Black;
            textBoxUtilisateur.BackColor = Color.White;
            textBoxUtilisateur.ForeColor = Color.Black;
            textBoxPort.BackColor = Color.White;
            textBoxPort.ForeColor = Color.Black;
            textBoxMdp.BackColor = Color.White;
            textBoxMdp.ForeColor = Color.Black;
            textBoxRepertoire.BackColor = Color.White;
            textBoxRepertoire.ForeColor = Color.Black;
            textBoxVersion.BackColor = Color.White;
            textBoxVersion.ForeColor = Color.Black;
            textBoxCle.BackColor = Color.White;
            textBoxCle.ForeColor = Color.Black;
            textBoxRepertoireFtp.BackColor = Color.White;
            textBoxRepertoireFtp.ForeColor = Color.Black;
        }

        private void textBoxVersion_Click(object sender, EventArgs e)
        {
            flag = 45;
            unselectAllMaj();
            textBoxVersion.BackColor = Color.Navy;
            textBoxVersion.ForeColor = Color.White;
        }

        private void textBoxCle_Click(object sender, EventArgs e)
        {
            flag = 46;
            unselectAllMaj();
            textBoxCle.BackColor = Color.Navy;
            textBoxCle.ForeColor = Color.White;
        }
        private void textBoxConfirmation_Click(object sender, EventArgs e)
        {
            flag = 47;
            unselectAllMaj();
            textBoxRepertoireFtp.BackColor = Color.Navy;
            textBoxRepertoireFtp.ForeColor = Color.White;
        }

        private void buttonZ10_Click(object sender, EventArgs e)
        {
            write_caractere(@"\");
        }

        private void buttonZ11_Click_1(object sender, EventArgs e)
        {
            write_caractere("@");
        }

        private void wsRB_Click(object sender, EventArgs e)
        {
            wssRB.Checked = !wsRB.Checked;
            textBoxXSProtocole.Text = "ws://";
        }

        private void wssRB_Click(object sender, EventArgs e)
        {
            wsRB.Checked = !wssRB.Checked;
            textBoxXSProtocole.Text = "wss://";
        }

        private void textBoxContext2_Click(object sender, EventArgs e)
        {
            flag = 50;
            textBoxDns.BackColor = Color.White;
            textBoxDns.ForeColor = Color.Black;
            textBoxContext.BackColor = Color.White;
            textBoxContext.ForeColor = Color.Black;
            textBoxContext2.BackColor = Color.Navy;
            textBoxContext2.ForeColor = Color.White;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            TerminalMonitor.SetScreensToExtendedMode();
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.btnChiffre204_Sel;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = Properties.Resources.btnChiffre204_Desel;
        }

        private void buttonAutres_Click(object sender, EventArgs e)
        {
            serverPanel.Hide();
            networkPanel.Hide();
            pingPanel.Hide();
            majPanel.Hide();
            panelCaisse2.Hide();
            panelAutres.Show();
            btnServeur.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            btnConfig.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonPing.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            updateBtn.BackgroundImage = Properties.Resources.btnSelectionMenu_Desel;
            buttonAutres.BackgroundImage = Properties.Resources.btnSelectionMenu_Sel;
            flagMaj = 0;
            flagServeur = 0;
            flagPing = 0;
            flagConfig = 0;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.btnChiffre204_Sel;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = Properties.Resources.btnChiffre204_Desel;
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            if (ApplicationContext.IsEcranClientInitialized())
            {
                ApplicationContext.EcranClient.MyDevice.Dispose();
            }
            ApplicationContext.EcranClient = new EcranClient(null);

            EcranClientModel ecranClientModel = new EcranClientModel();
            ecranClientModel.MessageType = MessageType.Simple;
            ecranClientModel.SimpleMsg = "GUICHET FERMÉ";
            ApplicationContext.UpdateEcranClient(ecranClientModel);
        }

        public override void HideForm()
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            if (_dateTimeTimer != null)
                _dateTimeTimer.Dispose();
            Hide();
        }

 

        private void decrease_Click(object sender, EventArgs e)
        {
            if (ApplicationContext.IsEcranClientInitialized())
            {
                ApplicationContext.EcranClient.decreaseBrightness();
            }
        }

        private void increase_Click(object sender, EventArgs e)
        {
            if (ApplicationContext.IsEcranClientInitialized())
            {
                ApplicationContext.EcranClient.increaseBrightness();
            }
        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }
    }
}
