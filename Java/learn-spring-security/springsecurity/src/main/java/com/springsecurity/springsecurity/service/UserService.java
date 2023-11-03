package com.springsecurity.springsecurity.service;

import com.springsecurity.springsecurity.entity.User;
import com.springsecurity.springsecurity.entity.VerificationToken;
import com.springsecurity.springsecurity.model.UserModel;
import com.springsecurity.springsecurity.service.impl.UserServiceImpl;

import java.util.List;
import java.util.Optional;

public interface UserService {
    User register(UserModel userModel);

    void saveVerificationTokenForUser(User user, String token);

    String validateVerifyRegistration(String token);

    VerificationToken generateNewVerificationToken(String token);

    User findUserByEmial(String email);

    void createPasswordResetTokenForUser(User user, String token);

    String validatePasswordResetToken(String token);

    Optional<User> findUserByPasswordResetToken(String token);

    void changePassword(User user, String newPassword);

    boolean checkIfValidOldPassword(User user, String oldPassword);

    List<User> usersList();
}
