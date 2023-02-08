using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModulePari.Model
{
    public class Voucher
    {
        private String idVoucher;
        private DateTime dateEmission;
        private DateTime dateSession;
        private DateTime dateExpiration;
        private decimal montant ;
        private String cvnt;
        private String idServeur;
        public Voucher()
        {
        }
        public Voucher(Voucher v)
        {
            idVoucher = v.idVoucher;
            dateEmission = v.dateEmission;
            dateSession = v.dateSession;
            dateExpiration = v.dateExpiration;
            montant = v.montant;
            cvnt = v.cvnt;
            idServeur = v.idServeur;
        }

        public String IdVoucher
        {

            get { return this.idVoucher; }
            set { this.idVoucher = value; }
        }

        public String IdServeur
        {

            get { return this.idServeur; }
            set { this.idServeur = value; }
        }
        public String CVNT
        {

            get { return this.cvnt; }
            set { this.cvnt = value; }
        }
        

        public decimal Montant
        {
            get { return this.montant; }
            set { this.montant = value; }
        }
     
        public DateTime DateEmission
        {
            get { return this.dateEmission; }
            set { this.dateEmission = value; }
        }     
        public DateTime DateSession
        {
            get { return this.dateSession; }
            set { this.dateSession = value; }
        }
        public DateTime DateExpiration
        {
            get { return this.dateExpiration; }
            set { this.dateExpiration = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Id Voucher: " + idVoucher);
            sb.AppendLine("Date Emission: " + dateEmission);
            sb.AppendLine("Date Session: " + dateSession);
            sb.AppendLine("Date Expiration: " + dateExpiration);
            sb.AppendLine("Montant: " + montant);
            sb.AppendLine("CVNT: " + cvnt);
            sb.AppendLine("Id Serveur: " + idServeur);
            return sb.ToString();
        }
    }
}
