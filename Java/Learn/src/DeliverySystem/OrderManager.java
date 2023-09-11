package DeliverySystem;

import DeliverySystem.Constants.orderStatus;
import DeliverySystem.Logger.log4j;

public class OrderManager {

    public OrderManager()
    {
        log4j.info("You have access to Order Service.");
    }

    public void placeOrder(Customer customer, double total)
    {
        Order order = new Order(customer, total);
        log4j.info(order.toString());
    }

    public void trackOrder(Order order)
    {
        log4j.info(order.toString());
    }

    public void completeOrder(Order order)
    {
        order.setStatus(orderStatus.Completed);
        log4j.info("(1) Row updated Successfully.");
    }
}
