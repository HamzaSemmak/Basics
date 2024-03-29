package com.ticketingsystem.ticketingsystem.entity;

import jakarta.persistence.*;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

@Entity
@Table(name = "users")
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "user_id")
    private Long ID;

    @Column(name = "username", length = 255, unique = true)
    private String Name;

    @Column(name = "password")
    private String Password;

    @Column(name = "role")
    private String Role;

    public Long getID() {
        return ID;
    }

    public void setID(Long ID) {
        this.ID = ID;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String plainTextPassword) {
        BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
        Password = passwordEncoder.encode(plainTextPassword);
    }

    public String getRole() {
        return Role;
    }

    public void setRole(String role) {
        Role = role;
    }

    public User() {
    }

    public User(String name, String password, String role) {
        Name = name;
        Role = role;
        setPassword(password);
    }

    @Override
    public String toString() {
        return "User{" +
                "ID=" + ID +
                ", Name='" + Name + '\'' +
                ", Password='" + Password + '\'' +
                ", Role=" + Role +
                '}';
    }
}
