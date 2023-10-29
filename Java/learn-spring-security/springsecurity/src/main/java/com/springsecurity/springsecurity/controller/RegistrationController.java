package com.springsecurity.springsecurity.controller;

import com.springsecurity.springsecurity.entity.User;
import com.springsecurity.springsecurity.event.RegistrationCompleteEvent;
import com.springsecurity.springsecurity.model.UserModel;
import com.springsecurity.springsecurity.service.UserService;
import jakarta.servlet.http.HttpServletRequest;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationEventPublisher;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

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

    private String applicationUrl(HttpServletRequest request) {
        return "http://" + request.getServerName() + ":"
                    + request.getServerPort()
                    + request.getContextPath();
    }

}
