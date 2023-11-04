package com.example.demo.student;

import jakarta.persistence.*;

import java.time.LocalDate;
import java.time.Period;

@Entity
@Table
public class Student {
    @Id
    @SequenceGenerator(
            name = "student_sequence",
            sequenceName = "student_sequence",
            allocationSize = 1
    )
    @GeneratedValue(
            strategy = GenerationType.SEQUENCE,
            generator = "student_sequence"
    )
    private Long ID;
    private String Name;
    private String Email;
    private LocalDate DOB;
    @Transient
    private Integer Age;

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

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public LocalDate getDOB() {
        return DOB;
    }

    public void setDOB(LocalDate DOB) {
        this.DOB = DOB;
    }

    public int getAge() {
        return Period.between(this.DOB, LocalDate.now()).getYears();
    }

    public void setAge(int age) {
        Age = age;
    }

    public Student() {
    }

    public Student(Long ID, String name, String email, LocalDate DOB) {
        this.ID = ID;
        Name = name;
        Email = email;
        this.DOB = DOB;
    }

    public Student(String name, String email, LocalDate DOB) {
        Name = name;
        Email = email;
        this.DOB = DOB;
    }

    @Override
    public String toString() {
        return "Student{" +
                "ID=" + ID +
                ", Name='" + Name + '\'' +
                ", Email='" + Email + '\'' +
                ", DOB=" + DOB +
                ", Age=" + Age +
                '}';
    }
}
