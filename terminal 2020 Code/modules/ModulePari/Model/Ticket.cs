
using System;
using System.Collections.Generic;
using System.Text;
using sorec_gamma.modules.ModulePari.Model;

namespace sorec_gamma.modules.ModulePari
{
    public class Ticket
    {
        private String idTicket;
       
        private DateTime dateReunion;
        private int numReunion;
        private Canal canal;
        // code Hippodrome 
        private String codeHippo;
        private int numCourse;
        // date validation de Pari
        private DateTime dateEmission;
        // Statut Ticket ==> valide ou annulé 
        private StatutTicket statut;
        private long prixTotalTicket;
        //PDV ayant effectué la transaction
        private string numPDV;
        //Position de Terminal
        private String posTerminal;
        
        //Date Paiement
        private DateTime datePaiement;
        // PDV ayant effectué le paiement ou annulation
        private int numPDVPaiement;
        private String cvnt;
        private String idServeur;
        // liste des formulation dans le Ticket
        private List<Formulation> listeFormulation = new List<Formulation>();

        private int numberPartant;

        public Ticket()
        {

        }
        public Ticket(Ticket t)
        {
            idTicket = t.idTicket;
            dateReunion = t.dateReunion;
            numReunion = t.numReunion;
            canal = t.canal;
            codeHippo = t.codeHippo;
            numCourse = t.numCourse;
            dateEmission = t.dateEmission;
            statut = t.statut;
            numPDV = t.numPDV;
            posTerminal = t.posTerminal;
            datePaiement = t.datePaiement;
            numPDVPaiement = t.numPDVPaiement;
            cvnt = t.cvnt;
            idServeur = t.idServeur;
            prixTotalTicket = t.prixTotalTicket;
            List<Formulation> listF = new List<Formulation>();
            foreach (Formulation f in t.ListeFormulation)
            {
                listF.Add(new Formulation(f));
            }
            listeFormulation = listF;
        }
        public String IdTicket {

            get { return this.idTicket; }
            set{ this.idTicket = value;}
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
        public Canal Canal {
            get { return this.canal; }
            set { this.canal = value; }
        }

        public long PrixTotalTicket
        {

            get { return this.prixTotalTicket; }
            set { this.prixTotalTicket = value; }
        }
        public DateTime DateReunion
        {

            get { return this.dateReunion; }
            set { this.dateReunion = value; }
        }
        public int NumReunion
        {

            get { return this.numReunion; }
            set { this.numReunion = value; }
        }
        public String CodeHippo
        {
            get { return this.codeHippo; }
            set { this.codeHippo = value; }
        }

        public int NumCourse
        {
            get { return this.numCourse; }
            set { this.numCourse = value; }
        }
        public DateTime DateEmission
        {
            get { return this.dateEmission; }
            set { this.dateEmission = value; }
        }
        public StatutTicket Statut
        {
            get { return this.statut; }
            set { this.statut = value; }
        }
        public string NumPDV
        {
            get { return this.numPDV; }
            set { this.numPDV = value; }
        }
        public int NumPDVPaiement
        {
            get { return this.numPDVPaiement; }
            set { this.numPDVPaiement = value; }
        }

        public DateTime DatePaiement
        {
            get { return this.datePaiement; }
            set { this.datePaiement = value; }
        }
        public String PosTerminal
        {
            get { return this.posTerminal; }
            set { this.posTerminal = value; }
        }
        public List<Formulation > ListeFormulation
        {
            get { return this.listeFormulation; }
            set { this.listeFormulation = value; }
        }

        public int NumberPartans
        {
            get { return this.numberPartant; }
            set { this.numberPartant = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID Ticket: " + idTicket);
            sb.AppendLine("DateReunion: " + dateReunion);
            sb.AppendLine("NumReunion: " + numReunion);
            sb.AppendLine("Canal: " + canal);
            sb.AppendLine("CodeHippo: " + codeHippo);
            sb.AppendLine("NumCourse: " + numCourse);
            sb.AppendLine("DateEmission: " + dateEmission);
            sb.AppendLine("Statut: " + statut);
            sb.AppendLine("PrixTotalTicket: " + prixTotalTicket);
            sb.AppendLine("Nombre Partans: " + numberPartant);
            return sb.ToString();
        }
    }
}
