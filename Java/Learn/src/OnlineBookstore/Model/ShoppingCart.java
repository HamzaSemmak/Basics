package OnlineBookstore.Model;

import OnlineBookstore.Constants.TypeBook;

import java.util.ArrayList;
import java.util.List;

public class ShoppingCart {
    private static int newShoppingCart = 1;

    private int ID;

    private User User;

    private List<Book<TypeBook>> Books;

    public List<Book<TypeBook>> getBooks() {
        return Books;
    }

    public void setBooks(List<Book<TypeBook>> books) {
        Books = books;
    }

    public User getUser() {
        return User;
    }

    public void setUser(User user) {
        User = user;
    }

    public ShoppingCart(User user) {
        this.ID = newShoppingCart++;
        this.User = user;
        this.Books = new ArrayList<>();
        System.out.println("(1) Row affected Successfully.");
    }

    public void addBook(Book<TypeBook> Book)
    {
        this.Books.add(Book);
        System.out.println("(1) Row affected Successfully.");
    }

    public void removeBook(Book<TypeBook> Book)
    {
        this.Books.remove(Book);
        System.out.println("(1) Row deleted Successfully.");
    }

    public void clearCart()
    {
        int size = this.Books.size();
        this.Books.clear();
        System.out.println("(" + size  + ") Row(s) deleted Successfully.");
    }

    public void calculateTotalPrice()
    {
        double total = 0;
        System.out.println("The total price of the books in your shopping cart");
        for(Book<TypeBook> book : this.Books)
        {
            total += book.getPrice();
        }
        System.out.println("Total : " + total);
    }
}
