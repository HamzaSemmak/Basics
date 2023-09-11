package DeliverySystem;

import DeliverySystem.Logger.log4j;

public class CustomerManager {

    protected Customer Session;

    public CustomerManager()
    {
        log4j.info("You have access to Customer Service.");
    }

    public void registerCustomer(String name, String address, String phone)
    {
        Customer customer = new Customer(name, address, phone);
        log4j.info(customer.toString());
    }

    public void login(Customer customer)
    {
        Session = customer;
        log4j.info("User " + customer.getID() + " just logged in");
        log4j.info(customer.toString());
    }

    public void logout()
    {
        Session = null;
        log4j.info("logout.");
    }

    public boolean user()
    {
        return Session == null;
    }
}
