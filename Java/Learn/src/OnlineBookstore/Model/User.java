package OnlineBookstore.Model;

import OnlineBookstore.Constants.TypeBook;

import java.util.Arrays;
import java.util.List;

public class User {
    private static int UserCounter = 1;

    private int ID;

    private String UserName;

    private String Password;

    private List<Book<TypeBook>> ShoppingCart;

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getUserName() {
        return UserName;
    }

    public void setUserName(String userName) {
        UserName = userName;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public List<Book<TypeBook>> getShoppingCart() {
        return ShoppingCart;
    }

    public void setShoppingCart(List<Book<TypeBook>> shoppingCart) {
        ShoppingCart = shoppingCart;
    }

    public User(String userName, String password, List<Book<TypeBook>> shoppingCart) {
        this.ID = UserCounter++;
        this.UserName = userName.toLowerCase();
        this.Password = password;
        this.ShoppingCart = shoppingCart;
        System.out.println("(1) Row affected Successfully.");
    }

    @Override
    public String toString()
    {
        return "User N°: " + this.ID + "\n" +
                "User name : " + this.UserName + ", " +
                "His Shopping Cart : " + "\n" +
                this.ShoppingCart;
    }
}
