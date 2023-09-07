package OnlineBookstore.Controller;

import OnlineBookstore.Constants.TypeBook;
import OnlineBookstore.Model.Book;

public class BookController {

    public static void getCategory()
    {
        System.out.println("This is the list Of Books Categories : ");
        TypeBook[] Types = TypeBook.values();
        for(TypeBook t : Types)
        {
            System.out.println(t + " ");
        }
    }

    public static void updateQuantity(Book<TypeBook> book, int newQuantity)
    {
        book.setQuantite(newQuantity);
        System.out.println("(1) Row updated Successfully.");
    }
}
