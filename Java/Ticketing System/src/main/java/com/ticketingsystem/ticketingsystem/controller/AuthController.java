package com.ticketingsystem.ticketingsystem.controller;

import com.ticketingsystem.ticketingsystem.entity.User;
import com.ticketingsystem.ticketingsystem.service.AuthService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;

import java.util.Optional;

@RestController
@RequestMapping(path = "${proxy-config}/authentication")
public class AuthController {

    private final Logger logger = LoggerFactory.getLogger(AuthController.class);

    @Autowired
    private AuthService authService;

    /**
     * login a user based on their username and password.
     *
     * @param username The username of the user.
     * @param password The user's password.
     * @return void.
      */
    @GetMapping(path = "/login")
    public String login(@RequestParam("username") String username, @RequestParam("password") String password) throws Exception
    {
        try {
            User logedInUser = authService.login(username, password);
            if(logedInUser != null)
            {
                return "user log in successfully. " + authService.user().toString();
            }
            else {
                return "username or password is invalid, Please try again";
            }
        }
        catch (Exception e) {
            return "Exception : " + e.getMessage();
        }
    }

    /**
     * Registers a user in the system.
     *
     * This method allows a user to register with the system by providing their
     * username, password, and other registration information. Upon successful
     * registration, the user will be granted access to the system.
     *
     * @param username The username chosen by the user.
     * @param password The password chosen by the user.
     * @param userRole The password chosen by the user.
     * @throws IllegalArgumentException If the provided username or password is invalid.
     */
    @PostMapping(path = "/register")
    public String register(@RequestParam("username") String username, @RequestParam("password") String password, @RequestParam("userRole") String userRole) throws Exception
    {
        try {
            User newUser = new User(username, password, userRole);
            authService.register(newUser);
            return "User registered successfully: " + newUser.toString();
        } catch (Exception e) {
            return "Exception: " + e.getMessage();
        }
    }

    /**
     * Resets the password for a user account based on their username or email.
     *
     * This method allows an administrator or the user themselves to initiate a password reset
     * for a specified user account. The user can reset their password by providing their
     * username or email address, and an email with a password reset link will be sent to
     * the associated email address.
     *
     * @param username The username or email of the user for whom the password should be reset.
     * @return true if the password reset request was successfully initiated, false otherwise.
     * @throws Exception If there is an issue during the password reset process.
     */
    @PostMapping(path = "/reset/password")
    public String resetPassword(@RequestParam("username") String username, @RequestParam("password") String password) {
        User user = authService.getUserByName(username);
        try {
            if(user != null) {
                if(password.isEmpty()) {
                    return "Error : password is empty.";
                }
                else if(password.length() < 8) {
                    return "Error : password must be at least 8";
                }
                else {
                    User newUser = authService.resetPassword(user, password);
                    return "Password it's rested successfully. " + newUser.toString();
                }
            }
            return  "user invalid, Please try again.";
        }
        catch(Exception e) {
            return "Exception: " + e.getMessage();
        }
    }

    /**
     * Retrieve the currently authenticated user.
     *
     * @return The user object representing the currently authenticated user.
     * @throws Exception if no user is authenticated.
     */
    @GetMapping(path = "/user")
    public Optional<User> User(@RequestParam("id") Long ID) throws Exception {
        return authService.getUserByID(ID);
    }
}
