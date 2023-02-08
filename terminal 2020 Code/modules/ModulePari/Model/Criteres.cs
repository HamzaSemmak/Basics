using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.ModulePari.Model
{
    class Criteres
    {
        public Course Course { get; set; }
        public string Hipodrome { get; set; }
        public DateTime DateReunion { get; set; }
        public int NumReunion { get; set; }

        public Criteres()
        {

        }
        public Criteres(Course course, string hipodrome, DateTime dateReunion, int numReunion)
        {
            Course = course;
            Hipodrome = hipodrome;
            DateReunion = dateReunion;
            NumReunion = numReunion;
            foreach(Produit p in course.ListeProduit)
            {
                p.CiriteresOrdre = GetCritiresOrder(p.CodeProduit);
            }
        }

        private int GetCritiresOrder(string productCode)
        {
            switch (productCode)
            {
                case "GAG": return 0;
                case "PLA": return 1;
                case "JUG": return 2;
                case "JUP": return 3;
                case "JUO": return 4;
                case "TRO": return 5;
                case "TRC": return 6;
                case "QUU": return 7;
                case "QIP": return 8;
                case "QAP": return 9;
                default: return 100;
            }
        }

    }
}
