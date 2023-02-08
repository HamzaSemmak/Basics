namespace sorec_gamma.modules.ModulePari
{
    public class Produit
    {
        private int id_produit;
        private string codeProduit;
        private string nomProduit;
        private int nombreBase;
        private int enjeuMin;
        private int enjeuMax;
        private bool ordre;
        private bool chevalExpress;
        private bool champX;
        private int nbrJeux;
        private StatutProduit statut;
        private string genre;
        public int CiriteresOrdre { get; set; }

        public Produit()
        {
        }
        public Produit(Produit produit)
        {
            id_produit = produit.id_produit;
            codeProduit = produit.codeProduit;
            nomProduit = produit.nomProduit;
            nombreBase = produit.nombreBase;
            enjeuMin = produit.enjeuMin;
            enjeuMax = produit.enjeuMax;
            ordre = produit.ordre;
            chevalExpress = produit.chevalExpress;
            champX = produit.champX;
            nbrJeux = produit.nbrJeux;
            statut = produit.statut;
            genre = produit.genre;
            CiriteresOrdre = produit.CiriteresOrdre;
        }
        public StatutProduit Statut
        {
            get { return statut; }
            set { statut = value; }
        }
        public string NomProduit
        {
            get { return nomProduit; }
            set { nomProduit = value; }
        }
        public int NombreBase
        {
            get { return nombreBase; }
            set { nombreBase = value; }
        }
        public int NbrJeux
        {
            get { return nbrJeux; }
            set { nbrJeux = value; }
        }
        public int IdProduit
        {
            get { return id_produit; }
            set { id_produit = value; }
        }
        public int EnjeuMin
        {
            get { return enjeuMin; }
            set { enjeuMin = value; }
        }
        public string CodeProduit
        {
            get { return codeProduit; }
            set { codeProduit = value; }
        }
        public int EnjeuMax
        {
            get { return enjeuMax; }
            set { enjeuMax = value; }
        }
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }
        public bool Ordre
        {
            get { return ordre; }
            set { ordre = value; }
        }
        public bool ChevalExpress
        {
            get { return chevalExpress; }
            set { chevalExpress = value; }
        }
        public bool ChampX
        {
            get { return champX; }
            set { champX = value; }
        }

        public bool IsMulti()
        {
            return CodeProduit == "ML4" || CodeProduit == "ML5" || CodeProduit == "ML6" || CodeProduit == "ML7";
        }
    }
}
