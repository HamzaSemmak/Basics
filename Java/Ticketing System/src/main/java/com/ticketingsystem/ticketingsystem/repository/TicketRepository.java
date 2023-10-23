package com.ticketingsystem.ticketingsystem.repository;

import com.ticketingsystem.ticketingsystem.entity.Ticket;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface TicketRepository extends JpaRepository<Ticket, Long> {
}
