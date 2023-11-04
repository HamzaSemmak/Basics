package com.springsecurity.springsecurity;

import lombok.extern.slf4j.Slf4j;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
@Slf4j
public class SpringsecurityApplication {

	public static void main(String[] args) {
		log.info("Test lo tarik info.");
		log.info("Test lo tarik info.");
		SpringApplication.run(SpringsecurityApplication.class, args);
	}

}
