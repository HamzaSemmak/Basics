package com.springboot.Springboot.tutorial.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

@RestController
public class HelloController {

    @GetMapping(path = "/")
    public String hello()
    {
        return "Hello";
    }

}
