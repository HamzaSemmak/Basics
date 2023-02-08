using System;
using System.Collections.Generic;
using sorec_gamma.modules.ModulePari;
using System.Drawing;
using sorec_gamma.modules.TLV;
using System.Drawing.Imaging;
using sorec_gamma.modules.UTILS;
using System.Threading;
using QRCoder;
using sorec_gamma.modules.Config;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerTicket
    {
        private static string imageFilePath = @"Images\ticketModels\ticketModel.bmp";
        private static string lastPariTicketPath = @"Images\tickets\LastPari.bmp";

        //int counter = 0;

        public static bool imprimerTicket(Ticket ticket, string coprs, int countPartant, List<int> multiplicators)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            ApplicationContext.Logger.Info("Start ImprimerTicket : " + ticket.ToString());
            bool success = false;
            int SPACE = 15;
            int marginLeft = 18;
            int width = 640;

            using (Image image = Image.FromFile(imageFilePath))
            using (Bitmap bitmap = new Bitmap(image))
            {
                using (StringFormat stringFormatFar = new StringFormat
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Far
                })
                using (StringFormat stringFormatNear = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Near
                })
                using (StringFormat stringFormatCenter = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                using (SolidBrush sb = new SolidBrush(Color.Black))
                using (Font fBody3 = new Font("Lucida Console", 20, FontStyle.Bold))
                using (Font fArial = new Font("Arial", 22, FontStyle.Bold, GraphicsUnit.Point))
                using (Font fArial26 = new Font("Arial", 26, FontStyle.Bold, GraphicsUnit.Point))
                using (Font fBody4 = new Font("Lucida Console", 20, FontStyle.Regular))
                using (Graphics g = Graphics.FromImage(bitmap))
                {

                    SPACE += 5;
                    Rectangle rectQ = new Rectangle(8, SPACE, width, 30);
                    g.DrawString(">>  " + "1." + ticket.DateReunion.ToString("ddMMyy") + "." + ticket.IdServeur + "."
                        + Int64.Parse(ticket.IdTicket) + "." + ticket.CVNT + "  <<", fArial, sb, rectQ, stringFormatCenter);

                    SPACE += 50;
                    Rectangle rectHippo = new Rectangle(225, SPACE, width, 40);
                    g.DrawString(ticket.CodeHippo.ToUpper(), fArial26, sb, rectHippo, stringFormatNear);

                    SPACE += 40;
                    Rectangle rectDate = new Rectangle(225, SPACE, width, 40);
                    string dateR = ticket.DateReunion.ToString("dddd, dd MMMM yyyy") + " (J" + ticket.DateReunion.DayOfYear.ToString() + ")";
                    if (dateR.Length > 28)
                    {
                        using (Font fDate = new Font("Lucida Console", 16, FontStyle.Regular))
                        {
                            g.DrawString(dateR.ToUpper(), fDate, sb, rectDate, stringFormatNear);
                        }
                    }
                    else
                    {
                        using (Font fDate = new Font("Lucida Console", 18, FontStyle.Regular))
                        {
                            g.DrawString(dateR.ToUpper(), fDate, sb, rectDate, stringFormatNear);
                        }
                    }
                    SPACE += 40;
                    Rectangle rectCourse = new Rectangle(225, SPACE, width, 40);
                    g.DrawString("Réunion" + ticket.NumReunion + "- Course " + ticket.NumCourse, fBody3, sb, rectCourse, stringFormatNear);

                    SPACE += 40;
                    Rectangle rectPart = new Rectangle(225, SPACE, width, 40);
                    g.DrawString(countPartant + " DECLARES PARTANTS", fBody4, sb, rectPart, stringFormatNear);

                    SPACE += 65;
                    for (int j = 0; j < ticket.ListeFormulation.Count; j++)
                    {
                        Rectangle rectProduct = new Rectangle(marginLeft, SPACE, width, 50);
                        if (ticket.ListeFormulation[j].Produit.NomProduit == "QUARTE PLUS" || ticket.ListeFormulation[j].Produit.NomProduit == "QUINTE PLUS")
                        {
                            string prodName = StringUtils.Truncate(getNameProduit(ticket.ListeFormulation[j].Produit.NomProduit), 8);
                            g.DrawString(prodName, fArial, sb, rectProduct, stringFormatNear);
                        }
                        else
                        {
                            string prodName = StringUtils.Truncate(ticket.ListeFormulation[j].Produit.NomProduit, 8);
                            g.DrawString(prodName, fArial, sb, rectProduct, stringFormatNear);
                        }
                        Rectangle rectEnjeu = new Rectangle(0, SPACE, width, 30);
                        g.DrawString("Enjeu " + (ticket.ListeFormulation[j].MiseCombinaison) + " DH", fBody4, sb, rectEnjeu, stringFormatCenter);
                        Rectangle rectAmount = new Rectangle(0, SPACE, width, 35);
                        g.DrawString(ticket.ListeFormulation[j].MiseTotal + " DH", fArial, sb, rectAmount, stringFormatFar);

                        SPACE += 17;
                        Rectangle rectLigne = new Rectangle(marginLeft, SPACE, width, 30);
                        g.DrawString("---------------------------------------------------------------------------------------", fArial, sb, rectLigne, stringFormatNear);

                        SPACE += 25;
                        Rectangle rectDesignation = new Rectangle(marginLeft + 20, SPACE, width, 60);
                        string designation = ticket.ListeFormulation[j].Designation;

                        if (ticket.ListeFormulation[j].FormuleExpress)
                        {
                            designation = designation.Replace(" ", "*");
                        }

                        if (ticket.ListeFormulation[j].ChevalExpress)
                        {
                            foreach (int i in ticket.ListeFormulation[j].ListSindexes) 
                            {
                                designation = (i != (designation.Length - 1)) ? designation.Remove(i + 1, 1).Insert(i + 1, "*") : designation += "*";
                            }
                        }

                        if (designation.Length > 45)
                        {
                            using (Font fBody6 = new Font("Lucida Console", 18, FontStyle.Bold))
                            {
                                g.DrawString(designation, fBody6, sb, rectDesignation, stringFormatNear);
                            }
                        }
                        else
                        {
                            g.DrawString(designation, fBody3, sb, rectDesignation, stringFormatNear);
                        }

                        if (ticket.ListeFormulation[j].FormComplete)
                        {
                            SPACE += 25;
                            g.DrawString("Formule complète", fBody4, sb, 50, SPACE);
                        }
                        SPACE += 40;
                    }
                    string idServeur = ticket.IdServeur;
                    string CVNT = ticket.CVNT;
                    string dataNotEncrypted = "1F" + ticket.DateReunion.ToString("ddMMyy");
                    dataNotEncrypted += StringUtils.AddSuffix(idServeur, 'F', 2);
                    dataNotEncrypted += StringUtils.AddSuffix(ticket.IdTicket, 'F', 8);
                    dataNotEncrypted += CVNT;
                    dataNotEncrypted = StringUtils.AddSuffix(dataNotEncrypted, 'F', 32);
                    string DataEncrypted = "G" + DESOperations.Encrypt(dataNotEncrypted);

                    Rectangle rectEn = new Rectangle(marginLeft, SPACE, width, 35);
                    g.DrawString("ENJEU TOTAL", fArial, sb, rectEn, stringFormatNear);

                    Rectangle rect = new Rectangle(0, SPACE, width, 40);
                    g.DrawString(ticket.PrixTotalTicket + " DH", fArial26, sb, rect, stringFormatFar);

                    SPACE += 20;
                    Rectangle rectLigne1 = new Rectangle(marginLeft, SPACE, width, 30);
                    g.DrawString("---------------------------------------------------------------------------------------", fArial, sb, rectLigne1, stringFormatNear);

                    SPACE += 40;
                    Rectangle rectD = new Rectangle(8, SPACE, width, 30);
                    g.DrawString(ticket.DateEmission.ToString("dd/MM/yyyy HH:mm:ss") + "-" + ConfigUtils.ConfigData.Num_pdv + "." +
                        ConfigUtils.ConfigData.Pos_terminal + " - " +
                        ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV, fBody3, sb, rectD, stringFormatCenter);

                    SPACE += 45;
                    Rectangle rectEncr = new Rectangle(8, SPACE, width, 30);
                    g.DrawString(DataEncrypted.Substring(1), fBody3, sb, rectEncr, stringFormatCenter);
                    if (!coprs.Equals(""))
                    {
                        using (Font font1 = new Font("Arial", 24, FontStyle.Regular, GraphicsUnit.Point))
                        {
                            SPACE += 50;
                            Rectangle rect1 = new Rectangle(8, SPACE, width, 40);
                            g.DrawString(coprs, font1, sb, rect1, stringFormatCenter);
                        }
                    }

                    SPACE += 45;
                    Rectangle rectangle = new Rectangle(230, SPACE, 200, 200);
                    using (Bitmap bitmap4 = GenerateMyQCCode(DataEncrypted))
                        g.DrawImage(bitmap4, rectangle);

                    SPACE += 350;
                    Bitmap finalBitmap = null;
                    if (SPACE < 1500)
                    {
                        Rectangle recRes = new Rectangle(0, 0, width, SPACE);
                        finalBitmap = CropImage(bitmap, recRes);
                    }
                    else
                    {
                        finalBitmap = bitmap;
                    }
                    Thread thread = new Thread(() =>
                    {
                        try
                        {
                            using (Bitmap bmpNew = GraphicUtils.ConvertBitmapTo1Bpp(finalBitmap))
                            {
                                if (ApplicationContext.develop)
                                {
                                    bmpNew.Save(lastPariTicketPath);
                                }
                                else if (ApplicationContext.IsPrinterInitialized())
                                {
                                    success = ApplicationContext.imprimante.ImprimerBMP(bmpNew);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ApplicationContext.Logger.Error(string.Format("Impression: Calipso Exception => {0}, StackTrace: {1}", e.Message, e.StackTrace));
                        }
                        finally
                        {
                            if (success || ApplicationContext.develop)
                            {
                                TerminalUtils.updateLastTicketInfos(ticket);
                            }
                            finalBitmap.Dispose();
                            TlvUtlis.listSindexes.Clear();
                        }
                    });
                    thread.Name = "PRINT_TICKET";
                    thread.Start();
                    thread.Join();
                }
            }
            ApplicationContext.Logger.Info(string.Format("End ImprimerTicket : {0}", success));
            return success || ApplicationContext.develop;
        }

        private static Bitmap GenerateMyQCCode(string QCText)
        {
            /*BarcodeWriter barcodeWriter = new BarcodeWriter();
            QrCodeEncodingOptions expr_0C = new QrCodeEncodingOptions();
            expr_0C.Height = 100;
            expr_0C.Width =  100;
            QrCodeEncodingOptions options = expr_0C;
            barcodeWriter.Options = options;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            return barcodeWriter.Write(QCText);*/
            Bitmap qrBmp;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(QCText, QRCodeGenerator.ECCLevel.H);
                QRCode qrCode = new QRCode(qrCodeData);
                qrBmp = qrCode.GetGraphic(20);
            }
            return qrBmp;
        }

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }

        private static Bitmap CropImage(Bitmap img, Rectangle cropArea)
        {
            return img.Clone(cropArea, img.PixelFormat);
        }

        private static Bitmap ConvertTo1Bpp(Bitmap bmp)
        {
            int w = bmp.Width;
            int h = bmp.Height;
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            Bitmap cropped = new Bitmap(w, h, bmpData.Stride, PixelFormat.Format1bppIndexed, bmpData.Scan0);
            bmp.UnlockBits(bmpData);
            return cropped;
        }
        private static string getNameProduit(string nomProduit)
        {
            string produit;
            switch (nomProduit)
            {
                case "QUARTE PLUS": produit = "QUARTE +"; break;
                case "QUINTE PLUS": produit = "QUINTE +"; break;
                default: produit = nomProduit; break;
            }
            return produit;
        }
    }
}
