using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleMAJ;
using sorec_gamma.modules.ModuleMAJ.Controls;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/*                                                                         ²
 * yelkarkouri@PCARD
 */
namespace sorec_gamma.IHMs.ComposantsGraphique
{
    public partial class MAJForm : Form
    {
        private MAJControle _majLogicielleControle;
        private bool _background = false;
        private DateTime _dateVersion;
        private string _newVersion;
        private string _url;
        private string _port;
        private string _login;
        private string _mdp;
        private string _destRep;
        private string _sourceDir;
        public MAJForm(bool auth = false, string newVersion = null)
        {
            InitializeComponent();
            ApplicationContext.Logger.Info(string.Format("MAJForm: init"));
            
            _majLogicielleControle = new MAJControle();
            string host = ConfigUtils.ConfigData.Host_ftp;
            string protocol = ConfigUtils.ConfigData.Protocol_ftp;
            _url = protocol + "://" + host;
            _port = ConfigUtils.ConfigData.Port_ftp;
            _login = ConfigUtils.ConfigData.Login;
            _mdp = ConfigUtils.ConfigData.Mdp;
            string decryptedMdpFtp = Cryptography.Decrypt(_mdp, "sorecgamma123");
            _mdp = decryptedMdpFtp;
            _destRep = "D:\\GAMMA\\MAJ";
            _sourceDir = ConfigUtils.ConfigData.Source_rep;

            _newVersion = newVersion;
            if (auth)
                _dateVersion = DateTime.Now;

            Thread majThread = new Thread(() => MAJLogicielleControle());
            majThread.Name = "MAJ";
            majThread.Start();
        }

        private void MAJLogicielleControle()
        {
            if (_dateVersion == null || _newVersion == null)
            {
                string result = _majLogicielleControle.sendRequest("EN_COURS_MAJ");
                int code_reponse = TLVHandler.Response(result);
                if (code_reponse == Constantes.RESPONSE_CODE_OK)
                {
                    string tlv = TLVHandler.getTLVChamps(result);
                    TLVHandler appTagHandler = new TLVHandler(tlv);
                    TLVTags majLog = appTagHandler.getTLV(TLVTags.SOREC_MAJ_LOGICIELLE);
                    TLVHandler majHandler = new TLVHandler(Utils.bytesToHex(majLog.value));
                    _newVersion = Utils.bytesToHex(majHandler.getTLV(TLVTags.SOREC_DATA_LOGICIEL_VERSION).value);
                    _dateVersion = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(majHandler.getTLV(TLVTags.SOREC_DATA_LOGICIEL_DATE_TIME).value)));

                    MAJLogicielle();
                }
                else
                {
                    ApplicationContext.Logger.Info(string.Format("MAJLogicielleControle: Response => {0}", code_reponse));
                    ShowProgressBar(false);
                    ChangeMsgLabel("La demande pour la nouvelle version a échoué. Veuillez réessayer ou contacter l'administration.", Color.Red);
                    ChangeBtnVisibility(retryBtn, true);
                    ChangeBtnVisibility(cancelBtn, true);
                    ApplicationContext.isVersionAuthorized = false;
                    return;
                }
            }
            MAJLogicielle();
        }

        private void MAJLogicielle()
        {
            string fileName = "T2020_" + _newVersion + ".zip";
            string destPath = _destRep + "\\" + _dateVersion.ToString("yyyyMMdd") + "\\";
            bool alreadyDownloaded = FileUtils.FileExists(destPath + fileName);

            ApplicationContext.Logger.Info(string.Format("version: {0}, date maj: {1}", _newVersion, _dateVersion));

            if (_dateVersion.Date <= DateTime.Now.Date)
            {
                ApplicationContext.isVersionAuthorized = false;
                if (alreadyDownloaded)
                {
                    ChangeMsgLabel("Cette version est déjà téléchargée en arrière-plan. Le terminal redémarrera dans quelques instants...", Color.White);
                    if (!ApplicationContext.develop) TerminalMonitor.Reboot();
                }
                else
                {
                    ShowProgressBar(true);
                    ApplicationContext.Logger.Info(string.Format("url: {0}, port: {1}, source: {2}, destination: {3} ", _url, _port, _sourceDir, _destRep));
                    FtpClient ftpClient = FtpClient.GetInstance(_url, _login, _mdp, _port, this);
                    try
                    {
                        ChangeMsgLabel(string.Format("La nouvelle version {0} en cours de téléchargement. Veuillez ne pas éteindre le terminal.", _newVersion), Color.White);
                        bool isDownloaded = ftpClient.DownloadFile(fileName, _sourceDir, destPath);
                        if (isDownloaded)
                        {
                            ApplicationContext.Logger.Info(string.Format("MAJ Logicielle: version {0} de {1} est téléchargée", _newVersion, _dateVersion));

                            // TODO: calculate checksum
                            //string checksum = ChecksumUtils.GetChecksum(HashingAlgoTypes.MD5, destDir + newVersionFilename);
                            // TODO: compare checksums
                            _majLogicielleControle.sendRequest("CHARGE");

                            ChangeMsgLabel("Le téléchargement s'est terminé avec succès. Le terminal redémarrera dans quelques instants...", Color.White);
                            // closeForm();
                            if (!ApplicationContext.develop) TerminalMonitor.Reboot();
                        }
                        else
                        {
                            ApplicationContext.Logger.Error("MAJ Failed to load from ftp");
                            ShowProgressBar(false);
                            ChangeMsgLabel(string.Format("Une erreur s'est produite lors du téléchargement de la version {0}. Veuillez réessayer ou contacter l'administration.",
                                _newVersion), Color.Red);
                            ChangeBtnVisibility(retryBtn, true);
                            ChangeBtnVisibility(cancelBtn, true);
                        }
                    }
                    catch (Exception e)
                    {
                        ApplicationContext.Logger.Error("Exception download ftp " + _newVersion + " " + e.StackTrace);
                        ChangeMsgLabel(string.Format("Une erreur inconnue s'est produite lors du téléchargement de la version {0}. Veuillez réessayer ou contacter l'administration.",
                            _newVersion), Color.Red);
                        ShowProgressBar(false);
                        ChangeBtnVisibility(retryBtn, true);
                        ChangeBtnVisibility(cancelBtn, true);
                    }
                }
            }
            else if (!alreadyDownloaded)
            {
                ShowProgressBar(false);
                ApplicationContext.isVersionAuthorized = true;
                ChangeMsgLabel(string.Format("La nouvelle version {0} sera téléchargée en arrière-plan pour une installation ultérieure le {1}." +
                    " Veuillez ne pas éteindre le terminal.", _newVersion, _dateVersion.ToString("dd/MM/yyyy")), Color.White);
                ChangeBtnVisibility(validateBtn, true);
                _background = true;
            }
            else
            {
                ApplicationContext.Logger.Info(string.Format("MAJ Locgicielle, Version already downloaded: [{0}, {1}]", _newVersion, _dateVersion));
            }
        }

        private void MAJLogicielleBackground()
        {
            ApplicationContext.majThread = new Thread(() =>
            {
                FtpClient ftpClient = FtpClient.GetInstance(_url, _login, _mdp, _port);
                try
                {
                    string datePrevu = _dateVersion.ToString("yyyyMMdd");
                    bool isDownloaded = ftpClient.DownloadFile("T2020_" + _newVersion + ".zip", _sourceDir, _destRep + "\\" + datePrevu + "\\");
                    if (isDownloaded)
                    {
                        ApplicationContext.Logger.Info("MAJ Logicielle: isDownloaded ");
                        // TODO: calculate checksum
                        //string checksum = ChecksumUtils.GetChecksum(HashingAlgoTypes.MD5, destDir + newVersionFilename);
                        // TODO: compare checksums
                        _majLogicielleControle.sendRequest("CHARGE");
                    }
                    else
                    {
                        ApplicationContext.Logger.Error("MAJLogicielle failed to load from ftp ");
                    }
                }
                catch (Exception e)
                {
                    ApplicationContext.Logger.Error("MAJLogicielle Exception download ftp " + e.StackTrace);
                }
            });
            ApplicationContext.majThread.Name = "MAJ";
            ApplicationContext.majThread.Start();
        }


        private void closeForm()
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    closeForm();
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            Close();
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void validateBtn_Click(object sender, EventArgs e)
        {
            if (_background)
            {
                MAJLogicielleBackground();
            }
            Close();
        }
        

        private void retryBtn_Click(object sender, EventArgs e)
        {
            ChangeMsgLabel(string.Format("La nouvelle version {0} en cours de téléchargement. Veuillez ne pas éteindre le terminal.", _newVersion), Color.White);
            ResetProgressBar(0);
            ChangeBtnVisibility(retryBtn, false);
            ChangeBtnVisibility(cancelBtn, false);
            Thread majThread = new Thread(() => MAJLogicielleControle());
            majThread.Name = "MAJ";
            majThread.Start();
        }

        private void ChangeMsgLabel(string msg, Color color)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    ChangeMsgLabel(msg, color);
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            
            msgLabel.ForeColor = color;
            msgLabel.Text = msg;
            msgLabel.Refresh();
        }

        private void ChangeBtnVisibility(ButtonZ btn, bool visibility)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    ChangeBtnVisibility(btn, visibility);
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            btn.Visible = visibility;
            btn.Refresh();
        }
        
        public void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    UpdateProgress(progress);
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            majProgressbar.Value = (int)progress;
            majProgressbar.PerformStep();
            majProgressbar.Refresh();
        }

        public void ShowProgressBar(bool show = true)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    ShowProgressBar(show);
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            majProgressbar.Visible = show;
            majProgressbar.Refresh();
        }
        
        public void ResetProgressBar(int size)
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    ResetProgressBar(size);
                };
                Invoke(invoker);
                invoker = null;
                return;
            }
            majProgressbar.Visible = true;
            majProgressbar.Minimum = 0;
            majProgressbar.Value = 0;
            majProgressbar.Maximum = size;
        }

    }
}
