package com.ticketingsystem.ticketingsystem.service.impl;

import com.ticketingsystem.ticketingsystem.entity.Ticket;
import com.ticketingsystem.ticketingsystem.repository.TicketRepository;
import com.ticketingsystem.ticketingsystem.service.TicketService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class TicketServiceImpl implements TicketService {

    @Autowired
    private TicketRepository ticketRepository;

    @Override
    public Ticket createTicket(Ticket ticket) {
        Ticket newTickets = ticketRepository.save(ticket);
        return newTickets;
    }
}
