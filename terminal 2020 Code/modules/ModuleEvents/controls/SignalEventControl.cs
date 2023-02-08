using sorec_gamma.modules.ModuleCoteRapport.models;
using sorec_gamma.modules.ModuleEvents.CotesEvents.models;
using sorec_gamma.modules.ModuleEvents.SignauxEvents.models;
using sorec_gamma.modules.ModulePari;
using sorec_gamma.modules.ModuleCoteRapport;
using System.Linq;
using System.Collections.Generic;
using sorec_gamma.modules.ModuleEvents.helpers;
using System.Text;

namespace sorec_gamma.modules.ModuleEvents.SignauxEvents.controls
{
    public class SignalEventControl
    {
        public static CourseData CourseInfoPushed = new CourseData();
        public static void HandleSignal(SignalEventModel sem)
        {
            if (sem == null)
            {
                ApplicationContext.Logger.Warn("SignalEventModel est nul");
                return;
            }
            ApplicationContext.Logger.Info("Signal Infos: " + sem.ToString());
            if (sem.Version != null)
            {
                if (ApplicationContext.CURRENT_NUMOFFRE_VERSION == 0)
                {
                    ApplicationContext.CURRENT_NUMOFFRE_VERSION = sem.Version.NumVersion;
                }
                else
                {
                    ApplicationContext.CURRENT_NUMOFFRE_VERSION++;
                    if ((sem.Version.NumVersion - ApplicationContext.CURRENT_NUMOFFRE_VERSION) != 0
                        && ApplicationContext.PrincipaleForm != null
                        && ApplicationContext.isAuthenticated)
                    {
                        ApplicationContext.PrincipaleForm.DiffuserOffre();
                    }
                }
                string messageEcran = "";
                switch (sem.Type)
                {
                    case SignalType.COURSE:
                        StatutCourse statut = getStatusCourse(sem.Course.Statut);
                        string status = getStatusText(statut);
                        messageEcran = status + "\n\n" + sem.Course.Resultats;
                        updateLastCourseData(sem);
                        UpdateCourse(sem);
                        if (ApplicationContext.PrincipaleForm != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            _ = sb.Append("Course R" + (sem.NumeroReunion <= 9 ? "0" + sem.NumeroReunion : "" + sem.NumeroReunion));
                            _ = sb.Append(".C" + (sem.NumeroCourse <= 9 ? "0" + sem.NumeroCourse : "" + sem.NumeroCourse));
                            _ = sb.Append("\n" + status);
                            ApplicationContext.PrincipaleForm.SignalLoaded(sb.ToString(), statut == StatutCourse.Depart);
                        }
                        break;
                    case SignalType.PRODUIT:
                        updateLastCourseData(sem);
                        UpdateProduit(sem);
                        if (ApplicationContext.PrincipaleForm != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            _ = sb.Append("Produits R" + (sem.NumeroReunion <= 9 ? "0" + sem.NumeroReunion : "" + sem.NumeroReunion));
                            _ = sb.Append(".C" + (sem.NumeroCourse <= 9 ? "0" + sem.NumeroCourse : "" + sem.NumeroCourse));
                            ApplicationContext.PrincipaleForm.SignalLoaded(sb.ToString());
                        }
                        break;
                    case SignalType.PARTANT:
                        messageEcran = getPartantsText(sem.Partants);
                        updateLastCourseData(sem);
                        UpdatePartant(sem);
                        if (ApplicationContext.PrincipaleForm != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            _ = sb.Append("Partants R" + (sem.NumeroReunion <= 9 ? "0" + sem.NumeroReunion : "" + sem.NumeroReunion));
                            _ = sb.Append(".C" + (sem.NumeroCourse <= 9 ? "0" + sem.NumeroCourse : "" + sem.NumeroCourse));
                            ApplicationContext.PrincipaleForm.SignalLoaded(sb.ToString());
                        }
                        break;
                    case SignalType.SESSION:
                        ApplicationContext.CloseSession("Suspension Guichet.");
                        break;
                    default:
                        break;
                }
                if (sem.Type == SignalType.COURSE || sem.Type == SignalType.PARTANT)
                {
                    CourseSignal signal = new CourseSignal();
                    signal.CodeHippo = sem.LibReunion;
                    signal.DateReunion = sem.DateReunion;
                    signal.NumReunion = "" + sem.NumeroReunion;
                    signal.NumCourse = "" + sem.NumeroCourse;
                    signal.Message = messageEcran;
                    _ = ApplicationContext.SIGNALS.AddOrUpdateSignals(signal);
                }
            }
        }

        public static void HandleRapport(CourseRapport courseRapport)
        {
            ApplicationContext.Logger.Info("Signal Rapport: " + courseRapport == null ? "Null" : courseRapport.ToString());
            _ = ApplicationContext.RAPPORTS.AddOrUpdateCourseRapports(courseRapport);
        }

        public static void HandleCotes(CotesEventModel cem)
        {
            foreach (CoteEventModel cote in cem.Cotes)
            {
                ProduitCote produitCote = new ProduitCote(cote.CodeProd, cote.DateReceptionCote, cote.Mises);
                foreach (CombinaisonEventModel combinaison in cote.Combinaisons)
                {
                    CombinaisonCote co = new CombinaisonCote(combinaison.Comb, combinaison.Cote,
                        combinaison.Mises, cote.CodeProd);
                    _ = produitCote.AddOrUpdateCombCotes(co);
                }
                CourseCote courceCote = new CourseCote(cote.NumReunion, cote.NumCourse);
                _ = courceCote.AddOrUpdateProduitCotes(produitCote);
                ReunionCote reCote = new ReunionCote(cote.NumReunion, cote.LibReunion, cote.DateReunion);
                _ = reCote.AddOrUpdateCourseCotes(courceCote);
                _ = ApplicationContext.COTES.AddOrUpdateReunionCotes(reCote);
            }
        }

        private static void UpdateCourse(SignalEventModel sem)
        {
            Course course = getCourse(sem);
            if (course != null)
            {
                course.Resultat = sem.Course.Resultats;
                course.Statut = getStatusCourse(sem.Course.Statut);
            }
        }

        private static Course getCourse(SignalEventModel sem)
        {
            Offre offre = ApplicationContext.SOREC_DATA_OFFRE;
            if (offre == null)
            {
                return null;
            }

            Course course = offre.GetCourseByNumReunionAndNumCourse(sem.DateReunion, sem.NumeroReunion, sem.NumeroCourse);
            if (course == null)
            {
                ApplicationContext.Logger.Warn("Course " + sem.NumeroCourse + " R" + sem.NumeroReunion + " Non trouvée");
            }
            return course;
        }

        private static void UpdatePartant(SignalEventModel sem)
        {
            Course course = getCourse(sem);
            if (course != null)
            {
                foreach (PartantEventModel partantEvent in sem.Partants)
                {
                    SaveOrUpdatePartant(course, partantEvent);
                }
            }
        }

        private static void UpdateProduit(SignalEventModel sem)
        {
            if (sem == null || sem.Produit == null || sem.Produit.Produits == null)
            {
                return;
            }
            Course course = getCourse(sem);
            if (course != null)
            {
                foreach (ProduitModel produit in sem.Produit.Produits)
                {
                    if (produit == null)
                    {
                        continue;
                    }
                    Produit p = getProduitByCode(course, produit.Code);
                    if (p != null)
                    {
                        p.Statut = getStatusProduit(sem.Produit.Statut);
                    }
                }
            }
        }

        private static void SaveOrUpdatePartant(Course c, PartantEventModel partantEvent)
        {
            Horse p = c.GetHorseByNum(partantEvent.Numero);
            if (p == null)
            {
                p = new Horse
                {
                    NomPartant = partantEvent.Nom,
                    Ecurie_Part = partantEvent.Ecurie,
                    EstPartant = getStatusPartant(partantEvent.Statut),
                    NumPartant = partantEvent.Numero
                };
                c.ListeHorses.Add(p);
            }
            else
            {
                p.EstPartant = getStatusPartant(partantEvent.Statut);
            }
        }

        private static Produit getProduitByCode(Course course, string codeProduit)
        {
            return course.ListeProduit.Where(p => p.CodeProduit == codeProduit).FirstOrDefault();
        }
        private static StatutCourse getStatusCourse(string statutCourse)
        {
            switch (statutCourse)
            {
                case "ENV":
                    return StatutCourse.EnVente;
                case "ARR":
                    return StatutCourse.ArretVente;
                case "PDE":
                    return StatutCourse.PreDepart;
                case "DEP":
                    return StatutCourse.Depart;
                case "DEI":
                    return StatutCourse.DeppartInterrompu;
                case "APR":
                    return StatutCourse.ArriveeProvisoire;
                case "ADE":
                    return StatutCourse.ArriveeDefinitive;
                case "DDX":
                    return StatutCourse.DepartDansXX;
                case "ANN":
                    return StatutCourse.ANN;
                case "CHA":
                    return StatutCourse.CHA;
                default:
                    return StatutCourse.UNDIFINED;
            }
        }

        private static StatutPartant getStatusPartant(string statusPartant)
        {
            switch (statusPartant)
            {
                case "PAR":
                    return StatutPartant.EstPartant;
                case "NPA":
                    return StatutPartant.NonPartant;
                default:
                    return StatutPartant.Undefined;
            }
        }

        private static StatutProduit getStatusProduit(string statusProduit)
        {
            switch (statusProduit)
            {
                case "ENV":
                    return StatutProduit.EnVenteProduit;
                case "ARR":
                    return StatutProduit.ArretVenteProduit;
                case "REM":
                    return StatutProduit.Remboursement;
                case "REA":
                    return StatutProduit.RemboursementArrete;
                case "PAI":
                    return StatutProduit.Paiement;
                default:
                    return StatutProduit.Undefined;
            }
        }

        private static string getPartantsText(List<PartantEventModel> partants)
        {
            string nParts = "";
            List<string> nPartsList = partants.Where(par => par.Statut == "NPA")
                .Select(par => par.Numero.ToString())
                .ToList();
            if (nPartsList.Count > 0)
                nParts = nPartsList.Aggregate((i, j) => i + " - " + j);

            string parts = "";
            List<string> partsList = partants.Where(par => par.Statut == "PAR")
                .Select(par => par.Numero.ToString())
                .ToList();
            if (partsList.Count > 0)
                parts = partsList.Aggregate((i, j) => i + " - " + j);
           
            return (string.IsNullOrEmpty(nParts) ? "" : "Non partants: " + nParts)
                + (string.IsNullOrEmpty(parts) ? "" : "\n\nNouveaux partants: " + parts);
        }
         
        private static string getStatusText(StatutCourse status)
        {
            switch (status)
            {
                case StatutCourse.EnVente:
                    return "Mise en vente";
                case StatutCourse.ArretVente:
                    return "Arrêt de vente";
                case StatutCourse.Depart:
                    return "Partie";
                case StatutCourse.PreDepart:
                    return "Départ dans 3 minutes";
                case StatutCourse.ArriveeDefinitive:
                    return "Arrivée définitive";
                case StatutCourse.ArriveeProvisoire:
                    return "Arrivée provisoire";
                case StatutCourse.DepartDansXX:
                    return "Départ dans 3 minutes";
                case StatutCourse.CHA:
                    return "Arrivée modificative";
                case StatutCourse.ANN:
                    return "Annulée";
                default:
                    return "";
            }
        }

        private static void updateLastCourseData(SignalEventModel sem)
        {
            if (ApplicationContext.SOREC_DATA_OFFRE == null)
            {
                return;
            }

            CourseInfoPushed.DateReunion = sem.DateReunion;
            CourseInfoPushed.NumeroCourse = sem.NumeroCourse;
            CourseInfoPushed.NumeroReunion = sem.NumeroReunion;
            CourseInfoPushed.ListReunionCount = ApplicationContext.SOREC_DATA_OFFRE.ListeReunion.Count;
            CourseInfoPushed.StatusCourse = SignalType.COURSE.Equals(sem.Type) && sem.Course != null ? sem.Course.Statut : "";
        }
    }
}
