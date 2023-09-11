package DeliverySystem;

import DeliverySystem.Logger.log4j;

public class Customer {
    private static int newCustomer = 1;

    private int ID;

    private String Name;

    private String Address;

    private String Phone;

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getAddress() {
        return Address;
    }

    public void setAddress(String address) {
        Address = address;
    }

    public String getPhone() {
        return Phone;
    }

    public void setPhone(String phone) {
        Phone = phone;
    }

    public Customer(String name, String address, String phone) {
        ID = newCustomer++;
        Name = name;
        Address = address;
        Phone = phone;
        log4j.info("(1) Row affected Successfully.");
        log4j.info(this.toString());
    }

    @Override
    public String toString() {
        return "Customer{" +
                "ID=" + ID +
                ", Name='" + Name + '\'' +
                ", Address='" + Address + '\'' +
                ", Phone='" + Phone + '\'' +
                '}';
    }
}
