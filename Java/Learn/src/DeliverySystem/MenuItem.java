package DeliverySystem;

import DeliverySystem.Logger.log4j;
import sun.security.krb5.internal.crypto.Des;

public class MenuItem {
    protected int newObject = 1;

    private int ID;

    private String Name;

    private String Description;

    private double Price;

    private String Category;

    public String getCategory() {
        return Category;
    }

    public void setCategory(String category) {
        Category = category;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        Price = price;
    }

    public MenuItem(String name, String description, double price, String category) {
        ID = this.newObject++;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        log4j.info("(1) Row affected Successfully.");
        log4j.info(this.toString());
    }

    @Override
    public String toString() {
        return "MenuItem N°: " + ID +
                "\n Name : + " + Name +
                ", Description : " + Description +
                ", Price : " + Price +
                ", Category : " + Category;
    }
}
