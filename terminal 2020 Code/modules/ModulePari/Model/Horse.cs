using System;

namespace sorec_gamma.modules.ModulePari
{
    public class Horse
    {
        private int numPartant;
        private string nomPartant;
        private StatutPartant estPartant;
        private string ecurie_part;

        public int NumPartant {
            get { return this.numPartant; }
            set { this.numPartant = value;  }
        }
        public string EcuriePart
        {
            get { return this.ecurie_part; }
            set { this.ecurie_part = value; }
        }

        public string NomPartant
        {
            get { return this.nomPartant; }
            set { this.nomPartant = value; }
        }

        public string Ecurie_Part
        {
            get { return this.ecurie_part; }
            set { this.ecurie_part = value; }
        }

        public StatutPartant EstPartant
        {
            get { return this.estPartant; }
            set { this.estPartant = value; }
        }


    }
}
