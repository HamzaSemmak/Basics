using System;
using System.Collections.Generic;
using System.Text;
using sorec_gamma.modules.ModuleAuthentification;

namespace sorec_gamma.modules.ModulePari
{
    public class Formulation
    {
        private int id_formulation;
        private Produit produit;
        private bool formComplete;
        private String designation;
        private long miseCombinaison;
        private long miseTotal;
        private List<int> listeNonPartant;
        private bool formExpress;
        private bool chevalExpress;
        private List<int> listSindexes;
        //private bool champTotal_X;
        
        public Formulation()
        {
           
        }
        public Formulation(Formulation f)
        {
            this.produit = f.produit;
            this.formComplete = f.formComplete;
            this.designation = f.designation;
            this.miseCombinaison = f.miseCombinaison;
            this.miseTotal = f.miseTotal;
            this.listeNonPartant = f.listeNonPartant;
            this.formExpress = f.formExpress;
        }

        public int IdFormulation
        {
            get { return this.id_formulation; }
            set { this.id_formulation = value; }
        }
        
        public List<int> ListeNonPartant
        {
            get { return this.listeNonPartant; }
            set { this.listeNonPartant = value; }
        }
        public String Designation
        {
            get { return this.designation; }
            set { this.designation = value; }
        }

        public Produit Produit
        {
            get { return this.produit; }
            set { this.produit = value; }
        }
        public long MiseCombinaison
        {
            get { return this.miseCombinaison; }
            set { this.miseCombinaison = value; }
        }
        public long MiseTotal
        {
            get { return this.miseTotal; }
            set { this.miseTotal = value; }
        }
        public bool FormComplete
        {
            get { return this.formComplete; }
            set { this.formComplete = value; }
        }

        public bool FormuleExpress
        {
            get { return this.formExpress; }
            set { this.formExpress = value; }
        }

        public bool ChevalExpress
        {
            get { return this.chevalExpress; }
            set { this.chevalExpress = value; }
        }

        public List<int> ListSindexes
        {
            get { return this.listSindexes; }
            set { this.listSindexes = value; }
        }

        //public bool ChampTotal_X
        //{
        //    get { return this.champTotal_X; }
        //    set { this.champTotal_X = value; }
        //}

    }
}
