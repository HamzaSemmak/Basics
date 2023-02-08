using calipso2020.Utils;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace sorec_gamma.modules.ModuleImpression
{
    public class ImpressionService
    {
        private static string ticketModelsPath = @"Images\ticketModels\";

        public static bool AvisGain(string v_nom, string v_prenom, string v_cin, string v_montant, string v_avance, string v_restant, string v_date_print, string v_piedPage, string v_piedPage2)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            bool isPrinted = false;
            using (var image = Image.FromFile(ticketModelsPath + "AvisGrosGain.bmp"))
            using (Bitmap bitmap = new Bitmap(image))
            {
                int Width = bitmap.Width;

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 20))
                    {
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;
                        StringFormat drawFormatRight = new StringFormat();
                        drawFormatRight.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                        DateTime datesysdate = Convert.ToDateTime(v_date_print);
                        string datesys = datesysdate.ToLongDateString().ToUpper() + " (J" + datesysdate.DayOfYear.ToString() + ")";
                        graphics.DrawString(datesys, arialFont, Brushes.Black, 120f, 80f);
                        graphics.DrawString("Nom", arialFont, Brushes.Black, 50f, 500f);
                        graphics.DrawString(v_nom.ToUpper(), arialFont, Brushes.Black, 170f, 500f);
                        graphics.DrawString("Prénom", arialFont, Brushes.Black, 50f, 540f);
                        graphics.DrawString(v_prenom.ToUpper(), arialFont, Brushes.Black, 170f, 540f);
                        graphics.DrawString("N° CIN", arialFont, Brushes.Black, 50f, 580f);
                        graphics.DrawString(v_cin.ToUpper(), arialFont, Brushes.Black, 170f, 580f);
                        graphics.DrawString("Montant total du gain", arialFont, Brushes.Black, 50f, 670f);
                        graphics.DrawString("DH", arialFont, Brushes.Black, 600f, 670f, drawFormatRight);
                        graphics.DrawString(v_montant, arialFont, Brushes.Black, 560f, 670f, drawFormatRight);
                        graphics.DrawString("Avance déjà payée", arialFont, Brushes.Black, 50f, 710f);
                        graphics.DrawString("DH", arialFont, Brushes.Black, 600f, 710f, drawFormatRight);
                        graphics.DrawString(v_avance, arialFont, Brushes.Black, 560f, 710f, drawFormatRight);
                        graphics.DrawString("Restant dû", arialFont, Brushes.Black, 50f, 750f);
                        graphics.DrawString("DH", arialFont, Brushes.Black, 600f, 750f, drawFormatRight);
                        graphics.DrawString(v_restant, arialFont, Brushes.Black, 560f, 750f, drawFormatRight);

                        Rectangle rect2 = new Rectangle(0, 830, Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        graphics.DrawString(datesysdate.ToString("dd/MM/yy HH:mm:ss") + "-" + v_piedPage, arialFont, Brushes.Black, rect2, stringFormat);

                    }
                    using (Font arialFont = new Font("Arial", 24, FontStyle.Bold))
                    {
                        graphics.DrawString("Bénéficiaire", arialFont, Brushes.Black, 250f, 470f);
                        graphics.DrawString("Gains", arialFont, Brushes.Black, 280f, 640f);
                    }

                    using (Font arialFont = new Font("Arial", 24, FontStyle.Bold))
                    {
                        Rectangle rect1 = new Rectangle(0, 870, Width, 30);
                        StringFormat stringFormat = new StringFormat();
                        stringFormat.Alignment = StringAlignment.Center;
                        stringFormat.LineAlignment = StringAlignment.Center;

                        graphics.DrawString(">> " + v_piedPage2 + " <<", arialFont, Brushes.Black, rect1, stringFormat);
                    }
                }
                using (Bitmap logo = new Bitmap(ticketModelsPath + "logo.bmp"))
                using (var result = FileUtils.AppendBitmap(logo, bitmap, 0))
                using (Bitmap newBmp = FileUtils.BitmapTo1Bpp(result))
                {
                    if (ApplicationContext.develop)
                        newBmp.Save(@"Images\tickets\LastAvisGain.bmp");
                    else
                        isPrinted = ApplicationContext.imprimante.ImprimerBMP(newBmp);
                }
            }
            return isPrinted || ApplicationContext.develop;
        }

        public static bool AvisDepot(string v_PV, string v_nom, string v_prenom, string v_cin, string v_montant, string v_transact, string v_date_print, string v_piedPage)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            DateTime datesysdate = Convert.ToDateTime(v_date_print);
            string datesys = datesysdate.ToLongDateString().ToUpper() + " (J" + datesysdate.DayOfYear.ToString() + ")";

            v_montant = string.Format("{0:n}", Convert.ToDecimal(v_montant)) + "DH";

            Bitmap image1bit = new Bitmap(ticketModelsPath + "DepotCompte.bmp", true);
            BitmapData bmpData = image1bit.LockBits(new Rectangle(0, 0, image1bit.Width, image1bit.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Bitmap image24bit = new Bitmap(image1bit.Width, image1bit.Height, bmpData.Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, bmpData.Scan0);

            Bitmap result;
            bool isPrinted = false;
            using (var bitmap = image24bit)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 20))
                    {
                        graphics.DrawString(datesys, arialFont, Brushes.Black, 90f, 70f);
                        graphics.DrawString("Point de vente", arialFont, Brushes.Black, 50f, 360f);
                        graphics.DrawString(v_PV, arialFont, Brushes.Black, 250f, 360f);
                        graphics.DrawString("Nom", arialFont, Brushes.Black, 50f, 490f);
                        graphics.DrawString(v_nom.ToUpper(), arialFont, Brushes.Black, 200f, 490f);
                        graphics.DrawString("Prénom", arialFont, Brushes.Black, 50f, 520f);
                        graphics.DrawString(v_prenom.ToUpper(), arialFont, Brushes.Black, 200f, 520f);
                        graphics.DrawString("N° CIN", arialFont, Brushes.Black, 50f, 550f);
                        graphics.DrawString(v_cin.ToUpper(), arialFont, Brushes.Black, 200f, 550f);
                        graphics.DrawString("Montant", arialFont, Brushes.Black, 50f, 690f);
                        graphics.DrawString(v_montant, arialFont, Brushes.Black, 250f, 690f);
                        graphics.DrawString("Transaction N°", arialFont, Brushes.Black, 50f, 720f);
                        graphics.DrawString(v_transact, arialFont, Brushes.Black, 250f, 720f);
                        graphics.DrawString(datesysdate.ToString("dd/MM/yy HH:mm:ss") + "-" + v_piedPage, arialFont, Brushes.Black, 30f, 770f);
                    }
                    using (Font arialFont = new Font("Arial", 24, FontStyle.Bold))
                    {
                        graphics.DrawString("Titulaire", arialFont, Brushes.Black, 250f, 430f);
                        graphics.DrawString("Dépôt", arialFont, Brushes.Black, 280f, 650f);
                    }
                }
                Bitmap logo = new Bitmap(ticketModelsPath + "logo.bmp");
                result = FileUtils.AppendBitmap(logo, bitmap, 0);
                using (Bitmap bmp1 = FileUtils.BitmapTo1Bpp(result))
                {
                    if (ApplicationContext.develop)
                        bmp1.Save(@"Images\tickets\LastAvisDepot.bmp");
                    else 
                        isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmp1);
                }
            }
            return isPrinted || ApplicationContext.develop;
        }

        public static bool CreationCompte(string v_PV, string v_nom, string v_prenom, string v_cin, string v_Date_naissance, string v_date_print, string v_piedPage)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            DateTime datesysdate = Convert.ToDateTime(v_date_print);
            string datesys = datesysdate.ToLongDateString().ToUpper() + " (J" + datesysdate.DayOfYear.ToString() + ")";

            Bitmap image1bit = new Bitmap(ticketModelsPath + "CreationCompte.bmp");
            BitmapData bmpData = image1bit.LockBits(new Rectangle(0, 0, image1bit.Width, image1bit.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            Bitmap image24bit = new Bitmap(image1bit.Width, image1bit.Height, bmpData.Stride, PixelFormat.Format24bppRgb, bmpData.Scan0);
            bool isPrinted = false;
            using (var bitmap = image24bit)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 20))
                    {
                        graphics.DrawString(datesys, arialFont, Brushes.Black, 120f, 70f);
                        graphics.DrawString("Point de vente", arialFont, Brushes.Black, 50f, 360f);
                        graphics.DrawString(v_PV, arialFont, Brushes.Black, 250f, 360f);
                        graphics.DrawString("Nom", arialFont, Brushes.Black, 50f, 480f);
                        graphics.DrawString(v_nom.ToUpper(), arialFont, Brushes.Black, 200f, 480f);
                        graphics.DrawString("Prénom", arialFont, Brushes.Black, 50f, 510f);
                        graphics.DrawString(v_prenom.ToUpper(), arialFont, Brushes.Black, 200f, 510f);
                        graphics.DrawString("N° CIN", arialFont, Brushes.Black, 50f, 540f);
                        graphics.DrawString(v_cin.ToUpper(), arialFont, Brushes.Black, 200f, 540f);
                        graphics.DrawString("Date de naissance", arialFont, Brushes.Black, 50f, 570f);
                        graphics.DrawString(v_Date_naissance, arialFont, Brushes.Black, 320f, 570f);
                        graphics.DrawString(datesysdate.ToString("dd/MM/yy HH:mm:ss") + "-" + v_piedPage, arialFont, Brushes.Black, 40f, 690f);
                    }
                    using (Font arialFont = new Font("Arial", 24, FontStyle.Bold))
                    {
                        graphics.DrawString("Titulaire", arialFont, Brushes.Black, 250f, 420f);
                    }
                }
                Bitmap logo = new Bitmap(ticketModelsPath + "logo.bmp");
                Bitmap result = FileUtils.AppendBitmap(logo, bitmap, 0);
                using (Bitmap bmp1 = FileUtils.BitmapTo1Bpp(result))
                {
                    if (ApplicationContext.develop)
                        bmp1.Save(@"Images\tickets\LastCreationCompte.bmp");
                    else isPrinted = ApplicationContext.imprimante.ImprimerBMP(bmp1);
                }
            }
            return isPrinted || ApplicationContext.develop;
        }
    }
}
