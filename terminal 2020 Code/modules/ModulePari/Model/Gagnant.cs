using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModulePari.Model
{
    class Gagnant
    {
        private string idConc;
        private string nom;
        private string prenom;
        private string cin;
        private string numTele;
        private string commentaire;
        private DateTime dateReunion;
        private string idServeur;

        public string IdConc
        {
            get { return this.idConc; }
            set { this.idConc = value; }
        }
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public string Prenom
        {
            get { return this.prenom; }
            set { this.prenom = value; }
        }
        public string CIN
        {
            get { return this.cin; }
            set { this.cin = value; }
        }
        public string NumTele
        {
            get { return this.numTele; }
            set { this.numTele = value; }
        }
        public string Commentaires
        {
            get { return this.commentaire; }
            set { this.commentaire = value; }
        }
        public DateTime DateReunion
        {
            get { return this.dateReunion; }
            set { this.dateReunion = value; }
        }
        public string IdServeur
        {
            get { return this.idServeur; }
            set { this.idServeur = value; }
        }
       

    }
}
