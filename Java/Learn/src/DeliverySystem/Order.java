package DeliverySystem;

import DeliverySystem.Constants.orderStatus;
import DeliverySystem.Logger.log4j;
import com.sun.corba.se.impl.oa.toa.TOA;

import java.util.ArrayList;
import java.util.List;

public class Order {
    private static int newOrder = 1;

    private int ID;

    private Customer Customer;

    private List<MenuItem> Items;

    private String Status;

    private double totalPrice;

    public DeliverySystem.Customer getCustomer() {
        return Customer;
    }

    public void setCustomer(DeliverySystem.Customer customer) {
        Customer = customer;
    }

    public List<MenuItem> getItems() {
        return Items;
    }

    public void setItems(List<MenuItem> items) {
        Items = items;
    }

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        if(!orderStatus.status(status))
        {
            log4j.error("Order status invalid");
            return;
        }

        Status = status;
    }

    public double getTotalPrice() {
        return totalPrice;
    }

    public void setTotalPrice(double totalPrice) {
        this.totalPrice = totalPrice;
    }

    public Order(Customer customer, double totalPrice) {
        ID = newOrder++;
        Items = new ArrayList<>();

        Customer = customer;
        Status = orderStatus.Pending;
        this.totalPrice = totalPrice;
        log4j.info("(1) Row affected Successfully.");
        log4j.info(this.toString());
    }

    public void addItem(MenuItem item)
    {
        Items.add(item);
        log4j.info("(1) Row affected Successfully.");
    }

    public void removeItem(MenuItem item)
    {
        Items.remove(item);
        log4j.info("(1) Row deleted Successfully.");
    }

    public void calculateTotalPrice()
    {
        log4j.info("Calculates the total price of the order based on the selected items : ");
        double Total = 0;
        for(MenuItem item: Items)
            Total += item.getPrice();

        this.setTotalPrice(Total);
        log4j.info("Total : " + Total);
    }

    public void updateStatus(String status)
    {
        this.setStatus(status);
        log4j.info("(1) Row Updated Successfully.");
    }

    @Override
    public String toString() {
        return "Order{" +
                "ID=" + ID +
                ", Customer=" + Customer +
                ", Items=" + Items +
                ", Status='" + Status + '\'' +
                ", totalPrice=" + totalPrice +
                '}';
    }
}
