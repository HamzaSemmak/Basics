using sorec_gamma.modules.ModuleCote_rapport.controls;
using sorec_gamma.modules.ModuleCoteRapport;
using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.TLV;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System;
using log4net;
using System.Drawing;
using sorec_gamma.modules.UTILS;
using static sorec_gamma.modules.UTILS.MonitorUtils;
using sorec_gamma.modules.ModuleMAJ;

namespace sorec_gamma.modules.ModuleImpression
{
    public partial class CoteForm : Form
    {
        private BackgroundWorker _secondScreenBW;
        private CoteControle _coteControle;
        private readonly ILog Logger = LogManager.GetLogger(typeof(CoteForm));
        private CourseRapport _lastRapport;
        private int _rapportLinesMax = 10;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if (_secondScreenBW != null)
                {
                    if (_secondScreenBW.IsBusy)
                        _secondScreenBW.CancelAsync();
                    _secondScreenBW.Dispose();
                }
            }
            Logger.Info("Dispose Cote form");
            base.Dispose(disposing);
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            MonitorInfo screen = ApplicationContext.GetEcranCotesScreen();
            if (screen == null)
            {
                CloseForm();
            }
        }

        public void CloseForm()
        {
            if (InvokeRequired)
            {
                MethodInvoker invoker = delegate
                {
                    CloseForm();
                };
                Invoke(invoker);
                invoker = null;
            }
            else if (!IsDisposed)
            {
                Close();
            }
        }

        private void _sleepThread(int n)
        {
            while (ApplicationContext.isAuthenticated && n > 0)
            {
                n--;
                Thread.Sleep(1 * 1000);
            }
        }

        public CoteForm(MonitorInfo monitorInfo)
        {
            Logger.Info("Create Cote Form");
            ClientSize = new Size(monitorInfo.Bounds.Width, monitorInfo.Bounds.Height);
            InitializeComponent();

            TerminalMonitor.SetFormToScreen(this, monitorInfo.Bounds);

            Location = new Point(monitorInfo.Bounds.Left, monitorInfo.Bounds.Top);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;

            Cursor.Hide();
            StopCotes(false);
            _coteControle = new CoteControle();
            _configSecondScreenBackgroundWorker();
        }

        private void _configSecondScreenBackgroundWorker()
        {
            Logger.Info("Config Second Screen BackgroundWorker");

            _secondScreenBW = new BackgroundWorker();
            _secondScreenBW.DoWork += _secondScreenWorker_DoWork;
            _secondScreenBW.ProgressChanged += _secondScreenWorker_ProgressChanged;
            _secondScreenBW.WorkerSupportsCancellation = true;
            _secondScreenBW.WorkerReportsProgress = true;
            
            Logger.Info("Start Second Screen BackgroundWorker");
            _secondScreenBW.RunWorkerAsync();
        }

        private void _secondScreenWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (_secondScreenBW.CancellationPending)
                {
                    Logger.Info("Cancellation Pending _secondScreenWorker_DoWork");
                    break;
                }
                UpdateEcranCote();
            }
        }

        private void _secondScreenWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 1: // Cotes
                    ContentCote contentCote = e.UserState as ContentCote;
                    UpdateCotesContent(contentCote);
                    break;
                case 2: // Signals
                    CourseSignal courseSignal = e.UserState as CourseSignal;
                    UpdateCourseSignalContent(courseSignal);
                    break;
                case 3: // Rapports
                    ContentRapport contentRapport = e.UserState as ContentRapport;
                    UpdateRapportsContent(contentRapport);
                    break;
                case 4: // Stop
                    StopCotes();
                    break;
            }
        }
        
        private bool CheckForNewUpdates(bool wait = true)
        {
            bool exist = false;
            if (ApplicationContext.SIGNALS != null && !ApplicationContext.SIGNALS.IsEmpty())
            {
                CourseSignal courseSignal = ApplicationContext.SIGNALS.GetCourseSignal();
                _secondScreenBW.ReportProgress(2, courseSignal);
                _sleepThread(10);
                exist = true;
            }
            else if (ApplicationContext.RAPPORTS != null 
                && ApplicationContext.RAPPORTS .IsContainsNotDisplayedRapports())
            {
                _lastRapport = ApplicationContext.RAPPORTS.GetLastRapport();
                foreach (CourseRapport cr in ApplicationContext.RAPPORTS.CourseRapports)
                {
                    if (cr.Displayed)
                        continue;
                    ContentRapport contentRapport = _getLineRapports(cr);
                    var groupByParent = contentRapport.LineRapports.GroupBy(lr => lr.Parent);
                    groupByParent = groupByParent.OrderBy(p => p.Key?.GetOrder());
                    List<LineRapport> lines = new List<LineRapport>();
                    int nbrGroups = groupByParent.Count();
                    foreach (var group in groupByParent)
                    {
                        var groupSorted = group.OrderBy(g => g.GetOrder());
                        if (_rapportLinesMax - lines.Count <= groupSorted.Count())
                            _fireContentRapportEvent(contentRapport, lines);

                        lines.Add(group.Key);
                        foreach (var child in groupSorted)
                            lines.Add(child);
                        
                        nbrGroups--;
                        if (lines.Count == _rapportLinesMax || nbrGroups < 1)
                            _fireContentRapportEvent(contentRapport, lines);
                    }
                    cr.Displayed = true;
                    exist = true;
                }
            }
            else if (wait)
            {
                _sleepThread(10);
            }
            if (ApplicationContext.SOREC_DATA_COTES != null)
            {
                while (ApplicationContext.SOREC_DATA_COTES.Count > 0)
                {
                    TLVTags tlvTagCote;
                    ApplicationContext.SOREC_DATA_COTES.TryTake(out tlvTagCote);
                    ApplicationContext.COTES.AddOrUpdateReunionCotes(_coteControle.ProcessCotes(tlvTagCote));
                }
            }
            if (ApplicationContext.COTES != null
                && ApplicationContext.SOREC_DATA_OFFRE != null
                && ApplicationContext.COTES.ReunionCotes != null
                && ApplicationContext.COTES.ReunionCotes.Count > 0)
            {
                ApplicationContext.COTES.UpdateCotes(ApplicationContext.SOREC_DATA_OFFRE);
            }

            return exist;
        }

        private void _fireContentRapportEvent(ContentRapport contentRapport, List<LineRapport> lines)
        {
            ContentRapport contentRapportPartial = new ContentRapport();
            contentRapportPartial.CodeHippo = contentRapport.CodeHippo;
            contentRapportPartial.DateReunion = contentRapport.DateReunion;
            contentRapportPartial.NumCourse = contentRapport.NumCourse;
            contentRapportPartial.NumReunion = contentRapport.NumReunion;
            contentRapportPartial.DateTimeRapport = contentRapport.DateTimeRapport;
            contentRapportPartial.AddOrUpdateLineRapports(lines);
            _secondScreenBW.ReportProgress(3, contentRapportPartial);
            _sleepThread(10);
            lines.Clear();
        }

        private ContentRapport _getLineRapports(CourseRapport courseRapport)
        {
            ProduitRapport produitGagRapports = courseRapport.Produits
                .Where(pc => pc.Produit == "GAG" && pc.CanBeDisplayed())
                .FirstOrDefault();
            ProduitRapport produitPlcRapports = courseRapport.Produits
                .Where(pc => pc.Produit == "PLA" && pc.CanBeDisplayed())
                .FirstOrDefault();
            ProduitRapport produitJugRapports = courseRapport.Produits
                .Where(pc => pc.Produit == "JUG" && pc.CanBeDisplayed())
                .FirstOrDefault();
            ProduitRapport produitJupRapports = courseRapport.Produits
                .Where(pc => pc.Produit == "JUP" && pc.CanBeDisplayed())
                .FirstOrDefault();
            List<ProduitRapport> otherProduitsRapports = courseRapport.Produits
                .Where(pc => pc.CanBeDisplayed() && pc.Produit != "GAG" && pc.Produit != "PLA" && pc.Produit != "JUG" && pc.Produit != "JUP")
                .ToList();

            ContentRapport contentRapport = new ContentRapport();
            contentRapport.NumCourse = "" + courseRapport.Course;
            contentRapport.NumReunion = "" + courseRapport.Reunion;
            contentRapport.DateReunion = courseRapport.Date;
            contentRapport.CodeHippo = courseRapport.LibReunion;
            contentRapport.DateTimeRapport = courseRapport.Timestamp;
            
            List<string> produitsRembourses = new List<string>();

            if (produitGagRapports != null)
            {
                if (produitGagRapports.IsRapport())
                {
                    LineRapport lrParent = new LineRapport("SIMPLE", "SIMPLE", "GAGNANT", "PLACE", null, true);
                    foreach (CombinaisonRapport cr in produitGagRapports.Rapports)
                    {
                        LineRapport lrChild = new LineRapport("SIMPLE", cr.Combinaison, Convert.ToString(cr.Rapport), "", lrParent);
                        contentRapport.AddOrUpdateLineRapports(lrChild, false);
                    }
                }
                else if(produitGagRapports.IsRemboursement())
                {
                    produitsRembourses.Add(_getNameProduit(produitGagRapports.Produit));
                }
            }
            if (produitPlcRapports != null)
            {
                if (produitPlcRapports.IsRapport())
                {
                    LineRapport lrParent = new LineRapport("SIMPLE", "SIMPLE", "GAGNANT", "PLACE", null, true);
                    foreach (CombinaisonRapport cr in produitPlcRapports.Rapports)
                    {
                        LineRapport lrChild = new LineRapport("SIMPLE", cr.Combinaison, "", Convert.ToString(cr.Rapport), lrParent);
                        contentRapport.AddOrUpdateLineRapports(lrChild, true);
                    }
                }
                else if (produitPlcRapports.IsRemboursement())
                {
                    produitsRembourses.Add(_getNameProduit(produitPlcRapports.Produit));
                }

            }
            if (produitJugRapports != null)
            {
                if (produitJugRapports.IsRapport())
                {
                    LineRapport lrParent = new LineRapport("JUMELE", "JUMELE", "GAGNANT", "PLACE", null, true);
                    foreach (CombinaisonRapport cr in produitJugRapports.Rapports)
                    {
                        LineRapport lrChild = new LineRapport("JUMELE", cr.Combinaison, Convert.ToString(cr.Rapport), "", lrParent);
                        contentRapport.AddOrUpdateLineRapports(lrChild, false);
                    }
                }
                if (produitJugRapports.IsRemboursement())
                {
                    produitsRembourses.Add(_getNameProduit(produitJugRapports.Produit));
                }
            }
            if (produitJupRapports != null)
            {
                if (produitJupRapports.IsRapport())
                {
                    LineRapport lrParent = new LineRapport("JUMELE", "JUMELE", "GAGNANT", "PLACE", null, true);
                    foreach (CombinaisonRapport cr in produitJupRapports.Rapports)
                    {
                        LineRapport lrChild = new LineRapport("JUMELE", cr.Combinaison, "", Convert.ToString(cr.Rapport), lrParent);
                        contentRapport.AddOrUpdateLineRapports(lrChild, true);
                    }
                }
                else if (produitJupRapports.IsRemboursement())
                {
                    produitsRembourses.Add(_getNameProduit(produitJupRapports.Produit));
                }
            }

            if (otherProduitsRapports != null)
            {
                foreach (ProduitRapport pr in otherProduitsRapports)
                {
                    if (pr.IsRapport())
                    {
                        LineRapport lrParent = new LineRapport(pr.Produit, _getNameProduit(pr.Produit), "", "", null, true);
                        foreach (CombinaisonRapport cr in pr.Rapports)
                        {
                            LineRapport lrChild = new LineRapport(pr.Produit, cr.Combinaison, _getCombinaisonType(cr.Type), Convert.ToString(cr.Rapport), lrParent);
                            contentRapport.AddOrUpdateLineRapports(lrChild, null);
                        }
                    }
                    else if (pr.IsRemboursement())
                    {
                        produitsRembourses.Add(_getNameProduit(pr.Produit));
                    }
                }
            }

            if (produitsRembourses.Count > 0)
            {
                LineRapport remboursementParent = new LineRapport("REMBOURSEMENT", "REMBOURSEMENT", "", "", null, true);
                List<List<string>> prodRemChunks = StringUtils.SplitList(produitsRembourses, 3);
                prodRemChunks.Sort((a, b) => a.Count - b.Count);
                foreach (List<string> proList in prodRemChunks)
                {
                    string prod1 = proList[0];
                    string prod2 = proList.Count > 1 ? proList[1] : "";
                    string prod3 = proList.Count > 2 ? proList[2] : "";
                    LineRapport prodRemLine = new LineRapport("REMBOURSEMENT", prod1, prod2, prod3, remboursementParent, false);
                    contentRapport.AddOrUpdateLineRapports(prodRemLine, null);
                }
            }
            
            return contentRapport;
        }
         
        private void UpdateEcranCote()
        {
            if (ApplicationContext.isAuthenticated)
            {
                if (CheckForNewUpdates(false))
                    return;
                foreach (ReunionCote reunionCote in ApplicationContext.COTES.ReunionCotes)
                {
                    if (reunionCote.DateReunion.Day != DateTime.Today.Day)
                    {
                        continue;
                    }

                    CourseCote firstCoteEnVente = reunionCote.GetFirstCourseCoteEnVente();
                    if (firstCoteEnVente == null)
                    {
                        continue;
                    }
                    ContentCote contentCoteSimple = new ContentCote();
                    contentCoteSimple.NumReunion = reunionCote.NumReunion;
                    contentCoteSimple.NumCourse = firstCoteEnVente.NumCourse;
                    contentCoteSimple.OrderBy = CoteOrderBy.HorseNumber;

                    List<ProduitCote> produitGagCotes = firstCoteEnVente.ProduitCotes.Where(pc => pc.Code == "GAG").ToList();
                    List<ProduitCote> produitPlaCotes = firstCoteEnVente.ProduitCotes.Where(pc => pc.Code == "PLA").ToList();

                    DateTime dateTime = DateTime.MinValue;
                    foreach (ProduitCote produitCote in produitGagCotes)
                    {
                        contentCoteSimple.IsTwoColumns = false;
                        contentCoteSimple.Column1Name = produitCote.Code;
                        contentCoteSimple.Column2Name = null;
                        contentCoteSimple.MisesTotal1 = produitCote.TotalMises;
                        if (produitCote.DateCote.CompareTo(dateTime) > 0)
                        {
                            dateTime = produitCote.DateCote;
                        }
                        foreach (CombinaisonCote cc in produitCote.CombinaisonCotes)
                        {
                            LineCote lineCote = new LineCote();
                            lineCote.CombPartants = cc.CombPartants;
                            lineCote.CoteProduit1 = cc.Cote;
                            lineCote.CoteProduit2 = "0";
                            contentCoteSimple.AddOrUpdateLineCotes(lineCote, false);
                        }
                    }
                    foreach (ProduitCote produitCote in produitPlaCotes)
                    {
                        contentCoteSimple.Column2Name = produitCote.Code;
                        contentCoteSimple.IsTwoColumns = true;
                        contentCoteSimple.MisesTotal2 = produitCote.TotalMises;
                        if (produitCote.DateCote.CompareTo(dateTime) > 0)
                        {
                            dateTime = produitCote.DateCote;
                        }
                        foreach (CombinaisonCote cc in produitCote.CombinaisonCotes)
                        {
                            LineCote lineCote = new LineCote();
                            lineCote.CombPartants = cc.CombPartants;
                            lineCote.CoteProduit1 = "0";
                            lineCote.CoteProduit2 = cc.TotalMise;
                            contentCoteSimple.AddOrUpdateLineCotes(lineCote, true);
                        }
                    }
                    contentCoteSimple.HeureCote = dateTime.ToString("HH:mm");

                    if (!contentCoteSimple.IsEmpty())
                    {
                        _secondScreenBW.ReportProgress(1, contentCoteSimple);
                    }

                    if (!ApplicationContext.isAuthenticated || CheckForNewUpdates())
                        break;
                    
                    List<ProduitCote> produitJugCotes = firstCoteEnVente.ProduitCotes.Where(pc => pc.Code == "JUG").ToList();
                    
                    ContentCote contentCoteJumele = new ContentCote();
                    contentCoteJumele.NumReunion = reunionCote.NumReunion;
                    contentCoteJumele.NumCourse = firstCoteEnVente.NumCourse;
                    contentCoteJumele.OrderBy = CoteOrderBy.Cote;

                    dateTime = DateTime.MinValue;
                    foreach (ProduitCote produitCote in produitJugCotes)
                    {
                        if (produitCote.DateCote.CompareTo(dateTime) > 0)
                        {
                            dateTime = produitCote.DateCote;
                        }
                        contentCoteJumele.Column1Name = produitCote.Code;
                        contentCoteJumele.IsTwoColumns = false;
                        contentCoteJumele.MisesTotal1 = produitCote.TotalMises;
                        foreach (CombinaisonCote cc in produitCote.CombinaisonCotes)
                        {
                            LineCote lineCote = new LineCote();
                            lineCote.CombPartants = cc.CombPartants;
                            lineCote.CoteProduit2 = "0";
                            lineCote.CoteProduit1 = cc.Cote;
                            contentCoteJumele.AddOrUpdateLineCotes(lineCote, false);
                        }
                    }
                    contentCoteJumele.HeureCote = dateTime.ToString("HH:mm");
                    if (!contentCoteJumele.IsEmpty())
                    {
                        _secondScreenBW.ReportProgress(1, contentCoteJumele);
                    }

                    if (!ApplicationContext.isAuthenticated || CheckForNewUpdates())
                        break;
                }
            }
            if (!ApplicationContext.isAuthenticated || ApplicationContext.COTES.IsEmpty())
            {
                _secondScreenBW.ReportProgress(4);
                Thread.Sleep(3 * 1000);
            }
        }

        private void StopCotes(bool clear = true)
        {
            if (ApplicationContext.isAuthenticated)
            {
                slidingRapport.Text = _lastRapport != null ? _lastRapport.ToString() : "";

                standbyMsg1.Text = "BIENVENUE DANS L'UNIVERS DU CHEVAL.";
                standbyMsg2.Show();

                panelFooter.BackColor = Color.White;
                slidingRapport.ForeColor = Color.Black;
                panelFooter.Show();
            }
            else
            {
                standbyMsg1.Text = "GUICHET FERMÉ";
                standbyMsg2.Hide();
                panelFooter.Hide();
                if (clear)
                {
                    _lastRapport = null;
                    ApplicationContext.SIGNALS.ClearAll();
                    ApplicationContext.RAPPORTS.ClearAll();
                }
            }
            panelStandBy.Show();
            panelSignaux.Hide();
            panelCotes.Hide();
        }

        private void UpdateCotesContent(ContentCote contentCote)
        {
            slidingRapport.Text = _lastRapport != null ? _lastRapport.ToString() : "";
            if (contentCote != null)
            {
                //hideLabels();
                lblHippo.Text = contentCote.Hippo;
                courseLbl.Text = "C" + contentCote.NumCourse;
                reunionLbl.Text = "R" + contentCote.NumReunion;
                lblProdName1.Text = _getNameProduit(contentCote.Column1Name);
                lbltotalMiseProd1.Text = formatCote(contentCote.MisesTotal1).ToString();
                timeLbl.Text = contentCote.HeureCote;
                lblProdName1.Show();
                if (contentCote.IsTwoColumns)
                {
                    lblProdName2.Text = _getNameProduit(contentCote.Column2Name);
                    lblProdName2.Show();
                    lbltotalMiseProd2.Text = formatCote(contentCote.MisesTotal2).ToString();
                    lbltotalMiseProd2.Show();
                }
                int indice = 1;
                List<LineCote> lineCotes = contentCote.LineCotes.ToList();

                try
                {
                    if (contentCote.OrderBy == CoteOrderBy.Cote)
                    {
                        lineCotes = lineCotes.OrderBy(lc => decimal.Parse(lc.CoteProduit1, CultureInfo.InvariantCulture)).ToList();
                    }
                    else
                    {
                        lineCotes = lineCotes.OrderBy(lc => long.Parse(lc.CombPartants)).ToList();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Cotes UpdateContent Exception: " + ex.Message);
                }
                finally
                {
                    foreach (LineCote combCote in lineCotes)
                    {
                        Control[] labels = getLabelsByIndice(indice);
                        if (labels[0] != null && labels[1] != null && labels[2] != null)
                        {
                            labels[0].Text = combCote.CombPartants;
                            labels[0].Show();
                            labels[1].Text = formatCote(combCote.CoteProduit1, true).ToString();
                            labels[1].Show();
                            if (contentCote.IsTwoColumns)
                            {
                                labels[2].Text = formatCote(combCote.CoteProduit2).ToString();
                                labels[2].Show();
                            }
                        }
                        indice++;
                    }

                    panelFooter.BackColor = Color.LimeGreen;
                    slidingRapport.ForeColor = Color.White;
                    panelProduits.Show();
                    panelRapports.Hide();

                    panelCotes.Show();
                    panelFooter.Show();
                    panelSignaux.Hide();
                    panelStandBy.Hide();
                }
            }
        }

        private void UpdateCourseSignalContent(CourseSignal courseSignal)
        {
            slidingRapport.Text = _lastRapport != null ? _lastRapport.ToString() : "";
            signalHippoLbl.Text = courseSignal.CodeHippo;
            signalCourseReunLbl.Text = string.Format("Réunion {0} - Course {1}",
                courseSignal.NumReunion, courseSignal.NumCourse);
            signalCourseMessage.Text = courseSignal.Message;

            panelStandBy.Hide();
            panelCotes.Hide();

            panelFooter.BackColor = Color.Black;
            slidingRapport.ForeColor = Color.White;
            panelSignaux.Show();
            panelFooter.Show();
        }

        private void UpdateRapportsContent(ContentRapport contentRapport)
        {
            slidingRapport.Text = _lastRapport != null ? _lastRapport.ToString() : "";
            if (contentRapport != null)
            {
                hideRapportPanels();
                lblHippo.Text = contentRapport.CodeHippo;
                courseLbl.Text = "C" + contentRapport.NumCourse;
                reunionLbl.Text = "R" + contentRapport.NumReunion;
                timeLbl.Text = contentRapport.DateTimeRapport.ToString("HH:mm");
                int i = 1;
                foreach (LineRapport lineRapport in contentRapport.LineRapports)
                {
                    Control panel = getPanelRapportByIndice(i);
                    if (panel != null)
                    {
                        panel.BackColor = lineRapport.IsHeader ? Color.LimeGreen : Color.Black;
                        Control[] labels = getLabelsRapportsByIndice(i, panel);
                        if (labels[0] != null && labels[1] != null && labels[2] != null)
                        {
                            labels[0].Text = lineRapport.Column1;
                            labels[1].Text = lineRapport.Column2;
                            labels[2].Text = lineRapport.Column3;
                            panel.Show();
                        }
                    }
                    i++;
                }

                panelStandBy.Hide();
                panelSignaux.Hide();
                panelProduits.Hide();

                panelFooter.BackColor = Color.LimeGreen;
                slidingRapport.ForeColor = Color.White;
                panelFooter.Show();
                panelRapports.Show();
                panelCotes.Show();
            }
        }

        // ---------------------- Utils ------------------------
        private Control[] getLabelsByIndice(int indice)
        {
            Control[] labels = new Control[3];
            string combName = "lblC" + indice.ToString();
            string prodGagName = "lblCote" + indice.ToString();
            string produitPlcName = "lblCO" + indice.ToString();
            int count = 0;
            foreach (Control control in panelProduits.Controls)
            {
                if (control is Label)
                {
                    if (control.Name.Equals(combName))
                    {
                        labels[0] = control;
                        count++;
                    }
                    else if (control.Name.Equals(prodGagName))
                    {
                        labels[1] = control;
                        count++;
                    }
                    else if (control.Name.Equals(produitPlcName))
                    {
                        labels[2] = control;
                        count++;
                    }
                }
                if (count == 3)
                    break;
            }
            return labels;   
        }
        
        private Control getPanelRapportByIndice(int indice)
        {
            string rapportPanel = "panelR" + indice.ToString();
            foreach (Control control in panelRapports.Controls)
            {
                if (control is Panel)
                {
                    if (control.Name.Equals(rapportPanel))
                    {
                       return control;
                    }
                }
            }
            return null;
        }

        private Control[] getLabelsRapportsByIndice(int indice, Control panel)
        {
            Control[] labels = new Control[3];
            string col3 = "column" + indice.ToString() + "3Lbl";
            string col2 = "column" + indice.ToString() + "2Lbl";
            string col1 = "column" + indice.ToString() + "1Lbl";
            int count = 0;
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    if (control.Name.Equals(col1))
                    {
                        labels[0] = control;
                        count++;
                    }
                    else if (control.Name.Equals(col2))
                    {
                        labels[1] = control;
                        count++;
                    }
                    else if (control.Name.Equals(col3))
                    {
                        labels[2] = control;
                        count++;
                    }
                }
                if (count == 3)
                    break;
            }
            return labels;
        }

        private string _getNameProduit(string code)
        {
            string produit;
            switch (code)
            {
                case "GAG": produit = "GAGNANT"; break;
                case "PLA": produit = "PLACE"; break;
                case "JUG": produit = "JUMELE G"; break;
                case "JUO": produit = "JUMELE O"; break;
                case "JUP": produit = "JUMELE P"; break;
                case "TRO": produit = "TRIO"; break;
                case "TRC": produit = "TIERCE"; break;
                case "QUU": produit = "QUARTE"; break;
                case "QAP": produit = "QUARTE +"; break;
                case "QIP": produit = "QUINTE +"; break;
                case "MU4": produit = "MULTI 4"; break;
                case "MU5": produit = "MULTI 5"; break;
                case "MU6": produit = "MULTI 6"; break;
                case "MU7": produit = "MULTI 7"; break;
                default: produit =code; break;
            }
            return produit;
        }

        private string _getCombinaisonType(string code)
        {
            string type;
            switch (code)
            {
                case "N":
                    type = "";
                    break;
                case "O":
                    type = "Ordre";
                    break;
                case "DO":
                    type = "Désordre";
                    break;
                case "B":
                    type = "Bonus";
                    break;
                case "B3":
                    type = "Bonus 3";
                    break;
                case "B4":
                    type = "Bonus 4";
                    break;
                default:
                    type = code;
                    break;
            }
            return type;
        }

        private double formatCote(string cote, bool reduce = false)
        {
            double c = 1.1;
            try
            {
                c = double.Parse(cote, CultureInfo.InvariantCulture);
                if (reduce) c = c < 1.1 ? 1.1 : c;
            }
            catch
            {}
            return c;
        }
     
        //private void hideLabels()
        //{
        //    lbltotalMiseProd2.Hide();
        //    lblProdName1.Hide();
        //    lblProdName2.Hide();
        //    lblC1.Hide();
        //    lblC2.Hide();
        //    lblC3.Hide();
        //    lblC4.Hide();
        //    lblC5.Hide();
        //    lblC6.Hide();
        //    lblC7.Hide();
        //    lblC8.Hide();
        //    lblC9.Hide();
        //    lblC10.Hide();
        //    lblC11.Hide();
        //    lblC12.Hide();
        //    lblC13.Hide();
        //    lblC14.Hide();
        //    lblC15.Hide();
        //    lblC16.Hide();
        //    lblC17.Hide();
        //    lblC18.Hide();
        //    lblC19.Hide();
        //    lblC20.Hide();
        //    lblCote1.Hide();
        //    lblCote2.Hide();
        //    lblCote3.Hide();
        //    lblCote4.Hide();
        //    lblCote5.Hide();
        //    lblCote6.Hide();
        //    lblCote7.Hide();
        //    lblCote8.Hide();
        //    lblCote9.Hide();
        //    lblCote10.Hide();
        //    lblCote11.Hide();
        //    lblCote12.Hide();
        //    lblCote13.Hide();
        //    lblCote14.Hide();
        //    lblCote15.Hide();
        //    lblCote16.Hide();
        //    lblCote17.Hide();
        //    lblCote18.Hide();
        //    lblCote19.Hide();
        //    lblCote20.Hide();
        //    lblCO1.Hide();
        //    lblCO2.Hide();
        //    lblCO3.Hide();
        //    lblCO4.Hide();
        //    lblCO5.Hide();
        //    lblCO6.Hide();
        //    lblCO7.Hide();
        //    lblCO8.Hide();
        //    lblCO9.Hide();
        //    lblCO10.Hide();
        //    lblCO11.Hide();
        //    lblCO12.Hide();
        //    lblCO13.Hide();
        //    lblCO14.Hide();
        //    lblCO15.Hide();
        //    lblCO16.Hide();
        //    lblCO17.Hide();
        //    lblCO18.Hide();
        //    lblCO19.Hide();
        //    lblCO20.Hide();
        //}
        
        private void hideRapportPanels()
        {
            panelR1.Hide();
            panelR2.Hide();
            panelR3.Hide();
            panelR4.Hide();
            panelR5.Hide();
            panelR6.Hide();
            panelR7.Hide();
            panelR8.Hide();
            panelR9.Hide();
            panelR10.Hide();
        }
    }
}
