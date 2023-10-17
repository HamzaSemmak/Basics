package com.ticketingsystem.ticketingsystem.controller;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class TicketController {

    private static final Logger logger = LoggerFactory.getLogger(TicketController.class);

    @GetMapping(path = "api/test")
    public String hello() {
        logger.info("first logs info");
        logger.trace("first logs trace");
        logger.debug("first logs debug");
        logger.error("first logs error");
        logger.warn("first logs warn");
        return "This is test";
    }
}
