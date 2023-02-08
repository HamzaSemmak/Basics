using CS_CLIB;
using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.ModulePari.Controls
{
    class ReglesPariControles
    {
        Tracing logger = new Tracing();

        public long factorial(int n)
        {

            if (n <= 1) return 1;
            else return n * factorial(n - 1);

        }

        public long Combinaison(int k, int n)
        {
            long var1 = factorial(n - k);
            long var2 = factorial(k);
            long var3 = factorial(n);
            return (var3) / (var2 * var1);

        }

        public long Max(long a, long b)
        {
            if (a > b) return a;
            return b;
        }

        /// <summary>
        /// Retourne Nombre de Champs X dans une formulation .
        /// </summary>
        public int getNbrVariablesX(List<Object> formulation)
        {

            int count = 0;
            int s = 0;
            while (count < formulation.Count)
            {
                if (formulation[count].Equals("X"))
                    s++;
                count++;

            }
            return s;
        }
        public Reunion getReunionByNuM(List<Reunion> lsReunion, int num)
        {
            foreach (Reunion reu in lsReunion)
            {
                if (reu.NumReunion.Equals(num))
                {
                    return reu;
                }
            }
            return null;
        }
        public int getindiceReunion(List<Reunion> lsReunion, int num)
        {
            int indice = 0;
            foreach (Reunion reu in lsReunion)
            {
                if (reu.NumReunion.Equals(num))
                {
                    return indice;
                }
                indice++;

            }
            return -1;
        }
        public Course getCourseByNum(Reunion r, int num)
        {
            foreach (Course c in r.ListeCourse)
            {
                if (c.NumCoursePmu.Equals(num))
                {
                    return c;
                }
            }
            return null;
        }
        public int getindiceCourse(Reunion r, int num)
        {
            int indice = 0;
            foreach (Course c in r.ListeCourse)
            {
                if (c.NumCoursePmu.Equals(num))
                {
                    return indice;
                }
                indice++;
            }
            return -1;
        }
        public int getindicePartant(List<Horse> ls, int num)
        {
            int indice = 0;
            foreach (Horse p in ls)
            {
                if (p.NumPartant.Equals(num))
                {
                    return indice;
                }
                indice++;
            }
            return -1;
        }
        public int getNbrChevalJoueWithR(List<Object> formulation)
        {
            int count = 0;
            int s = 0;
           
            while (count < formulation.Count && !formulation[count].Equals("R") && !formulation[count].Equals("TOTAL"))
            {
                s++;
                count++;
            }
            return s;
        }
        /// <summary>
        /// Retourne Nombre de chevaux joués dans une formulation, on compte le X.
        /// </summary>
        public int getNbrChevalJoue(List<Object> formulation)
        {
            int count = 0;
            int s = 0;
            while (count < formulation.Count && !formulation[count].Equals("R") && !formulation[count].Equals("TOTAL"))
            {
                s++;
                count++;

            }
            return s;
        }

        /// <summary>
        /// Retourne les valeurs possibles que X peut prendre dans une combinaison
        /// </summary>

        public int getVariablesX(List<Object> formulation, int nbrPartantCombinaison ,int nbrDesPartantCourse, int nbreChevalReduit)
        {
            int s = 0;
            if (formulation.Contains("X") && !formulation.Contains("R"))
            {
                s = nbrDesPartantCourse - nbrPartantCombinaison;
            }
            else if (formulation.Contains("R"))
            {
                s = nbreChevalReduit;
            }
            else s = 0;
            return s;
        }

        /// <summary>
        /// Retourne le nombre de chevaux joués dans une combinaison 
        /// </summary>
        public int getNbrPartantInCombinaison(List<object> formulations)
        {
            int s = 0;
            foreach (object formulation in formulations)
            {
                if (formulation == null)
                    continue;
                else if (formulation.Equals("R") || formulation.Equals("TOTAL"))
                    break;
                else if (!formulation.Equals("X"))
                    s++;
            }

            return s;
        }
        /// <summary>
        /// Retourne  Designation de Formulation 
        /// </summary>
        public String getPari(List<Object> formulation)
        {

            string listpari = "";
            for (int i = 0; i < formulation.Count; i++)
            {
                if (i != 0)
                    listpari += "  ";
                listpari += formulation[i].ToString();
            }
            return listpari;
        }

        public int addMultiplicateur(int m, List<int> listeMultiplicateurs)
        {
            int flag = 0;
            if (listeMultiplicateurs.Contains(m))
                flag = 1;

            if (flag == 0)
                listeMultiplicateurs.Add(m);

            else
                listeMultiplicateurs.Remove(m);
            return flag;
        }
    }
}
