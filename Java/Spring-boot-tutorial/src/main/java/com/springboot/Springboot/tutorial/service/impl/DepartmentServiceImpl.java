package com.springboot.Springboot.tutorial.service.impl;

import com.springboot.Springboot.tutorial.entity.Department;
import com.springboot.Springboot.tutorial.repository.DepartmentRepository;
import com.springboot.Springboot.tutorial.service.DepartmentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Objects;
import java.util.Optional;

@Service
public class DepartmentServiceImpl implements DepartmentService {

    @Autowired
    private DepartmentRepository departmentRepository;

    @Override
    public Department save(Department department) {
        return departmentRepository.save(department);
    }

    @Override
    public List<Department> findAll() {
        return departmentRepository.findAll();
    }

    @Override
    public Optional<Department> findByID(Long id)
    {
        return departmentRepository.findById(id);
    }

    @Override
    public void delete(Long id) {
        departmentRepository.deleteById(id);
    }

    @Override
    public Department update(Long id, Department department) {
        Department oldDepartment = departmentRepository.findById(id).get();
        if(Objects.nonNull(department.getName()) && !"".equalsIgnoreCase(department.getName())) {
            oldDepartment.setName(department.getName());
        }
        if(Objects.nonNull(department.getCode()) && !"".equalsIgnoreCase(department.getCode())) {
            oldDepartment.setCode(department.getCode());
        }
        if(Objects.nonNull(department.getAddress()) && !"".equalsIgnoreCase(department.getAddress())) {
            oldDepartment.setAddress(department.getAddress());
        }
        return departmentRepository.save(oldDepartment);
    }

    @Override
    public List<Department> findByName(String name) {
        return departmentRepository.getDepartmentByName(name);
    }

}
