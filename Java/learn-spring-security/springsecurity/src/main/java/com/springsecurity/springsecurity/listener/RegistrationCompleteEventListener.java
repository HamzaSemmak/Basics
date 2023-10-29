package com.springsecurity.springsecurity.listener;

import com.springsecurity.springsecurity.entity.User;
import com.springsecurity.springsecurity.event.RegistrationCompleteEvent;
import com.springsecurity.springsecurity.repository.VerificationTokenRepository;
import com.springsecurity.springsecurity.service.UserService;
import lombok.Getter;
import lombok.Setter;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationListener;

import java.util.UUID;

@Slf4j
public class RegistrationCompleteEventListener implements ApplicationListener<RegistrationCompleteEvent> {

    @Autowired
    private UserService userService;

    @Override
    public void onApplicationEvent(RegistrationCompleteEvent event) {
        //Create the verification token for the user with link.
        User user = event.getUser();
        String token = UUID.randomUUID().toString();

        userService.saveVerificationTokenForUser(user, token);

        //Send email to user
        String url = event.getApplicationUrl() + "verifyRegistration?token=" + token;

        log.info("Click the link to verify your account. " + url);

    }
}
