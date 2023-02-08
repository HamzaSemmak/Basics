using QRCoder;
using sorec_gamma.modules.Config;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.UTILS;
using System;
using System.Drawing;
using System.Threading;

namespace sorec_gamma.modules.ModuleImpression
{
    class ImprimerChequeJeuService
    {
        public bool ChequeJeux(Voucher voucher)
        {
            if (!ApplicationContext.IsPrinterInitialized())
                return false;
            bool result = false;
            using (Image image = Image.FromFile(@"Images\ticketModels\ChequeJeux.bmp", true))
            using (Bitmap bitmap = new Bitmap(image))
            {
                int bmpWidth = bitmap.Width;
                Bitmap finalBmp = null;
                try
                {
                    DateTime dateTime = voucher.DateEmission;
                    string text = dateTime.ToLongDateString().ToUpper() + " (J" + dateTime.DayOfYear.ToString() + ")";
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    using (StringFormat stringFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    })
                    using (SolidBrush sb = new SolidBrush(Color.Black))
                    {
                        voucher.IdVoucher = long.Parse(voucher.IdVoucher).ToString();
                        Rectangle rectangle = new Rectangle(0, 556, bmpWidth, 30);
                        Rectangle rectangle2 = new Rectangle(0, 590, bmpWidth, 30);
                        Rectangle rectangle3 = new Rectangle(0, 630, bmpWidth, 40);
                        Rectangle rectangle4 = new Rectangle(0, 145, bmpWidth, 50);
                        using (Font font2 = new Font("Arial", 40f, FontStyle.Bold))
                        {
                            graphics.DrawString(voucher.Montant.ToString(), font2, Brushes.Black, rectangle4, stringFormat);
                        }
                        using (Font font3 = new Font("Arial", 24f))
                        {
                            graphics.DrawString(voucher.DateExpiration.ToString("dd/MM/yyyy"), font3, Brushes.Black, 240f, 240f);
                        }
                        using (Font font = new Font("Arial", 20f))
                        {
                            graphics.DrawString(text, font, Brushes.Black, 140f, 70f);
                            graphics.DrawString(voucher.DateEmission.ToString("dd/MM/yy HH:mm:ss") + "-"
                                + ConfigUtils.ConfigData.Num_pdv + "."
                                + ConfigUtils.ConfigData.Pos_terminal + " - "
                                + ApplicationContext.SOREC_DATA_VERSION_LOG + ApplicationContext.SOREC_DATA_ENV,
                                font, Brushes.Black, rectangle, stringFormat);
                        }
                        using (Font font4 = new Font("Arial", 24f, FontStyle.Bold))
                        {
                            graphics.DrawString(">> 2." + voucher.DateSession.ToString("ddMMyy") + "." + voucher.IdServeur + "." + voucher.IdVoucher + "." + voucher.CVNT + " <<", font4, Brushes.Black, rectangle2, stringFormat);
                        }
                        string sDateForQRcode = "2F" + voucher.DateSession.ToString("ddMMyy");
                        string idServeur = voucher.IdServeur;
                        string idConc = voucher.IdVoucher;
                        string cvnt = voucher.CVNT;
                        while (idServeur.Length <= 2)
                        {
                            if (idServeur.Length == 2)
                            {
                                sDateForQRcode += idServeur;
                                break;
                            }
                            else
                            {
                                idServeur += "F";
                            }
                        }
                        while (idConc.Length <= 8)
                        {
                            if (idConc.Length == 8)
                            {
                                sDateForQRcode = sDateForQRcode + idConc + cvnt;
                                break;
                            }
                            else
                            {
                                idConc += "F";
                            }
                        }
                        while (sDateForQRcode.Length < 32)
                        {
                            sDateForQRcode += "F";
                        }

                        string DataEncrypted = "G" + DESOperations.Encrypt(sDateForQRcode);
                        using (Font font5 = new Font("Bahnschrift", 24f, FontStyle.Bold))
                        {
                            graphics.DrawString(DataEncrypted.Substring(1), font5, Brushes.Black, rectangle3, stringFormat);
                        }
                        using (Bitmap bitmap4 = GenerateMyQCCode(DataEncrypted))
                        using (Image logo = Image.FromFile(@"Images\ticketModels\Logo.bmp"))
                        using (Image imageLogo = resizeImage(logo, new Size(640, 200)))
                        using (Bitmap bitmap5 = new Bitmap(imageLogo))
                        using (Graphics graphics1 = Graphics.FromImage(bitmap5))
                        {
                            Rectangle rectangle5 = new Rectangle(400, 0, 200, 200);
                            graphics1.DrawImage(bitmap4, rectangle5);
                            finalBmp = AppendBitmap(bitmap5, bitmap, 0);
                        }
                        Thread thread = new Thread(() =>
                        {
                            using (Bitmap newBmp = GraphicUtils.ConvertBitmapTo1Bpp(finalBmp))
                            {
                                if (ApplicationContext.develop)
                                {
                                    newBmp.Save(@"Images\tickets\LastVoucher.bmp");
                                }
                                else if (ApplicationContext.IsPrinterInitialized())
                                {
                                    result = ApplicationContext.imprimante.ImprimerBMP(newBmp);
                                }
                            }
                        });
                        thread.Name = "PRINT_VOUCHER";
                        thread.Start();
                        thread.Join();
                    }
                }
                catch (Exception e)
                {
                    ApplicationContext.Logger.Error("Imprimer cheque jeu Exception : " + e.Message);
                }
                finally
                {
                    if (result || ApplicationContext.develop)
                    {
                        TerminalUtils.updateLastVoucherInfos(voucher);
                    }
                    if (finalBmp != null)
                    {
                        finalBmp.Dispose();
                    }
                }
            }
            return result || ApplicationContext.develop;
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
        private Bitmap GenerateMyQCCode(string QCText)
        {
            Bitmap qrBmp;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(QCText, QRCodeGenerator.ECCLevel.H);
                QRCode qrCode = new QRCode(qrCodeData);
                qrBmp = qrCode.GetGraphic(20);
            }
            return qrBmp;
        }

        private Bitmap AppendBitmap(Bitmap source, Bitmap target, int spacing)
        {
            int num = Math.Max(source.Width, target.Width);
            int num2 = source.Height + target.Height + spacing;
            Bitmap bitmap = new Bitmap(num, num2);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(source, 0, 0);
                graphics.DrawImage(target, 0, source.Height + spacing);
            }
            return bitmap;
        }
    }
}
