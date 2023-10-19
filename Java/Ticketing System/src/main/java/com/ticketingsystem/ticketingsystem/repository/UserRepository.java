package com.ticketingsystem.ticketingsystem.repository;

import com.ticketingsystem.ticketingsystem.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface UserRepository extends JpaRepository<User, Long> {

    @Query("SELECT u FROM User u WHERE u.Name = :name AND u.Password = :password")
    public User findUserByNameAndPassword(@Param("name") String name, @Param("password") String password);
}
