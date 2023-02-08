namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.models
{
    public class ProduitModel
    {
        public string Code { get; set; }

        public override string ToString()
        {
            return "Code: " + Code;
        }
    }
}
