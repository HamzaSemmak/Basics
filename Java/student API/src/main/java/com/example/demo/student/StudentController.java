package com.example.demo.student;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping(path = "api/v1/student")
public class StudentController {

    private final StudentService studentService;
    @Autowired
    public StudentController(StudentService studentService) {
        this.studentService = studentService;
    }

    @GetMapping
    public List<Student> getStudent()
    {
        return this.studentService.getStudent();
    }

    @PostMapping(path = "add")
    public void addStudent(@RequestBody Student student)
    {
        this.studentService.addStudent(student);
    }

    @DeleteMapping(path = "delete/{id}")
    public void deleteStudent(@PathVariable("id") Long id)
    {
        this.studentService.deleteStudent(id);
    }

    @PutMapping(path = "update/{id}")
    public void updateStudent(
            @PathVariable("id") Long id,
            @RequestParam(required = false) String Name,
            @RequestParam(required = false) String Email)
    {
        this.studentService.updateStudent(id, Name, Email);
    }
}
