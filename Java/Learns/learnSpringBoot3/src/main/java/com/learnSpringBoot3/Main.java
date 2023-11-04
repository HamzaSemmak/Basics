package com.learnSpringBoot3;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@SpringBootApplication
@RestController
@RequestMapping(path = "api/v1/customers")
public class Main {
    private final CustomerRepository customerRepository;
    @Autowired
    public Main(CustomerRepository customerRepository) {
        this.customerRepository = customerRepository;
    }
    @GetMapping
    public List<Customer> getCustomers()
    {
        return customerRepository.findAll();
    }
    record newCustomerRequest(
            String name,
            String email,
            Integer age
    ) {}
    @PostMapping
    public void addCustomer(@RequestBody newCustomerRequest Request)
    {
        Customer customer = new Customer();
        customer.setName(Request.name);
        customer.setAge(Request.age);
        customer.setEmail(Request.email);
        customerRepository.save(customer);
    }

    @DeleteMapping(path = "{id}")
    public void deleteCustomer(@PathVariable Integer id)
    {
        customerRepository.deleteById(id);
    }

    record UpdateCustomerRequest(
            String name,
            String email,
            Integer age
    ) {}
    @PutMapping(path = "{id}")
    public ResponseEntity<String> updateCustomer(
            @PathVariable Integer id,
            @RequestBody UpdateCustomerRequest request
    ) {
        Optional<Customer> optionalCustomer = customerRepository.findById(id);

        if (optionalCustomer.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        Customer existingCustomer = optionalCustomer.get();
        existingCustomer.setName(request.name());
        existingCustomer.setEmail(request.email());
        existingCustomer.setAge(request.age());
        customerRepository.save(existingCustomer);

        return ResponseEntity.ok("Customer updated successfully");
    }



    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }
}
