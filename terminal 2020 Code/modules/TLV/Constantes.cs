using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.TLV
{
    class Constantes
    {
        //Request message types
        public const int TYPE_MESSAGE_REQUEST_AUTHENTICATION = 800;
        public const int TYPE_MESSAGE_REQUEST_ENREGISTREMENT_PARI = 801;
        public const int TYPE_MESSAGE_REQUEST_DIFFUSION_OFFRE = 802;
        public const int TYPE_MESSAGE_REQUEST_AUTHENTICATION_PEL = 803;
        public const int TYPE_MESSAGE_REQUEST_ANNULATION_PARI = 804;
        public const int TYPE_MESSAGE_REQUEST_PAIEMENT_PARI = 805;
        public const int TYPE_MESSAGE_REQUEST_DIFFUSION_SIGNAL = 806;
        public const int TYPE_MESSAGE_REQUEST_TRAITEMENT_TICKET = 807;
        public const int TYPE_MESSAGE_REQUEST_DEMANDE_RAPPORT = 808;
        public const int TYPE_MESSAGE_REQUEST_DEMANDE_COTE = 809;
        public const int TYPE_MESSAGE_REQUEST_CREATION_COMPTE = 810;
        public const int TYPE_MESSAGE_REQUEST_VERIFICATION_COMPTE = 811;
        public const int TYPE_MESSAGE_REQUEST_ALIMENTATION_COMPTE = 812;
        public const int TYPE_MESSAGE_REQUEST_DEMANDE_FSOLD = 813;
        public const int TYPE_MESSAGE_REQUEST_INFORMATION_GAGNANT = 814;
        public const int TYPE_MESSAGE_REQUEST_DEMANDE_VOUCHER = 815;
        public const int TYPE_MESSAGE_REQUEST_PAIEMENT_VOUCHER = 816;
        public const int TYPE_MESSAGE_REQUEST_ETAT_CAISSE = 817;
        public const int TYPE_MESSAGE_REQUEST_SOLDE_PREPAYE = 818;
        public const int TYPE_MESSAGE_REQUEST_RECEPTION_PANNES = 819;
        public const int TYPE_MESSAGE_REQUEST_MAJ_LOGICIEL = 820;

        //Responses messages types
        public const int TYPE_MESSAGE_RESPONSE_AUTHENTICATION = 900;
        public const int TYPE_MESSAGE_RESPONSE_ENREGISTREMENT_PARI = 901;
        public const int TYPE_MESSAGE_RESPONSE_DIFFUSION_OFFRE = 902;
        public const int TYPE_MESSAGE_RESPONSE_AUTHENTICATION_PEL = 903;
        public const int TYPE_MESSAGE_RESPONSE_ANNULATION_PARI = 904;
        public const int TYPE_MESSAGE_RESPONSE_PAIEMENT_PARI = 905;
        public const int TYPE_MESSAGE_RESPONSE_DIFFUSION_SIGNAL = 906;
        public const int TYPE_MESSAGE_RESPONSE_TRAITEMENT_TICKET = 907;
        public const int TYPE_MESSAGE_RESPONSE_DEMANDE_COTE = 908;
        public const int TYPE_MESSAGE_RESPONSE_DEMANDE_RAPPORT = 909;
        public const int TYPE_MESSAGE_RESPONSE_CREATION_COMPTE = 910;
        public const int TYPE_MESSAGE_RESPONSE_VERIFICATION_COMPTE = 911;
        public const int TYPE_MESSAGE_RESPONSE_ALIMENTATION_COMPTE = 912;
        public const int TYPE_MESSAGE_RESPONSE_DEMANDE_FSOLD = 913;
        public const int TYPE_MESSAGE_RESPONSE_INFORMATION_GAGNANT = 914;
        public const int TYPE_MESSAGE_RESPONSE_DEMANDE_VOUCHER = 915;
        public const int TYPE_MESSAGE_RESPONSE_PAIEMENT_VOUCHER = 916;
        public const int TYPE_MESSAGE_RESPONSE_ETAT_CAISSE = 917;
        public const int TYPE_MESSAGE_RESPONSE_SOLDE_PREPAYE = 918;
        public const int TYPE_MESSAGE_RESPONSE_RECEPTION_PANNES = 919;
        public const int TYPE_MESSAGE_RESPONSE_MAJ_LOGICIEL = 920;

        //Common OK Response codes
        public const int RESPONSE_CODE_OK = 200;
        public const int RESPONSE_CODE_OK_UPDATE_OFFRE = 201;

        //Common KO Response codes
        public const int RESPONSE_CODE_KO = 400;
        public const int RESPONSE_CODE_KO_MAC_INVALIDE = 401;

        //Detailed KO Response codes

        //Authentication
        public const int RESPONSE_CODE_KO_DATA_TERMINAL_INVALIDE = 4116;
        public const int RESPONSE_CODE_KO_MS_PARAMETRAGE_INJOIGNABLE = 4119;
        public const int RESPONSE_CODE_KO_MS_PDV_INJOIGNABLE = 4120;
        public const int RESPONSE_CODE_KO_PDV_INTROUVABLE = 4121;
        public const int RESPONSE_CODE_KO_UNOTHORIZED_VERSION = 4127;
        public const int RESPONSE_CODE_KO_PREPOSE_MDP_INVALIDE = 4128;
        //EnregistrementPari
        public const int RESPONSE_CODE_KO_NB_FORMULATIONS = 4101;
        public const int RESPONSE_CODE_KO_STATUT_PDV = 4102;
        public const int RESPONSE_CODE_KO_SOLDE_INSUFFISANT = 4103;
        public const int RESPONSE_CODE_KO_PRODUIT_INDISPONIBLE = 4104;
        public const int RESPONSE_CODE_KO_PARTICIPANT_INDISPONIBLE = 4105;
        public const int RESPONSE_CODE_KO_MISSING_TAG = 4106;
        public const int RESPONSE_CODE_KO_TYPECANAL_INVALIDE = 4107;
        public const int RESPONSE_CODE_KO_STATUT_COURSE_INVALIDE = 4108;
        public const int RESPONSE_CODE_KO_STATUT_PRODUIT_INVALIDE = 4109;
        public const int RESPONSE_CODE_KO_STATUT_PARTICIPANT_INVALIDE = 4110;
        public const int RESPONSE_CODE_KO_PARTICIPANT_INTROUVABLE = 4111;
        public const int RESPONSE_CODE_KO_PRODUIT_INTROUVABLE = 4112;
        public const int RESPONSE_CODE_KO_DESIGNATION_ABSENT = 4113;
        public const int RESPONSE_CODE_KO_COURSE_INTROUVABLE = 4114;
        public const int RESPONSE_CODE_KO_REUNION_INTROUVABLE = 4115;
        public const int RESPONSE_CODE_KO_MISE_INVALIDE = 4122;
        public const int RESPONSE_CODE_KO_NB_PARTANT_INVALIDE = 4145;
        //AnnulationTicket
        public const int RESPONSE_CODE_KO_MODEANNULATION_AUTO = 4117;
        public const int RESPONSE_CODE_KO_TICKET_INVALIDE = 4118;
        public const int RESPONSE_CODE_KO_TICKET_DEJA_ANNULE = 4129;
        public const int RESPONSE_CODE_KO_TICKET_PERDANT = 4155;

        //PaiementPari
        public const int RESPONSE_CODE_KO_CVNT = 4123;
        public const int RESPONSE_CODE_KO_STATUT_TICKET_INVALIDE = 4124;
        public const int RESPONSE_CODE_KO_AUTORISATION_GROS_GAIN = 4126;
        public const int RESPONSE_CODE_KO_PAIEMENT_INACTIF = 4156;
        public const int RESPONSE_CODE_KO_PAIEMENT_BLOQUE = 4160;
        public const int RESPONSE_CODE_KO_PAIEMENT_COURSE_INACTIF = 4162;

        //TraitementTicket
        public const int RESPONSE_CODE_KO_MODEOPERATION_INVALIDE = 4125;

        //Voucher
        public const int RESPONSE_CODE_KO_VOUCHER_INTROUVABLE = 4130;
        public const int RESPONSE_CODE_KO_VOUCHER_MONTANT_SUP_PLAFOND = 4131;
        public const int RESPONSE_CODE_KO_VOUCHER_MONTANT_INF_SEUIL = 4132;
        public const int RESPONSE_CODE_KO_VOUCHER_CVNV_INVALIDE = 4133;
        public const int RESPONSE_CODE_KO_VOUCHER_PAYE = 4134;
        public const int RESPONSE_CODE_KO_VOUCHER_BLOQUE = 4135;
        public const int RESPONSE_CODE_KO_VOUCHER_EXPIRE = 4136;
        public const int RESPONSE_CODE_KO_VOUCHER_INVALIDE = 4161;

        // Information gagnant
        public const int RESPONSE_CODE_KO_TICKET_NOT_FOUND = 4140;
        public const int RESPONSE_CODE_KO_TICKET_DEJA_PAYE = 4141;
        public const int RESPONSE_CODE_KO_INFORMATIONGAGNANT_DEJA_APPELE = 4142;
        public const int RESPONSE_CODE_KO_INFORMATIONGAGNANT_TYPEPAIEMENT = 4143;

        // Signal
        public const int RESPONSE_CODE_KO_SIGNAL_TIMEOUT = 4150;

        //Solde prepaye
        public const int RESPONSE_CODE_KO_PDV_NOT_PREPAYE = 4151;

        public const int RESPONSE_CODE_KO_LIEU_NOT_ALLOWED = 4152;

        public const int RESPONSE_CODE_KO_LIEU_ANNULATION_NOT_ALLOWED = 4158;

        public const int RESPONSE_CODE_KO_LIEU_PAIEMENT_NOT_ALLOWED = 4159;

        public const int RESPONSE_CODE_KO_PLAFOND_PREPAYE_DEPASSE = 4153;

        public const int RESPONSE_CODE_KO_PLAFOND_POSTPAYE_DEPASSE = 4157;

        // Gestion compte
        public const int RESPONSE_CODE_KO_AUTORISATION_ALLOJEU = 4154;

        //Update PDV
        public const int RESPONSE_CODE_KO_PDV_INEXISTANT = 4163;

        //OuvertureFermetureEnMasse (PDV)
        public const int RESPONSE_CODE_KO_PDV_STATUT_INVALIDE = 4164;
    }
}
