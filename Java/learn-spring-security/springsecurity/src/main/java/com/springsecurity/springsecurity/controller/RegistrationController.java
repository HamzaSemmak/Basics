package com.springsecurity.springsecurity.controller;

import com.springsecurity.springsecurity.entity.User;
import com.springsecurity.springsecurity.entity.VerificationToken;
import com.springsecurity.springsecurity.event.RegistrationCompleteEvent;
import com.springsecurity.springsecurity.model.PasswordModel;
import com.springsecurity.springsecurity.model.UserModel;
import com.springsecurity.springsecurity.service.UserService;
import jakarta.servlet.http.HttpServletRequest;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationEventPublisher;
import org.springframework.web.bind.annotation.*;

import java.util.UUID;

@Slf4j
@RestController
public class RegistrationController {

    @Autowired
    private UserService userService;

    @Autowired
    private ApplicationEventPublisher publisher;

    @PostMapping(value = "/register")
    public String registerUser(@RequestBody UserModel userModel, final HttpServletRequest request) {
        User user = userService.register(userModel);
        publisher.publishEvent(new RegistrationCompleteEvent(user, applicationUrl(request)));
        return "Success. " + userModel.toString();
    }

    @GetMapping(path = "/verifyRegistration")
    public String verifyRegistration(@RequestParam(name = "token") String token)
    {
        String msg = userService.validateVerifyRegistration(token);
        if(msg.equalsIgnoreCase("valid")) {
            return  "User verifies successfully";
        }
        else {
            return "token not valid";
        }
    }

    @GetMapping(path = "/resendVerifyToken")
    public String resendVerifyToken(@RequestParam(name = "token") String token, HttpServletRequest request)
    {
        VerificationToken verificationToken = userService.generateNewVerificationToken(token);
        User user = verificationToken.getUser();
        resendVerificationTokenMail(user, applicationUrl(request), verificationToken);
        return "verification link Sent";
    }

    @PostMapping(path = "/restPassword")
    public String resetPassword(@RequestBody PasswordModel passwordModel, HttpServletRequest request)
    {
        User user = userService.findUserByEmial(passwordModel.getEmail());
        String url = "";
        if(user != null) {
            String token = UUID.randomUUID().toString();
            userService.createPasswordResetTokenForUser(user, token);
            url = passwordResetTokenMail(user, applicationUrl(request), token);
        }
        return url;
    }

    @PostMapping(path = "/savePassword")
    public String savePassword(@RequestParam("token") String token, @RequestBody PasswordModel passwordModel)
    {
        String result = userService.validatePasswordResetToken(token);
        if(result.equalsIgnoreCase("valid")) {
            return result;
        }

        return "invalid";
    }

    private String passwordResetTokenMail(User user, String applicationUrl, String token) {
        String url = applicationUrl + "/savePassword?token=" + token;

        log.info("Click the link to reset your password. {}", url);
        return url;
    }

    private void resendVerificationTokenMail(User user, String applicationUrl, VerificationToken verificationToken) {
        String url = applicationUrl + "/verifyRegistration?token=" + verificationToken.getToken();

        log.info("Click the link to verify your account. {}", url);
    }

    private String applicationUrl(HttpServletRequest request) {
        return "http://" + request.getServerName() + ":"
                    + request.getServerPort()
                    + request.getContextPath();
    }

}
