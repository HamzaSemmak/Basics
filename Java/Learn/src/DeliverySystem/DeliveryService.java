package DeliverySystem;

import DeliverySystem.Constants.orderStatus;
import DeliverySystem.Logger.log4j;

public class DeliveryService {

    public DeliveryService()
    {
        log4j.info("You have access to Delivery Service.");
    }

    public void startDelivery(Order order)
    {
        order.setStatus(orderStatus.Pending);
        log4j.info("initialize new Order");
    }

    public void endDelivery(Order order)
    {
        OrderManager orderManager = new OrderManager();
        order.setStatus(orderStatus.Completed);
        orderManager.completeOrder(order);
        log4j.info("order completed.");
    }
}
