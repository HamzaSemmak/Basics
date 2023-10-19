package com.ticketingsystem.ticketingsystem.service.impl;

import com.ticketingsystem.ticketingsystem.entity.User;
import com.ticketingsystem.ticketingsystem.repository.UserRepository;
import com.ticketingsystem.ticketingsystem.service.AuthService;
import com.ticketingsystem.ticketingsystem.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class AuthServiceImpl implements AuthService {

    private User user;

    @Autowired
    private UserRepository userRepository;

    @Override
    public User register(User newUser) {
        user = userRepository.save(newUser);
        return  user;
    }

    @Override
    public User user() {
        return user;
    }

    @Override
    public User login(String username, String password) {
        User user = userRepository.findUserByName(username);
        if (user != null) {
            BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
            if (passwordEncoder.matches(password, user.getPassword())) {
                this.user = user;
                return user;
            }
        }
        return null;
    }
}
