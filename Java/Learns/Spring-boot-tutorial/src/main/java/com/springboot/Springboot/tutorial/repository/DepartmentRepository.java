package com.springboot.Springboot.tutorial.repository;

import com.springboot.Springboot.tutorial.entity.Department;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface DepartmentRepository extends JpaRepository<Department, Long> {

    public Department findByname(String name);

    @Query(value = "SELECT d FROM Department d WHERE d.name LIKE %:name%", nativeQuery = true)
    public Department getDepartmentByName(@Param("name") String name);
}
