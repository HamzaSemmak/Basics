using System;
using System.Collections.Generic;
using log4net;
using sorec_gamma.modules.ModulePari.Services;
using sorec_gamma.modules.TLV;

namespace sorec_gamma.modules.ModulePari.Controls
{
    class DiffusionOffreControl
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(DiffusionOffreControl));

        public Offre getDataOffreFromMT()
        {
            Offre offre = new Offre();
            try
            {
                TLVHandler DiffusionOffreTagsHandler = new TLVHandler();
                DiffusionOffreTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_DIFFUSION_OFFRE);
                String tlvData = DiffusionOffreTagsHandler.toString();
                String tlvDataHash256 = Utils.getSha256(tlvData);
                byte[] macBytes = Utils.macSign(tlvDataHash256);
                string macString = Utils.bytesToHex(macBytes);

                string data = DiffusionOffreService.SendRequest(tlvData, macString);

                String myTlv = TLVHandler.getTLVChamps(data);
                TLVHandler appTagsHandler = new TLVHandler(myTlv);
                TLVTags dataReponseTag = appTagsHandler.getTLV(TLVTags.SOREC_DATA_RESPONSE);
                if (dataReponseTag == null || dataReponseTag.length < 1)
                {
                    Logger.Error("dataReponseTag is empty");
                }
                else
                {
                    TLVHandler dataReponseTagHandler = new TLVHandler(Utils.bytesToHex(dataReponseTag.value));
                    TLVTags sorecOffreTag = dataReponseTagHandler.getTLV(TLVTags.SOREC_OFFRE);
                    if (sorecOffreTag == null || sorecOffreTag.length < 1)
                    {
                        Logger.Error("sorecOffreTag is empty");
                    }
                    else
                    {
                        TLVHandler sorecOffreTagHandler = new TLVHandler(Utils.bytesToHex(sorecOffreTag.value));
                        TLVTags dateOffreTag = sorecOffreTagHandler.getTLV(TLVTags.SOREC_OFFRE_TIMESTAMP);
                        if (dateOffreTag != null)
                            offre.Date_Offre = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dateOffreTag.value)));
                        else
                            Logger.Error("dateOffreTag is NULL");
                        TLVTags NumVersionTag = sorecOffreTagHandler.getTLV(TLVTags.SOREC_OFFRE_NUMVERSION);
                        if (NumVersionTag != null)
                            offre.NumVersion = int.Parse(Utils.bytesToHex(NumVersionTag.value));
                        else
                            Logger.Error("NumVersionTag is NULL");

                        TLVTags listereunionTag = sorecOffreTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNIONS);
                        if (listereunionTag == null)
                            Logger.Error("listereunionTag is NULL");
                        else
                        {
                            TLVHandler listeReuinionsHandler = new TLVHandler(Utils.bytesToHex(listereunionTag.value));
                            List<Reunion> listeReunion = new List<Reunion>();
                            List<TLVTags> reunionsTag = listeReuinionsHandler.getTLVList();
                            if (reunionsTag != null && reunionsTag.Count > 0)
                            {
                                foreach (var r in reunionsTag)
                                {
                                    if (r == null)
                                    {
                                        Logger.Error("reunion is NULL");
                                        continue;
                                    }
                                    List<Course> listeCourse = new List<Course>();
                                    if (r.length > 0)
                                    {
                                        TLVHandler reunionTagHandler = new TLVHandler(Utils.bytesToHex(r.value));

                                        TLVTags dateReunionTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_DATE);
                                        TLVTags reunionDiffusionTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_DIFFUSION);
                                        TLVTags numreunionTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_NUMERO);
                                        TLVTags reunionHippodromesTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_HIPPODROME);
                                        TLVTags reunionHippodromesORGTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_HIPPODROME_ORG);

                                        Reunion reunion = new Reunion();
                                        if (dateReunionTag == null)
                                            Logger.Error("dateReunionTag is NULL");
                                        else
                                            reunion.DateReunion = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(dateReunionTag.value)));
                                        if (reunionDiffusionTag == null)
                                            Logger.Error("reunionDiffusionTag is NULL");
                                        else
                                            reunion.Diffusion = Utils.HexToASCII(Utils.bytesToHex(reunionDiffusionTag.value));
                                        if (numreunionTag == null)
                                            Logger.Error("numreunionTag is NULL");
                                        else
                                            reunion.NumReunion = Int32.Parse(Utils.bytesToHex(numreunionTag.value));
                                        if (reunionHippodromesTag == null)
                                            Logger.Error("reunionHippodromesTag is NULL");
                                        else
                                            reunion.CodeHippo = Utils.HexToASCII(Utils.bytesToHex(reunionHippodromesTag.value));
                                        if (reunionHippodromesORGTag == null)
                                            Logger.Error("reunionHippodromesORGTag is NULL");
                                        else
                                            reunion.LibReunion = Utils.HexToASCII(Utils.bytesToHex(reunionHippodromesORGTag.value));

                                        TLVTags listeCourseTag = reunionTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSES);
                                        if (listeCourseTag == null)
                                        {
                                            Logger.Error("listeCourseTag is NULL");
                                        }
                                        else
                                        {
                                            TLVHandler listeCourseHandler = new TLVHandler(Utils.bytesToHex(listeCourseTag.value));
                                            List<TLVTags> coursesTag = listeCourseHandler.getTLVList();
                                            if (coursesTag == null || coursesTag.Count < 1)
                                            {
                                                Logger.Error("coursesTag is empty");
                                            }
                                            else
                                            {
                                                foreach (var c in coursesTag)
                                                {
                                                    if (c == null)
                                                    {
                                                        Logger.Error("course is NULL");
                                                        continue;
                                                    }
                                                    List<Produit> listeProduit = new List<Produit>();
                                                    List<Horse> listePartant = new List<Horse>();

                                                    TLVHandler courseTagHandler = new TLVHandler(Utils.bytesToHex(c.value));

                                                    TLVTags numCourseTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_NUMERO);
                                                    TLVTags nomCourseTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_NOM);
                                                    TLVTags heureCourseTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_HEURE);
                                                    TLVTags courseStatutTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_STATUT);

                                                    Course course = new Course();

                                                    if (numCourseTag == null)
                                                        Logger.Error("numCourseTag is NULL");
                                                    else
                                                        course.NumCoursePmu = Int32.Parse(Utils.bytesToHex(numCourseTag.value));
                                                    if (nomCourseTag == null)
                                                        Logger.Error("nomCourseTag is NULL");
                                                    else
                                                        course.LibCourse = Utils.HexToASCII(Utils.bytesToHex(nomCourseTag.value));
                                                    if (heureCourseTag == null)
                                                        Logger.Error("heureCourseTag is NULL");
                                                    else
                                                        course.HeureDepartCourse = Convert.ToDateTime(Utils.HexToASCII(Utils.bytesToHex(heureCourseTag.value)));
                                                    if (courseStatutTag == null)
                                                        Logger.Error("courseStatutTag is NULL");
                                                    else
                                                    {
                                                        string statutCourse = Utils.HexToASCII(Utils.bytesToHex(courseStatutTag.value));
                                                        switch (statutCourse)
                                                        {
                                                            case "ENV":
                                                                course.Statut = StatutCourse.EnVente;
                                                                break;
                                                            case "ARR":
                                                                course.Statut = StatutCourse.ArretVente;
                                                                break;
                                                            case "PDE":
                                                                course.Statut = StatutCourse.PreDepart;
                                                                break;
                                                            case "DEP":
                                                                course.Statut = StatutCourse.Depart;
                                                                break;
                                                            case "DEI":
                                                                course.Statut = StatutCourse.DeppartInterrompu;
                                                                break;
                                                            case "APR":
                                                                course.Statut = StatutCourse.ArriveeProvisoire;
                                                                break;
                                                            case "ADE":
                                                                course.Statut = StatutCourse.ArriveeDefinitive;
                                                                break;
                                                            case "DDX":
                                                                course.Statut = StatutCourse.DepartDansXX;
                                                                break;
                                                            case "ANN":
                                                                course.Statut = StatutCourse.ANN;
                                                                break;
                                                            case "CHA":
                                                                course.Statut = StatutCourse.CHA;
                                                                break;
                                                            default:
                                                                course.Statut = StatutCourse.UNDIFINED;
                                                                break;
                                                        }
                                                    }
                                                    TLVTags listeParticipantCourseTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PARTICIPANTS);
                                                    if (listeParticipantCourseTag == null)
                                                    {
                                                        Logger.Error("listeParticipantCourseTag is NULL");
                                                    }
                                                    else
                                                    {
                                                        TLVHandler listePartantHandler = new TLVHandler(Utils.bytesToHex(listeParticipantCourseTag.value));
                                                        List<TLVTags> participantTag = listePartantHandler.getTLVList();

                                                        if (participantTag == null || participantTag.Count < 1)
                                                        {
                                                            Logger.Error("produitsTag is empty");
                                                        }
                                                        else
                                                        {
                                                            foreach (var participant in participantTag)
                                                            {
                                                                if (participant == null)
                                                                {
                                                                    Logger.Error("participant is NULL");
                                                                    continue;
                                                                }
                                                                TLVHandler participantTagHandler = new TLVHandler(Utils.bytesToHex(participant.value));

                                                                TLVTags numParticipantTag = participantTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PARTICIPANT_NUMERO);
                                                                TLVTags nomParicipantTag = participantTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PARTICIPANT_NOM);
                                                                TLVTags ecurieParticpantTag = participantTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PARTICIPANT_ECURIE);
                                                                TLVTags statutPartantTag = participantTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PARTICIPANT_STATUT);

                                                                Horse partant = new Horse();
                                                                if (numParticipantTag == null)
                                                                    Logger.Error("numParticipantTag is null");
                                                                else
                                                                    partant.NumPartant = int.Parse(Utils.bytesToHex(numParticipantTag.value));
                                                                if (nomParicipantTag == null)
                                                                    Logger.Error("nomParicipantTag is null");
                                                                else
                                                                    partant.NomPartant = Utils.HexToASCII(Utils.bytesToHex(nomParicipantTag.value));
                                                                if (ecurieParticpantTag == null)
                                                                    Logger.Error("ecurieParticpantTag is null");
                                                                else
                                                                    partant.Ecurie_Part = Utils.HexToASCII(Utils.bytesToHex(ecurieParticpantTag.value));
                                                                if (statutPartantTag == null)
                                                                    Logger.Error("statutPartantTag is null");
                                                                else
                                                                {
                                                                    string statutPartant = Utils.HexToASCII(Utils.bytesToHex(statutPartantTag.value));
                                                                    switch (statutPartant)
                                                                    {
                                                                        case "PAR":
                                                                            partant.EstPartant = StatutPartant.EstPartant;
                                                                            break;
                                                                        case "NPA":
                                                                            partant.EstPartant = StatutPartant.NonPartant;
                                                                            break;
                                                                    }
                                                                }
                                                                listePartant.Add(partant);
                                                            }
                                                        }
                                                    }

                                                    TLVTags listeProduitsCourseTag = courseTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PRODUITS);
                                                    if (listeProduitsCourseTag == null)
                                                    {
                                                        Logger.Error("listeProduitsCourseTag is empty");
                                                    }
                                                    else
                                                    {
                                                        TLVHandler listeProduitHandler = new TLVHandler(Utils.bytesToHex(listeProduitsCourseTag.value));
                                                        List<TLVTags> produitsTag = listeProduitHandler.getTLVList();
                                                        if (produitsTag == null || produitsTag.Count < 1)
                                                        {
                                                            Logger.Error("produitsTag is empty");
                                                        }
                                                        else
                                                        {
                                                            foreach (var produit in produitsTag)
                                                            {
                                                                if (produit == null)
                                                                {
                                                                    Logger.Error("produit is NULL");
                                                                    continue;
                                                                }

                                                                TLVHandler produitTagHandler = new TLVHandler(Utils.bytesToHex(produit.value));
                                                                TLVTags produitCodeTag = produitTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PRODUIT_CODE);
                                                                string codePR = null;
                                                                if (produitCodeTag == null)
                                                                {
                                                                    Logger.Error("produitCodeTag is NULL");
                                                                }
                                                                else
                                                                {
                                                                    codePR = Utils.HexToASCII(Utils.bytesToHex(produitCodeTag.value));
                                                                }
                                                                Produit pr = ApplicationContext.SOREC_DATA_ATTRIBUTAIRE.GetProduit(codePR, reunion.Diffusion.Equals("M"));
                                                                if (pr == null)
                                                                {
                                                                    Logger.Warn(string.Format("Product not allowed: {0} - {1}", codePR, reunion.ToString()));
                                                                }
                                                                else
                                                                {

                                                                    TLVTags produitStatutTag = produitTagHandler.getTLV(TLVTags.SOREC_OFFRE_REUNION_COURSE_PRODUIT_STATUT);
                                                                    if (produitStatutTag == null)
                                                                    {
                                                                        Logger.Error("produitStatutTag is NULL");
                                                                        pr.Statut = StatutProduit.Undefined;
                                                                    }
                                                                    else
                                                                    {
                                                                        string statutProduit = Utils.HexToASCII(Utils.bytesToHex(produitStatutTag.value));
                                                                        switch (statutProduit)
                                                                        {
                                                                            case "ENV":
                                                                                pr.Statut = StatutProduit.EnVenteProduit;
                                                                                break;
                                                                            case "ARR":
                                                                                pr.Statut = StatutProduit.ArretVenteProduit;
                                                                                break;
                                                                            case "REM":
                                                                                pr.Statut = StatutProduit.Remboursement;
                                                                                break;
                                                                            case "REA":
                                                                                pr.Statut = StatutProduit.RemboursementArrete;
                                                                                break;
                                                                            case "PAI":
                                                                                pr.Statut = StatutProduit.Paiement;
                                                                                break;
                                                                            default:
                                                                                pr.Statut = StatutProduit.Undefined;
                                                                                break;
                                                                        }
                                                                    }
                                                                    listeProduit.Add(pr);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    course.ListeHorses = listePartant;
                                                    course.ListeProduit = listeProduit;
                                                    listeCourse.Add(course);
                                                }
                                            }
                                        }
                                        reunion.ListeCourse = listeCourse;
                                        listeReunion.Add(reunion);
                                    }
                                    offre.ListeReunion = listeReunion;
                                }
                            }
                            else
                                Logger.Error("reunionsTag is empty");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Exception Duffision Offre Control: " + e.StackTrace + e.Message);
            }
            if (offre != null)
            {
                ApplicationContext.CURRENT_NUMOFFRE_VERSION = offre.NumVersion;
            }
            return offre;
        }

        public bool Contains(Produit pr, List<Produit> produits)
        {
            foreach (Produit p in produits)
            {
                if (p.CodeProduit.Equals(pr.CodeProduit))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
