package com.ticketingsystem.ticketingsystem.controller;

import com.ticketingsystem.ticketingsystem.entity.User;
import com.ticketingsystem.ticketingsystem.service.impl.AuthServiceImpl;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(path = "${proxy-config}/authentication")
public class AuthController {

    private final Logger logger = LoggerFactory.getLogger(AuthController.class);

    @Autowired
    private AuthServiceImpl authService;

    /**
     * Authenticate a user based on their username and password.
     *
     * @param username The username of the user.
     * @param password The user's password.
     * @return void.
      */

}
