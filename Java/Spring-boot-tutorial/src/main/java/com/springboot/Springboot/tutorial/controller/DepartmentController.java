package com.springboot.Springboot.tutorial.controller;

import com.springboot.Springboot.tutorial.entity.Department;
import com.springboot.Springboot.tutorial.service.DepartmentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
public class DepartmentController {
    @Autowired
    private DepartmentService departmentService;

    @PostMapping(path = "/department")
    public Department save(@RequestBody  Department department) {
        return departmentService.save(department);
    }

    @GetMapping(path = "/department")
    public List<Department> findAll()
    {
        return departmentService.findAll();
    }

    @GetMapping(path = "/department/{id}")
    public Optional<Department> findByID(@PathVariable("id") Long id)
    {
        return  departmentService.findByID(id);
    }

    @DeleteMapping(path = "/department/{id}")
    public String delete(@PathVariable("id") Long id)
    {
        departmentService.delete(id);
        return "department deleted successfully";
    }

    @PutMapping(path = "/department/{id}")
    public Department update(@PathVariable("id") Long id, @RequestBody Department department)
    {
        return departmentService.update(id, department);
    }

    @GetMapping(path = "department/name/{name}")
    public List<Department> findByName(@PathVariable("name") String name)
    {
        return departmentService.findByName(name);
    }
}
