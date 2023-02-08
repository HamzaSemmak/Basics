using sorec_gamma.modules.Config;
using sorec_gamma.modules.UTILS;
using System;
using System.Drawing;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerEtatDeCaisse
    {
        private string ModelFilePath = @"Images\ticketModels\EtatCaisse.bmp";
        private string lastetatcaisse = @"Images\tickets\EtatCaisse.bmp";

        public bool imprimerEtatCaisse(int nombreDist, string cummulDist, int nombreAnnu, string cummulAnnu,
            int nombreVoucher, string cummulVoucher, int nombrePVoucher,
            string cummulPVoucher, string total, int nombre,
            int caisse_depot_Nombre, string caisse_depot_Cummul,
            int caisse_annuSys_Nombre, string caisse_annuSys_Cummul,
            int caisse_paiements_Nombre, string caisse_paiements_Cummul,
            string year, string month, string day,
            int caisse_annuMachine_Nombre, string caisse_AnnulMachine_Cummul )
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;

            DateTime dateTime = DateTime.Now;
            string text = dateTime.ToString("dd/MM/yyy H:m:ss-") + " " 
                + ConfigUtils.ConfigData.Num_pdv + "." 
                + ConfigUtils.ConfigData.Pos_terminal + "-" 
                + ApplicationContext.SOREC_DATA_VERSION_LOG
                + ApplicationContext.SOREC_DATA_ENV
                + " (J" + dateTime.DayOfYear.ToString() + ")";
            bool isPrinted = false;

            using (Image img = Image.FromFile(ModelFilePath))
            using (Image image = resizeImage(img, new Size(640, 880)))
            using (Bitmap bitmap = new Bitmap(image))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                string st1;
                if (nombre > 0)
                {
                    st1 = "ETAT DE CAISSE DE PDV";
                    using (Font font = new Font("Arial", 24f, FontStyle.Bold))
                    {
                        graphics.DrawString(st1, font, Brushes.Black, 120f, 140f);
                    }
                }
                else
                {
                    st1 = "ETAT DE CAISSE ";
                    using (Font font = new Font("Arial", 24f, FontStyle.Bold))
                    {
                        graphics.DrawString(st1, font, Brushes.Black, 180f, 140f);
                    }
                }

                using (Font font = new Font("Arial", 16f))
                {
                    graphics.DrawString(text, font, Brushes.Black, 100f, 180f);
                    if (!year.Equals("") || !month.Equals("") || !day.Equals(""))
                    {
                        string dateEtatCaisse = "Etat de caisse du " + day + "/" + month + "/" + year;
                        graphics.DrawString(dateEtatCaisse, font, Brushes.Black, 100f, 210f);
                    }

                }
                using (Font font2 = new Font("Arial", 14f))
                {
                    graphics.DrawString("  Nombre ", font2, Brushes.Black, 300f, 260f);
                    graphics.DrawString("  CUMULS ", font2, Brushes.Black, 500f, 260f);
                    graphics.DrawString("  ------------------------------------------------------------------------------------------ ",
                        font2, Brushes.Black, 20, 280);

                    graphics.DrawString("  DISTRIBUTIONS ", font2, Brushes.Black, 60, 320f);
                    graphics.DrawString("  ANNULATIONS CLIENT ", font2, Brushes.Black, 60, 360f);
                    graphics.DrawString("  PAIEMENTS ", font2, Brushes.Black, 60, 400f);
                    graphics.DrawString("  CHEQUES PARIS EMIS  ", font2, Brushes.Black, 60, 440f);
                    graphics.DrawString("  CHEQUES PARIS RECUS  ", font2, Brushes.Black, 60, 480f);
                    graphics.DrawString("  DEPOT SUR COMPTES  ", font2, Brushes.Black, 60, 520f);
                    graphics.DrawString("  ANNULATIONS SYSTEME  ", font2, Brushes.Black, 60, 560f);
                    graphics.DrawString("  ANNULATIONS MACHINE  ", font2, Brushes.Black, 60, 600f);


                    graphics.DrawString(nombreDist.ToString(), font2, Brushes.Black, 350, 320f);
                    graphics.DrawString(nombreAnnu.ToString(), font2, Brushes.Black, 350, 360f);
                    graphics.DrawString(caisse_paiements_Nombre.ToString(), font2, Brushes.Black, 350, 400f);
                    graphics.DrawString(nombreVoucher.ToString(), font2, Brushes.Black, 350, 440f);
                    graphics.DrawString(nombrePVoucher.ToString(), font2, Brushes.Black, 350, 480f);
                    graphics.DrawString(caisse_depot_Nombre.ToString(), font2, Brushes.Black, 350, 520f);
                    graphics.DrawString(caisse_annuSys_Nombre.ToString(), font2, Brushes.Black, 350, 560f);
                    graphics.DrawString(caisse_annuMachine_Nombre.ToString(), font2, Brushes.Black, 350, 600f);

                    graphics.DrawString("  ------------------------------------------------------------------------------------------ ",
                        font2, Brushes.Black, 20, 630);

                }
                using (Font font2 = new Font("Arial", 14f, FontStyle.Bold))
                {
                    graphics.DrawString(cummulDist + "DH", font2, Brushes.Black, 480, 320f);
                    graphics.DrawString(("-" + cummulAnnu) + "DH", font2, Brushes.Black, 480, 360f);
                    graphics.DrawString("-" + caisse_paiements_Cummul + "DH", font2, Brushes.Black, 480, 400f);
                    graphics.DrawString(cummulVoucher + "DH", font2, Brushes.Black, 480, 440f);
                    graphics.DrawString("-" + cummulPVoucher + "DH", font2, Brushes.Black, 480, 480f);
                    graphics.DrawString(caisse_depot_Cummul + "DH", font2, Brushes.Black, 480, 520f);
                    graphics.DrawString("-" + caisse_annuSys_Cummul + "DH", font2, Brushes.Black, 480, 560f);
                    graphics.DrawString("-" + caisse_AnnulMachine_Cummul + "DH", font2, Brushes.Black, 480, 600f);
                }
                using (Font font = new Font("Arial", 16f, FontStyle.Bold))
                {
                    graphics.DrawString(" TOTAL  ", font, Brushes.Black, 60, 670f);
                    graphics.DrawString(total + "DH", font, Brushes.Black, 460, 670f);
                }
                Bitmap bmpNew = null;
                try
                {
                    bmpNew = GraphicUtils.ConvertBitmapTo1Bpp(bitmap);
                    if (ApplicationContext.develop)
                        bmpNew.Save(lastetatcaisse);
                    else
                    {
                        isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                    }
                }
                catch (Exception e)
                {
                    ApplicationContext.Logger.Error("Imprimer Etat de caisse Exception : " + e.Message);
                }
                finally
                {
                    if (bmpNew != null)
                    {
                        bmpNew.Dispose();
                    }
                }
            }
            return isPrinted || ApplicationContext.develop;
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
    }
}
