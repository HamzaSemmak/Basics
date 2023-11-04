package DeliverySystem.Constants;

import Logger.log4j;

import java.util.Objects;

public class orderStatus {

    public static final String Pending = "Pending";

    public static final String Delivered = "Delivered";

    public static final String Cancelled = "Cancelled";

    public static final String Shipped = "Shipped";

    public static final String Completed = "Completed";

    public orderStatus()
    {
        log4j.info("Order Status");
    }

    public static boolean status(String status) {
        return Objects.equals(status, Pending) ||
                Objects.equals(status, Delivered) ||
                Objects.equals(status, Cancelled) ||
                Objects.equals(status, Completed) ||
                Objects.equals(status, Shipped);
    }
}
