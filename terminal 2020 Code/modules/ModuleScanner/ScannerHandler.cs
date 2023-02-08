using OnBarcode.Barcode.BarcodeScanner;
using sorec_gamma.modules.ModuleBackOffice.Controle;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModulePari.Controls;
using sorec_gamma.modules.ModulePari.Model;
using sorec_gamma.modules.TLV;
using System;
using System.Drawing;
using System.Globalization;

namespace sorec_gamma.modules.ModuleScanner
{
    class ScannerHandler
    {
        private static PaiementVoucherControle paiementControle = new PaiementVoucherControle();
        private static TraitementTicketControle traitementTicketControle = new TraitementTicketControle();

        private Ticket ticket;
        private Voucher voucher;

        public Ticket Ticket
        {
            get { return this.ticket; }
            set { this.ticket = value; }
        }
        public Voucher Voucher
        {
            get { return this.voucher; }
            set { this.voucher = value; }
        }
        public ScannerHandler()
        {
            ticket = new Ticket();
            voucher = new Voucher();
        }
        
        public string getScanedData(Bitmap sScanedBitmap)
        {
            string tlvData = null;
            try
            {
                string[] barcodes = barcodes = BarcodeScanner.Scan(sScanedBitmap, BarcodeType.QRCode);
               
                if (barcodes == null || barcodes.Length == 0)
                {
                    ApplicationContext.Logger.Warn("GetScanedData QRCode problème numérisation");
                }
                else
                {
                    ApplicationContext.Logger.Info("GetScanedData QRCode result : " + string.Join(", ", barcodes));

                    char[] chars = { 'F' };

                    string content = DESOperations.Decrypt(barcodes[0].Substring(1));
                    string typeTicket = content.Substring(0, 2).TrimEnd(chars);
                    int idTypeTicket = Int32.Parse(typeTicket);
                    DateTime dateReunion = DateTime.ParseExact(content.Substring(2, 6), "ddMMyy", CultureInfo.InvariantCulture);
                    string idServeur = content.Substring(8, 2).TrimEnd(chars);
                    string idTicket = content.Substring(10, 8).TrimEnd(chars);
                    string cvnt = content.Substring(18, 3).TrimEnd(chars);

                    switch (idTypeTicket)
                    {
                        case 1:
                            //Ticket jeu
                            ticket.DateReunion = dateReunion;
                            ticket.IdServeur = idServeur;
                            ticket.IdTicket = idTicket;
                            ticket.CVNT = cvnt;
                            string resultT = traitementTicketControle.TraiterTicket(ticket,false);
                            tlvData = TLVHandler.getTLVChamps(resultT);
                            break;

                        case 2:
                            voucher.IdServeur = idServeur;
                            voucher.IdVoucher = idTicket;
                            voucher.DateSession = dateReunion;
                            voucher.CVNT = cvnt;

                            string resultV = paiementControle.PayerVoucher(voucher,false);
                            tlvData = TLVHandler.getTLVChamps(resultV);
                            break;
                        default:
                            ApplicationContext.Logger.Error("GetScanedData Id type ticket not reconized:  " + idTypeTicket);
                            ApplicationContext.scanner.Eject(false);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error(string.Format("GetScanedData Exception Global: {0}, GetScanedData Exception Global Stack trace: {1}", e.Message, e.StackTrace));
            }
            return tlvData;

        }

    }
}
