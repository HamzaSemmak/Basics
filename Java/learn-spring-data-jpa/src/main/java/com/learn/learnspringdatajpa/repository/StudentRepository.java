package com.learn.learnspringdatajpa.repository;

import com.learn.learnspringdatajpa.entity.Student;
import org.springframework.data.jpa.repository.JpaRepository;

public interface StudentRepository extends JpaRepository<Student, Long> {

}
