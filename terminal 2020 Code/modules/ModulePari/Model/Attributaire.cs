using System;
using System.Collections.Generic;
using System.Linq;
namespace sorec_gamma.modules.ModulePari
{
    public class Attributaire
    {
        private int idAttributaire;
        private string libelle;
        private string version;
        private string code;
        private List<Produit> produitsMA = new List<Produit>();
        private List<Produit> produitsFR = new List<Produit>();

        public int IdAttributaire {
            get { return this.idAttributaire; }
            set { this.idAttributaire = value; }

        }
        public String Libelle
        {
            get { return this.libelle; }
            set { this.libelle = value; }
        }
        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public List<Produit> ProditMA
        {
            get { return this.produitsMA; }
            set { this.produitsMA = value; }

        }
        public List<Produit> ProditFR
        {
            get { return this.produitsFR; }
            set { this.produitsFR = value; }

        }

        public Attributaire()
        {
            // TODO: Complete member initialization
        }

        public Produit GetProduit(string code, bool isMa)
        {
            Produit prod = null;
            if (isMa)
            {
                Produit prodMA = produitsMA.Where(pr => pr.CodeProduit == code).FirstOrDefault();
                if (prodMA != null)
                {
                    prod = new Produit(prodMA);
                }
            }
            else
            {
                Produit prodFR = produitsFR.Where(pr => pr.CodeProduit == code).FirstOrDefault();
                if (prodFR != null)
                {
                    prod = new Produit(prodFR);
                }
            }
            return prod;
        }
    }
}
