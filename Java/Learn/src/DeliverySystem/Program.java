package DeliverySystem;

import DeliverySystem.Constants.orderStatus;
import DeliverySystem.Logger.log4j;

import java.util.Arrays;

public class Program {

    public static void main(String[] args) {
        log4j.info("Start of delivery system application");
        log4j.info(Arrays.toString(args));
        log4j.info("\n");

        log4j.info("Test MenuItems : \n");
        MenuItem menuItem1 = new MenuItem("Pizza", "This is the Best pizza in the world", 13.00, "Pizza");
        MenuItem menuItem2 = new MenuItem("Burger", "A classic burger with all the fixings", 8.99, "Burger");
        MenuItem menuItem3 = new MenuItem("Salad", "Fresh garden salad with mixed greens", 6.49, "Salad");
        MenuItem menuItem4 = new MenuItem("Pasta", "Homemade pasta with rich tomato sauce", 12.99, "Pasta");
        MenuItem menuItem5 = new MenuItem("Sushi Roll", "Assorted sushi rolls with fresh seafood", 15.50, "Sushi");
        MenuItem menuItem6 = new MenuItem("Steak", "Grilled steak with a choice of side", 19.99, "Steak");
        MenuItem menuItem7 = new MenuItem("Ice Cream", "Creamy vanilla ice cream with toppings", 4.50, "Dessert");
        MenuItem menuItem8 = new MenuItem("Soft Drink", "Refreshing soft drink in various flavors", 2.00, "Beverage");
        log4j.info(menuItem1.toString());

        log4j.info("Test Menu : \n");
        Menu menu = new Menu();
        menu.addMenuItem(menuItem1);
        menu.addMenuItem(menuItem2);
        menu.addMenuItem(menuItem3);
        menu.addMenuItem(menuItem4);
        menu.addMenuItem(menuItem5);
        menu.addMenuItem(menuItem6);
        menu.addMenuItem(menuItem7);
        menu.addMenuItem(menuItem8);
        menu.removeMenuItem(menuItem8);
        menu.addMenuItem(menuItem8);
        menu.listMenu();
        menu.searchMenuItem("Pizza");
        menu.searchMenuItem("Ice");
        menu.searchMenuItem("Steak");

        log4j.info("Test Customer : \n");
        Customer customer = new Customer("Hamza Semmak", "IMM 7 APPT 13 RABAT", "0667786555");

        log4j.info("Test Order : \n");
        Order order = new Order(customer, 0);
        DeliveryService deliveryService = new DeliveryService();
        deliveryService.startDelivery(order);
        order.addItem(menuItem4);
        order.addItem(menuItem5);
        order.addItem(menuItem2);
        order.addItem(menuItem1);
        order.removeItem(menuItem4);
        order.calculateTotalPrice();
        order.updateStatus(orderStatus.Completed);

        log4j.info("Test OrderManager : \n");
        OrderManager orderManager = new OrderManager();
        orderManager.placeOrder(customer, 0);
        orderManager.trackOrder(order);
        orderManager.completeOrder(order);

        log4j.info("Test CustomerManager : \n");
        CustomerManager customerManager = new CustomerManager();
        customerManager.registerCustomer("Hamza Semmak", "IMM 7 APPT 13 RABAT", "0667786555");
        customerManager.login(customer);
        customerManager.logout();
        log4j.info("Check if user is logged => (" + customerManager.user() + ").");

        deliveryService.endDelivery(order);

        log4j.info("End of delivery system application" + "\n \n \n \n \n");
    }

}
