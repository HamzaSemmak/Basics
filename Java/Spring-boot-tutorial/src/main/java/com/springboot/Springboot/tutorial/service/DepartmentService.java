package com.springboot.Springboot.tutorial.service;

import com.springboot.Springboot.tutorial.entity.Department;

import java.util.List;
import java.util.Optional;

public interface DepartmentService {
    public Department save(Department department);

    public List<Department> findAll();

    public Optional<Department> findByID(Long id);

    public void delete(Long id);

    public Department update(Long id, Department department);

    public Department findByName(String name);
}
