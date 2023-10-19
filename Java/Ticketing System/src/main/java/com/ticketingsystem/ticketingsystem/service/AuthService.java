package com.ticketingsystem.ticketingsystem.service;

import com.ticketingsystem.ticketingsystem.entity.User;

public interface AuthService {

    User register(User user);

    User user();

    User login(String username, String password);

}
