package com.springboot.Springboot.tutorial.controller;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@RestController
public class HelloController {

    @Value("${hello.controller.message}")
    private String message;

    @GetMapping(path = "/")
    public String hello()
    {
        return message;
    }

}
