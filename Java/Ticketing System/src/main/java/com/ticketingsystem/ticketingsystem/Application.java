package com.ticketingsystem.ticketingsystem;

import com.ticketingsystem.ticketingsystem.config.CORS;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.web.servlet.FilterRegistrationBean;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class Application {

	public static void main(String[] args) {
		SpringApplication.run(Application.class, args);
	}

	@Bean
	public FilterRegistrationBean<CORS> corsFilter() {
		FilterRegistrationBean<CORS> registrationBean = new FilterRegistrationBean<>();
		registrationBean.setFilter(new CORS());
		registrationBean.addUrlPatterns("/*");
		return registrationBean;
	}

}
