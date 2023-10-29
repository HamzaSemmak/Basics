package com.springsecurity.springsecurity.repository;

import com.springsecurity.springsecurity.entity.VerificationToken;
import org.springframework.data.jpa.repository.JpaRepository;

public interface VerificationTokenRepository extends JpaRepository<VerificationToken, Long> {
}
