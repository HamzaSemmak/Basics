package com.ticketingsystem.ticketingsystem.controller;

import com.ticketingsystem.ticketingsystem.entity.Ticket;
import com.ticketingsystem.ticketingsystem.repository.TicketRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(path = "${proxy-config}/ticket")
public class TicketController {

    @Autowired
    private TicketRepository ticketRepository;

    /**
     * Gestion des Tickets :
     * Les utilisateurs peuvent créer de nouveaux tickets avec les informations suivantes :
     * - Titre
     * - Description
     * - Catégorie
     * - Priorité
     * - Pièces jointes
     * Les tickets doivent disposer d'options d'état telles que "Ouvert," "En cours," "Résolu" et "Fermé."
     *
     * Communication :
     * [Description de la communication, si nécessaire, peut être ajoutée ici.]
     *
     * @param ticket Le ticket.
     * @return [Description du résultat de la méthode, le cas échéant.]
     */
    @PostMapping(path = "/create")
    public String createTicket(@RequestBody Ticket ticket) {
        try {
            if(ticket == null) {
                return "ticket is null, Please Try again.";
            }
            else if(ticket.getTitle() == null || ticket.getTitle().isEmpty() ||
                    ticket.getDescription() == null || ticket.getDescription().isEmpty() ||
                    ticket.getCategory() == null || ticket.getCategory().isEmpty() ||
                    ticket.getPriority() == null || ticket.getPriority().isEmpty() ||
                    ticket.getStatus() == null || ticket.getStatus().isEmpty()
            ) {
                return "There is some field that is empty, Please try again";
            }
            else {
                ticketRepository.save(ticket);
                return "Ticket created successfully. " + ticket.toString();
            }
        }
        catch (Exception e) {
            return "Exception : " + e.getMessage();
        }
    }

}
