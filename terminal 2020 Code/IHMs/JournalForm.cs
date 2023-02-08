using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using log4net;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModuleBJournal.Models;
using sorec_gamma.modules.ModuleImpression;

namespace sorec_gamma.IHMs
{
    public partial class JournalForm : BaseForm
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(JournalForm));

        private Timer _datetimeTimer;
        private string textHeure;
        private string textMinute;
        private string textNumTransaction;
        private int flag;
        private int linesCount;
        private int index;
        private ConcurrentJournal _currentBJ;

        private BackgroundWorker dataGridBackgroundWorker;

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (dataGridBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                bool found = false;
                dataGridBackgroundWorker.ReportProgress(0);
                foreach (JournalLite journal in ApplicationContext.BJS)
                {
                    if (journal.Session.ToString("yyyyMMdd") == dateTimePicker1.Value.ToString("yyyyMMdd"))
                    {
                        ConcurrentJournal jj = JournalUtils.DeserializeJournalFile(journal.Filename, DateTime.Now);
                        DataworkerJournal dataworkerJournal = new DataworkerJournal(jj, journal);
                        dataGridBackgroundWorker.ReportProgress(100, dataworkerJournal);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    dataGridBackgroundWorker.ReportProgress(99);
                }
            }
            catch { }
        }
        
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                buttonZ4.Enabled = false;
                buttonZ49.Enabled = false;
                precedentPage(true);
                nextPage(true);
                firstLine(true);
                lastLine(true);
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                loadingLbl.Text = "Chargement en cours. Veuillez patienter...";
                loadingLbl.Show();
                loadingLbl.Refresh();
            }
            else if (e.ProgressPercentage == 100)
            {
                DataworkerJournal dataworkerJournal = e.UserState as DataworkerJournal;
                if (dataworkerJournal.JournalLite != null)
                {
                    precedentPage(dataworkerJournal.JournalLite.IsLast);
                    nextPage(dataworkerJournal.JournalLite.IsFirst);
                }
                else
                {
                    precedentPage(true);
                    nextPage(true);
                }
                if (dataworkerJournal.Journal != null)
                {
                    _currentBJ = dataworkerJournal.Journal;

                    linesCount = _currentBJ.GetTotalLines();
                    loadingLbl.Hide();
                    buttonZ4.Enabled = true;
                    buttonZ49.Enabled = true;
                }
                else
                {
                    if (dataworkerJournal.JournalLite != null)
                    {
                        _currentBJ = new ConcurrentJournal(dataworkerJournal.JournalLite.Session);
                    }
                    else
                    {
                        _currentBJ = new ConcurrentJournal(DateTime.Now.Date.AddSeconds(-1));
                    }
                    linesCount = 0;
                    loadingLbl.Text = "La source est corrompue.";
                    loadingLbl.Show();
                    loadingLbl.Refresh();
                }
                initializefields();
                index = 0;
                firstLine(true);
                addLines();
                lastLine(linesCount < JournalUtils.PAGE_SIZE);
            }
            else if (e.ProgressPercentage == 99)
            {
                _currentBJ = new ConcurrentJournal(DateTime.Now.Date.AddSeconds(-1));
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                loadingLbl.Text = "Aucune opération trouvée.";
                loadingLbl.Show();
                loadingLbl.Refresh();
                precedentPage(true);
                nextPage(false);
                initializefields();
                firstLine(true);
                addLines();
                lastLine(true);
            }
        }

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
            }
            base.Dispose(disposing);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            ApplicationContext.QuitApplication();
        }
        public JournalForm()
        {
            InitializeComponent();
            Initialize();
        }
        private void conn_HealthCheck(object sender, EventArgs e)
        {
            ProgressChangedEventArgs connEventArgs = e as ProgressChangedEventArgs;
            bool _isNetworkOnline = (bool)connEventArgs.UserState;
            etatConnNokPB.Invoke(new MethodInvoker(delegate { etatConnNokPB.Visible = !_isNetworkOnline; }));
            etatConnOKPB.Invoke(new MethodInvoker(delegate { etatConnOKPB.Visible = _isNetworkOnline; }));
        }

        public void Initialize()
        {
            _currentBJ = new ConcurrentJournal(ApplicationContext.JOURNAL);

            etatConnNokPB.Visible = !ApplicationContext.IsNetworkOnline;
            etatConnOKPB.Visible = ApplicationContext.IsNetworkOnline;
            ApplicationContext.HealthConnEventHandler += conn_HealthCheck;
            _datetimeTimer = new Timer
            {
                Enabled = true,
                Interval = 1
            };
            _datetimeTimer.Tick += new EventHandler(_TimeTick);

            label5.Hide();
            btnJournal.BackColor = Color.Gold;
            lblGuichet.Text = "Guichet  "
                + ConfigUtils.ConfigData.Num_pdv
                + " - "
                + ConfigUtils.ConfigData.Pos_terminal;
            lblIp.Text = "IP " + ApplicationContext.IP;
            lblVersion.Text = "V" + ApplicationContext.SOREC_DATA_VERSION_LOG
                 + ApplicationContext.SOREC_DATA_ENV;
            flag = 1;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.CurrentCell = null;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ScrollBars = ScrollBars.None;
            firstLine(true);
            linesCount = _currentBJ.GetTotalLines();
            lastLine(linesCount < JournalUtils.PAGE_SIZE);
            dateTimePicker1.MaxDate = DateTime.Today.AddDays(1).AddSeconds(-1);
            if (dateTimePicker1.MaxDate.CompareTo(_currentBJ.Session) >= 0)
            {
                dateTimePicker1.Value = _currentBJ.Session;
            }
            index = 0;
            addLines();
            initializefields();
            controlsPageButtons();
            buttonZ4.Enabled = true;
        }

        private void controlsPageButtons()
        {
            if (ApplicationContext.BJS.Count > 1)
            {
                precedentPage(false);
            }
            else
            {
                precedentPage(true);
            }
            nextPage(true);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (linesCount > JournalUtils.PAGE_SIZE)
            {
                if (index < linesCount - JournalUtils.PAGE_SIZE)
                {
                    index += JournalUtils.PAGE_SIZE;
                    addLines();
                    firstLine(false);
                }
                else
                {
                    index = linesCount - JournalUtils.PAGE_SIZE;
                    addLines();
                }
                lastLine(index >= linesCount - JournalUtils.PAGE_SIZE);
                initializefields();
            }
        }

        private void buttonZ4_Click(object sender, EventArgs e)
        {
          if (!(sender as Control).Enabled)
                return;
            buttonZ4.Enabled = false;
            try
            {
                List<ConcurrentLigne> lignes = JournalUtils.GetNextPage(index, _currentBJ);
                ImprimerJournalService.imprimerJournal(lignes);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Exception: {0} - StackTrace: {1}", ex.Message, ex.StackTrace));
            }
            finally
            {
                Application.DoEvents();
                buttonZ4.Enabled = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (linesCount > JournalUtils.PAGE_SIZE)
            {
                if (index > JournalUtils.PAGE_SIZE)
                {
                    index -= JournalUtils.PAGE_SIZE;
                    addLines();
                    lastLine(false);
                    firstLine(false);
                }
                else
                {
                    index = 0;
                    addLines();
                    lastLine(false);
                    firstLine(true);
                }
                initializefields();
            }
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                index--;
                lastLine(false);
            }
            firstLine(index <= 0);
            addLines();
            initializefields();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (linesCount > JournalUtils.PAGE_SIZE)
            {
                if (index < linesCount - JournalUtils.PAGE_SIZE)
                {
                    index++;
                    firstLine(false);
                    addLines();
                }
                lastLine(index >= linesCount - JournalUtils.PAGE_SIZE);
                initializefields();
            }
        }

        private void buttonZ49_Click(object sender, EventArgs e)
        {
            if (!(sender as Control).Enabled || _currentBJ == null)
                return;
            try
            {
                buttonZ49.Enabled = false;
                List<ConcurrentOperation> sortedOperations = _currentBJ.Operations.OrderByDescending(op => op.Heure).ToList();
                List<ConcurrentLigne> lines = JournalUtils.GetListLines(sortedOperations);
                if (!textNumTransaction.Equals(""))
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Value3.Contains("N°" + textNumTransaction))
                        {
                            index = i;
                            addLines();
                            searchControlButtons();
                            return;
                        }
                    }
                }
                if (!textHeure.Equals("") && !textMinute.Equals(""))
                {
                    for (int i = 0; i < lines.Count; i++)
                    {
                        if (lines[i].Value1.Contains(textHeure + ":" + textMinute))
                        {
                            index = i;
                            addLines();
                            searchControlButtons();
                            return;
                        }
                    }
                }
            }
            catch { }
            finally
            {
                Application.DoEvents();
                buttonZ49.Enabled = true;
            }
        }

        private void searchControlButtons()
        {
            if (index == 0)
            {
                firstLine(true);
            }
            else
            {
                firstLine(false);
            }
            if (index > linesCount - JournalUtils.PAGE_SIZE || linesCount < JournalUtils.PAGE_SIZE)
            {
                lastLine(true);
            }
            else
            {
                firstLine(false);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (linesCount > JournalUtils.PAGE_SIZE)
            {
                index = 0;
                addLines();
                firstLine(true);
                lastLine(false);
                initializefields();
            }
        }
        private void firstLine(bool active)
        {
            if (active)
            {
                pictureBox8.BackgroundImage = Properties.Resources.upAll;
                pictureBox3.BackgroundImage = Properties.Resources.upPage;
                pictureBox7.BackgroundImage = Properties.Resources.upOne;
                pictureBox8.Enabled = false;
                pictureBox3.Enabled = false;
                pictureBox7.Enabled = false;
            }
            else
            {
                pictureBox8.BackgroundImage = Properties.Resources.oneFlecheToLeft;
                pictureBox3.BackgroundImage = Properties.Resources.twoFlecheUp;
                pictureBox7.BackgroundImage = Properties.Resources.oneFlecheUp;
                pictureBox8.Enabled = true;
                pictureBox3.Enabled = true;
                pictureBox7.Enabled = true;
            }
        }
        private void addLines()
        {
            List<ConcurrentLigne> lignes = JournalUtils.GetNextPage(index, _currentBJ);
            dataGridView1.Rows.Clear();
            if (lignes.Count > 0)
            {
                dataGridView1.RowCount = JournalUtils.PAGE_SIZE;
                for (int j = 0; j < lignes.Count; j++)
                {
                    dataGridView1.Rows[j].Cells[0].Value = lignes[j].Value1;
                    dataGridView1.Rows[j].Cells[1].Value = lignes[j].Value2;
                    dataGridView1.Rows[j].Cells[2].Value = lignes[j].Value3;
                    if (!string.IsNullOrEmpty(lignes[j].BackColor)
                        && !string.IsNullOrEmpty(lignes[j].ForColor))
                    {
                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.FromName(lignes[j].BackColor);
                        dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.FromName(lignes[j].ForColor);
                    }
                    else
                    {
                        dataGridView1.Rows[j].DefaultCellStyle.BackColor = Color.Gray;
                        dataGridView1.Rows[j].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (linesCount > JournalUtils.PAGE_SIZE && linesCount - index > JournalUtils.PAGE_SIZE)
            {
                index = linesCount - JournalUtils.PAGE_SIZE;
                addLines();
                firstLine(false);
                lastLine(true);
                initializefields();
            }
        }
        private void lastLine(bool active)
        {
            if (active)
            {
                pictureBox5.BackgroundImage = Properties.Resources.downOne;
                pictureBox2.BackgroundImage = Properties.Resources.downPage;
                pictureBox9.BackgroundImage = Properties.Resources.downAll;
                pictureBox5.Enabled = false;
                pictureBox2.Enabled = false;
                pictureBox9.Enabled = false;
            }
            else
            {
                pictureBox5.BackgroundImage = Properties.Resources.oneFlecheDown;
                pictureBox2.BackgroundImage = Properties.Resources.twoFlecheDown;
                pictureBox9.BackgroundImage = Properties.Resources.oneFlecheToLeftDown;
                pictureBox5.Enabled = true;
                pictureBox2.Enabled = true;
                pictureBox9.Enabled = true;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ApplicationContext.BJS = ApplicationContext.BJS.OrderBy(j => j.Session).ToList();
            foreach (JournalLite journal in ApplicationContext.BJS)
            {
                if (journal != null
                    && journal.Session != default
                    && _currentBJ != null
                    && (journal.Session.Date.CompareTo(_currentBJ.Session.Date) > 0))
                {
                    if (dateTimePicker1.MaxDate.Date.CompareTo(journal.Session.Date) >= 0)
                    {
                        dateTimePicker1.Value = journal.Session;
                    }
                    break;
                }
            }
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ApplicationContext.BJS = ApplicationContext.BJS.OrderByDescending(j => j.Session).ToList();
            foreach (JournalLite journal in ApplicationContext.BJS)
            {
                if (journal != null
                    && journal.Session != default
                    && _currentBJ != null
                    && journal.Session.Date.CompareTo(_currentBJ.Session.Date) < 0)
                {
                    if (dateTimePicker1.MaxDate.Date.CompareTo(journal.Session.Date) >= 0)
                    {
                        dateTimePicker1.Value = journal.Session;
                    }
                    break;
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DisposeDataGridBackgroundWorker();
            StartDataGridBackgroundWorker();
        }

        private void nextPage(bool active)
        {
            if (active)
            {
                pictureBox4.BackgroundImage = Properties.Resources.journalSuivantInactif;
                pictureBox4.Enabled = false;
            }
            else
            {
                pictureBox4.BackgroundImage = Properties.Resources.journalSuivant;
                pictureBox4.Enabled = true;
            }

        }
        private void precedentPage(bool active)
        {
            if (active)
            {
                pictureBox6.BackgroundImage = Properties.Resources.journalPrecedentInactif;
                pictureBox6.Enabled = false;
            }
            else
            {
                pictureBox6.BackgroundImage = Properties.Resources.journalPrecedent;
                pictureBox6.Enabled = true;
            }
        }
        private void initializefields()
        {
            flag = 1;
            textBox1.Focus();
            textBox1.ForeColor = Color.White;
            textHeure = "";
            textMinute = "";
            textNumTransaction = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.BackColor = Color.Navy;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
        }

        private void write_caractere(string c)
        {
            switch (flag)
            {
                case 1:
                    this.textNumTransaction += c;
                    this.textBox1.Text = textNumTransaction;
                    this.textBox1.Focus();
                    this.textBox1.SelectionStart = this.textNumTransaction.Length;
                    break;
                case 2:
                    this.textHeure += c;
                    this.textBox2.Text = textHeure;
                    this.textBox2.Focus();
                    this.textBox2.SelectionStart = this.textHeure.Length;
                    break;
                case 3:
                    this.textMinute += c;
                    this.textBox3.Text = textMinute;
                    this.textBox3.Focus();
                    this.textBox3.SelectionStart = this.textMinute.Length;
                    break;
            }
        }
        private void delete_caractere()
        {
            try
            {
                switch (flag)
                {

                    case 1:
                        if (this.textNumTransaction.Length > 0)
                        {
                            this.textNumTransaction = textNumTransaction.Substring(0, textNumTransaction.Length - 1);
                            this.textBox1.Text = textNumTransaction;
                            this.textBox1.Focus();
                            this.textBox1.SelectionStart = this.textNumTransaction.Length;
                        }
                        break;
                    case 2:
                        if (this.textHeure.Length > 0)
                        {
                            this.textHeure = textHeure.Substring(0, textHeure.Length - 1);
                            this.textBox2.Text = textHeure;
                            this.textBox2.Focus();
                            this.textBox2.SelectionStart = this.textHeure.Length;
                        }
                        break;
                    case 3:
                        if (this.textMinute.Length > 0)
                        {
                            this.textMinute = textMinute.Substring(0, textMinute.Length - 1);
                            this.textBox3.Text = textMinute;
                            this.textBox3.Focus();
                            this.textBox3.SelectionStart = this.textMinute.Length;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ApplicationContext.Logger.Error("JournalForm Exception : " + ex.Message + "\n" + ex.StackTrace);
            }

        }

        private void btnPrepose_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchCaissePreposeForm();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchClientForm();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationContext.LaunchPrincipaleForm(false, "", 2);
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            flag = 1;
            textHeure = "";
            textMinute = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.BackColor = Color.Navy;
            textBox1.ForeColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            flag = 2;
            textBox1.Text = "";
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.Navy;
            textBox2.ForeColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textNumTransaction = "";
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            flag = 3;
            textBox1.Text = "";
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.Navy;
            textBox3.ForeColor = Color.White;
            textNumTransaction = "";
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

        private void buttonZ29_MouseEnter(object sender, EventArgs e)
        {
            buttonZ29.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ29_MouseLeave(object sender, EventArgs e)
        {
            buttonZ29.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ30_MouseEnter(object sender, EventArgs e)
        {
            buttonZ30.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ30_MouseLeave(object sender, EventArgs e)
        {
            buttonZ30.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ40_MouseEnter(object sender, EventArgs e)
        {
            buttonZ40.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ40_MouseLeave(object sender, EventArgs e)
        {
            buttonZ40.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ41_MouseEnter(object sender, EventArgs e)
        {
            buttonZ41.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ41_MouseLeave(object sender, EventArgs e)
        {
            buttonZ41.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ42_MouseEnter(object sender, EventArgs e)
        {
            buttonZ42.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ42_MouseLeave(object sender, EventArgs e)
        {
            buttonZ42.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ43_MouseEnter(object sender, EventArgs e)
        {
            buttonZ43.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ43_MouseLeave(object sender, EventArgs e)
        {
            buttonZ43.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ44_MouseEnter(object sender, EventArgs e)
        {
            buttonZ44.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ44_MouseLeave(object sender, EventArgs e)
        {
            buttonZ44.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ45_MouseEnter(object sender, EventArgs e)
        {
            buttonZ45.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ45_MouseLeave(object sender, EventArgs e)
        {
            buttonZ45.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ46_MouseEnter(object sender, EventArgs e)
        {
            buttonZ46.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ46_MouseLeave(object sender, EventArgs e)
        {
            buttonZ46.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ47_MouseEnter(object sender, EventArgs e)
        {
            buttonZ47.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ47_MouseLeave(object sender, EventArgs e)
        {
            buttonZ47.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ48_MouseEnter(object sender, EventArgs e)
        {
            buttonZ48.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ48_MouseLeave(object sender, EventArgs e)
        {
            buttonZ48.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }

        private void buttonZ49_MouseEnter(object sender, EventArgs e)
        {
            buttonZ49.BackgroundImage = Properties.Resources.btnChiffre64_Sel;
        }

        private void buttonZ49_MouseLeave(object sender, EventArgs e)
        {
            buttonZ49.BackgroundImage = Properties.Resources.btnChiffre64_Desel;
        }
        
        private void DisposeDataGridBackgroundWorker()
        {
            try
            {
                if (JournalUtils.DeserializerThread != null)
                {
                    JournalUtils.DeserializerThread.Abort();
                }
                if (dataGridBackgroundWorker != null)
                {
                    dataGridBackgroundWorker.CancelAsync();
                    dataGridBackgroundWorker.Dispose();
                    dataGridBackgroundWorker.DoWork -= backgroundWorker_DoWork;
                    dataGridBackgroundWorker.ProgressChanged -= backgroundWorker_ProgressChanged;
                }
                JournalUtils.DeserializerThread = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void StartDataGridBackgroundWorker()
        {
            dataGridBackgroundWorker = new BackgroundWorker();
            dataGridBackgroundWorker.WorkerSupportsCancellation = true;
            dataGridBackgroundWorker.WorkerReportsProgress = true;
            dataGridBackgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            dataGridBackgroundWorker.ProgressChanged +=
                backgroundWorker_ProgressChanged;
            dataGridBackgroundWorker.RunWorkerAsync();
        }

        public override void HideForm()
        {
            DisposeDataGridBackgroundWorker();
            if (_datetimeTimer != null)
                _datetimeTimer.Dispose();
            ApplicationContext.HealthConnEventHandler -= conn_HealthCheck;
            _currentBJ = null;
            Hide();
        }
        private void buttonZ4_MouseEnter(object sender, EventArgs e)
        {
            buttonZ4.BackgroundImage = Properties.Resources.btnChiffre134_Sel;

        }
        private void button7_MouseEnter(object sender, EventArgs e)
        {
            button7.BackgroundImage = Properties.Resources.btnChiffre134_Sel;
        }

        private void buttonZ4_MouseLeave(object sender, EventArgs e)
        {
            buttonZ4.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }
        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackgroundImage = Properties.Resources.btnChiffre134_Desel;
        }
        private void _TimeTick(object sender, EventArgs e)
        {
            lblwatch.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnJournal_Click(object sender, EventArgs e)
        {

        }
    }
}
