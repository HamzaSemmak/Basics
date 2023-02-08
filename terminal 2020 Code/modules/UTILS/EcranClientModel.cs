
namespace sorec_gamma.modules.UTILS
{
    public class EcranClientModel
    {
        public string SimpleMsg { get; set; }
        public string Date { get; set; }
        public string Hippo { get; set; }
        public string Reunion { get; set; }
        public string Course { get; set; }
        public string Paris { get; set; }
        public string Formulation { get; set; }
        public string Price { get; set; }
        public MessageType MessageType { get; set; }
        public decimal Distribution { get; set; }
        public decimal Paiement { get; set; }
        public decimal Annulation { get; set; }
        public int AnnulationCount { get; set; }
        public int DistributionCount { get; set; }
        public int PaiementCount { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;
            EcranClientModel comp = obj as EcranClientModel;

            return comp.MessageType == MessageType
                && comp.SimpleMsg == SimpleMsg
                && comp.Date == Date
                && comp.Hippo == Hippo
                && comp.Reunion == Reunion
                && comp.Course == Course
                && comp.Paris == Paris
                && comp.Formulation == Formulation
                && comp.Price == Price
                && comp.Distribution == Distribution
                && comp.DistributionCount == DistributionCount
                && comp.Paiement == Paiement
                && comp.PaiementCount == PaiementCount
                && comp.Annulation == Annulation
                && comp.AnnulationCount == AnnulationCount;
        }
    }
}
