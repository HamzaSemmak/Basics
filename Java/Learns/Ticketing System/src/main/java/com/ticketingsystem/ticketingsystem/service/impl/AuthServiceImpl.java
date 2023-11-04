package com.ticketingsystem.ticketingsystem.service.impl;

import com.ticketingsystem.ticketingsystem.entity.User;
import com.ticketingsystem.ticketingsystem.repository.UserRepository;
import com.ticketingsystem.ticketingsystem.service.AuthService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

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
        User user = this.getUserByName(username);
        if (user != null) {
            BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
            if (passwordEncoder.matches(password, user.getPassword())) {
                this.user = user;
                return user;
            }
        }
        return null;
    }

    @Override
    public User getUserByName(String username) {
        return userRepository.findUserByName(username);
    }

    @Override
    public User resetPassword(User user, String password) {
        user.setPassword(password);
        userRepository.save(user);
        return user;
    }

    @Override
    public Optional<User> getUserByID(Long ID) {
        return userRepository.findById(ID).stream().findFirst();
    }


}
