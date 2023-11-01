package com.springsecurity.springsecurity.service;

import com.springsecurity.springsecurity.entity.User;
import com.springsecurity.springsecurity.entity.VerificationToken;
import com.springsecurity.springsecurity.model.UserModel;
import com.springsecurity.springsecurity.service.impl.UserServiceImpl;

public interface UserService {
    User register(UserModel userModel);

    void saveVerificationTokenForUser(User user, String token);

    String validateVerifyRegistration(String token);

    VerificationToken generateNewVerificationToken(String token);

    User findUserByEmial(String email);

    void createPasswordResetTokenForUser(User user, String token);

    String validatePasswordResetToken(String token);
}
