package com.example.demo.student;

import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.PutMapping;

import java.time.LocalDate;
import java.time.Month;
import java.util.List;
import java.util.Objects;
import java.util.Optional;

@Service
public class StudentService {

    private final StudentRepository StudentRepository;

    @Autowired
    public StudentService(StudentRepository studentRepository) {
        StudentRepository = studentRepository;
    }

    public List<Student> getStudent()
    {
        return StudentRepository.findAll();
    }

    public void addStudent(Student student) {
        Optional<Student> studentByEmail = StudentRepository.findStudentByEmail(student.getEmail());
        if(studentByEmail.isPresent())
        {
            throw new IllegalStateException("Email already taken");
        }
        StudentRepository.save(student);
    }

    public void deleteStudent(Long id)
    {
        boolean exist = StudentRepository.existsById(id);
        if(!exist)
        {
            throw new IllegalStateException("student not found");
        }
        StudentRepository.deleteById(id);
    }

    @Transactional
    public void updateStudent(Long id, String name, String email)
    {
        Student student = StudentRepository.findById(id).orElseThrow(() -> new IllegalStateException("Student not found"));
        if(name != null && !name.isEmpty() && !Objects.equals(student.getName(), name)) {
            student.setName(name);
        }
        if(email != null && !email.isEmpty() && !Objects.equals(student.getEmail(), email)) {
            Optional<Student> studentByEmail = StudentRepository.findStudentByEmail(student.getEmail());
            if(studentByEmail.isPresent())
            {
                throw new IllegalStateException("Email already taken");
            }
            student.setEmail(email);
        }
        StudentRepository.save(student);
    }
}
