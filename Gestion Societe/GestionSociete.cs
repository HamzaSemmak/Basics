using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Societe
{
    internal class GestionSociete : SocieteDeConfection
    {
        private string[] collection;

        public string[] Collection { get => collection; set => collection = value; }

        public void ajouterSociete(SocieteDeConfection societeDeConfection)
        {
            int lenght = this.collection.Length - 1;
            this.collection[lenght] = societeDeConfection.NomDeSociete;
        }

    }
}
