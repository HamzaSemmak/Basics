package BookStoreSystem.Controller;

import BookStoreSystem.Constants.TypeBook;
import BookStoreSystem.Model.Book;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

public class InventoryController {
    private List<Book<TypeBook>> listOfBooks;

    public void setListOfBooks(List<Book<TypeBook>> listOfBooks) {
        this.listOfBooks = listOfBooks;
    }

    public List<Book<TypeBook>> getListOfBooks()
    {
        return this.listOfBooks;
    }

    public InventoryController()
    {
        this.listOfBooks = new ArrayList<>();
    }

    public void addToListOfBooksInInventory(Book<TypeBook> book)
    {
        this.listOfBooks.add(book);
        System.out.println("(1) Row affected Successfully.");
    }

    public void getBooksInInventory()
    {
        System.out.println("This the List Of Books in Inventory : ");
        for(Book<TypeBook> Book : listOfBooks)
        {
            System.out.println(Book.toString());
        }
        System.out.println("\n");
    }

    public void searchBooksByTitle(String title)
    {
        List<Book<TypeBook>> foundBooks = listOfBooks.stream()
                .filter(book -> Objects.equals(book.getTitle(), title))
                .collect(Collectors.toList());

        if (foundBooks.isEmpty()) {
            System.out.println("0 Book was Found.");
        } else {
            System.out.println("This is the List Of Books in Inventory with title " + title);
            foundBooks.forEach(System.out::println);
        }
    }

    public void searchBooksByAuthor(String author)
    {
        List<Book<TypeBook>> foundBooks = listOfBooks.stream()
                .filter(book -> Objects.equals(book.getAuthor(), author))
                .collect(Collectors.toList());

        if (foundBooks.isEmpty()) {
            System.out.println("0 Book was Found.");
        } else {
            System.out.println("This is the List Of Books in Inventory with author " + author);
            foundBooks.forEach(System.out::println);
        }
    }
}
