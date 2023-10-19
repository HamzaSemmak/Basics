package com.ticketingsystem.ticketingsystem.service.impl;

import com.ticketingsystem.ticketingsystem.entity.User;
import com.ticketingsystem.ticketingsystem.repository.UserRepository;
import com.ticketingsystem.ticketingsystem.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {

    @Autowired
    private UserRepository userRepository;

}
