package com.springboot.Springboot.tutorial.service;

import com.springboot.Springboot.tutorial.entity.Department;
import com.springboot.Springboot.tutorial.repository.DepartmentRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
class DepartmentServiceTest {

    @Autowired
    private DepartmentService departmentService;

    @MockBean
    private DepartmentRepository departmentRepository;

    @BeforeEach
    void setUp() {
        Department departement = Department.builder().name("IT").code("AA150").address("TT").build();

        Mockito.when(departmentRepository.findByname("IT")).thenReturn(departement);
    }

    @Test
    public void whenValidDepartmentName_thenDepartmentShouldFound()
    {
        String name = "IT";
        Department department = departmentService.findByName(name);

        assertEquals(department, department.getName());

    }
}