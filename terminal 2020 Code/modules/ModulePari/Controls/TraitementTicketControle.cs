using System;
using sorec_gamma.modules.TLV;
using sorec_gamma.modules.ModulePari.Services;
using sorec_gamma.modules.UTILS;

namespace sorec_gamma.modules.ModulePari.Controls
{
    class TraitementTicketControle
    {
        public String  TraiterTicket(Ticket ticket, bool isManuel = true, bool isMachine = false) {
            TLVHandler appTagsHandler = new TLVHandler();
            TLVHandler TicketDetailsTlvHandler = new TLVHandler();
            appTagsHandler.add(TLVTags.SOREC_TYPE_MESSAGE, Constantes.TYPE_MESSAGE_REQUEST_TRAITEMENT_TICKET);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_TERMINAL_IP, ApplicationContext.IP);
            appTagsHandler.addASCII(TLVTags.SOREC_DATA_OFFRE, "1");
            try
            {
                TicketDetailsTlvHandler.add(TLVTags.SOREC_DATA_TICKET_IDCONC, long.Parse(ticket.IdTicket));
            } catch (Exception ex)
            {
                ApplicationContext.Logger.Error(string.Format("Ticket ID: {0}, Exception Trace: {1}", ticket.IdTicket, ex.StackTrace));
            }
            TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_DATEREUNION, ticket.DateReunion.ToString("yyyy-MM-dd"));
            if(isMachine)
                TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_TYPE_ANNUL, "MACHINE");
            else
                TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_TYPE_ANNUL, "CLIENT");
            if (isManuel)
                TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_MODE_OPERATION, "MANUEL");
            else
                TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_MODE_OPERATION, "AUTOMATIQUE");
            TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_IDSERVER, ticket.IdServeur);
            TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_CVNT, ticket.CVNT);
            TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_NUMTICKET_ANNUL, "123");
            TicketDetailsTlvHandler.addASCII(TLVTags.SOREC_DATA_TICKET_TYPECANAL, "TERMINAL");

            appTagsHandler.add(TLVTags.SOREC_DATA_TICKET, TicketDetailsTlvHandler);
            TLVHandler pari_annul_paiem_handler = isMachine ? null : TlvUtlis.Annul_paiement_sys_controle();
            if (pari_annul_paiem_handler != null)
            {
                //Ajouter le tag dans le tlv
                appTagsHandler.add(TLVTags.SOREC_DATA_ANNU_PAI_SYSTEME, pari_annul_paiem_handler);
            }
            String tlvData = appTagsHandler.toString();
            String tlvDataHash256 = Utils.getSha256(tlvData);
            byte[] macBytes = Utils.macSign(tlvDataHash256);
            string macString = Utils.bytesToHex(macBytes);
            String result = "";
            try
            {
                result = TraitementTicketService.SendRequest(tlvData, macString);
            }
            catch (Exception e)
            {
                ApplicationContext.Logger.Error("TraiterTicket Exception : " + e.Message);
            }
            return result;
        }

    }
}
