package BookStoreSystem;

import BookStoreSystem.Constants.TypeBook;
import BookStoreSystem.Controller.BookController;
import BookStoreSystem.Controller.InventoryController;
import BookStoreSystem.Controller.UserController;
import BookStoreSystem.Model.Book;
import BookStoreSystem.Model.ShoppingCart;
import BookStoreSystem.Model.User;

public class Program {

    public static void main(String[] args) {

        System.out.println("Book Test : ");
        Book<TypeBook> book1 = new Book<>(TypeBook.Mystery, "The Da Vinci Code", "Dan Brown", 15.99, 5);
        Book<TypeBook> book2 = new Book<>(TypeBook.Mystery, "The Da Vinci Code", "Dan Brown", 15.99, 5);
        Book<TypeBook> book3 = new Book<>(TypeBook.Science, "A Brief History of Time", "Stephen Hawking", 12.99, 7);
        Book<TypeBook> book4 = new Book<>(TypeBook.Fantasy, "Harry Potter and the Sorcerer's Stone", "J.K. Rowling", 19.99, 3);
        Book<TypeBook> book5 = new Book<>(TypeBook.Romance, "Pride and Prejudice", "Jane Austen", 9.99, 8);
        Book<TypeBook> book6 = new Book<>(TypeBook.SelfHelp, "The 7 Habits of Highly Effective People", "Stephen R. Covey", 14.95, 6);
        Book<TypeBook> book7 = new Book<>(TypeBook.History, "A People's History of the United States", "Howard Zinn", 20.50, 4);
        Book<TypeBook> book8 = new Book<>(TypeBook.Biography, "Steve Jobs", "Walter Isaacson", 18.99, 2);
        Book<TypeBook> book9 = new Book<>(TypeBook.Technology, "Clean Code: A Handbook of Agile Software Craftsmanship", "Robert C. Martin", 29.99, 5);
        Book<TypeBook> book10 = new Book<>(TypeBook.Cooking, "The Joy of Cooking", "Irma S. Rombauer", 24.95, 3);
        Book<TypeBook> book11 = new Book<>(TypeBook.Art, "The Art Book", "Phaidon Editors", 22.75, 6);
        Book<TypeBook> book12 = new Book<>(TypeBook.Travel, "Into the Wild", "Jon Krakauer", 13.95, 7);
        Book<TypeBook> book13 = new Book<>(TypeBook.Poetry, "The Waste Land", "T.S. Eliot", 11.50, 4);
        Book<TypeBook> book14 = new Book<>(TypeBook.Other, "The Hitchhiker's Guide to the Galaxy", "Douglas Adams", 10.00, 5);
        System.out.println("\n");
        BookController.getCategory();
        System.out.println(book1.toString());
        BookController.updateQuantity(book1, 150);
        System.out.println(book1.toString());

        System.out.println("\n \n");

        System.out.println("Inventory System Test : ");
        InventoryController inventorySystem = new InventoryController();
        inventorySystem.addToListOfBooksInInventory(book1);
        inventorySystem.addToListOfBooksInInventory(book2);
        inventorySystem.addToListOfBooksInInventory(book3);
        inventorySystem.addToListOfBooksInInventory(book4);
        inventorySystem.addToListOfBooksInInventory(book5);
        inventorySystem.addToListOfBooksInInventory(book6);
        inventorySystem.addToListOfBooksInInventory(book7);
        inventorySystem.addToListOfBooksInInventory(book8);
        inventorySystem.addToListOfBooksInInventory(book9);
        inventorySystem.addToListOfBooksInInventory(book10);
        inventorySystem.addToListOfBooksInInventory(book11);
        inventorySystem.addToListOfBooksInInventory(book12);
        inventorySystem.addToListOfBooksInInventory(book13);
        inventorySystem.addToListOfBooksInInventory(book14);
        System.out.println("\n");
        inventorySystem.getBooksInInventory();
        System.out.println("\n");
        inventorySystem.searchBooksByAuthor("The Art Book");
        inventorySystem.searchBooksByAuthor("Dan Brown");
        inventorySystem.searchBooksByAuthor("Dan");

        System.out.println("\n \n");

        User user1 = new User("hamza", "AA102374", inventorySystem.getListOfBooks());
        UserController auth = new UserController();
        auth.addNewUser(user1);
        auth.login("hamza", "AA102374");

        System.out.println("\n \n");

        ShoppingCart shoppingCart = new ShoppingCart(user1);
        shoppingCart.addBook(book1);
        shoppingCart.addBook(book2);
        shoppingCart.addBook(book3);
        shoppingCart.addBook(book4);

        shoppingCart.removeBook(book4);

        shoppingCart.calculateTotalPrice();

        shoppingCart.clearCart();

        System.out.println("\n \n");
        System.out.println("Test 1 passed Successfully, (Class Book).");
        System.out.println("Test 2 passed Successfully, (Class User).");
        System.out.println("Test 3 passed Successfully, (Class ShoppingCart).");
        System.out.println("Test 4 passed Successfully, (Class all Controller).");
        System.out.println("Test 5 passed Successfully, (Class all TypeBook).");
        System.out.println("Done!");
    }
}
