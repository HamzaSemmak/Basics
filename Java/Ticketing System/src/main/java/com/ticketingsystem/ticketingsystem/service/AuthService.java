package com.ticketingsystem.ticketingsystem.service;

import com.ticketingsystem.ticketingsystem.entity.User;

import java.util.Optional;

public interface AuthService {

    User register(User user);

    User user();

    User login(String username, String password);

    User getUserByName(String username);

    User resetPassword(User user, String password);

    Optional<User> getUserByID(Long ID);

}
