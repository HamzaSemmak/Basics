package OnlineBookstore.Model;

public class Book<T> {
    private static int BookCounter = 1;

    private int ID;

    private T ISBN;

    private String Title;

    private String Author;

    private double Price;

    private int Quantite;

    public T getISBN() {
        return ISBN;
    }

    public void setISBN(T ISBN) {
        this.ISBN = ISBN;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getAuthor() {
        return Author;
    }

    public void setAuthor(String author) {
        Author = author;
    }

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        Price = price;
    }

    public int getQuantite() {
        return Quantite;
    }

    public void setQuantite(int quantite) {
        Quantite = quantite;
    }

    public Book(T ISBN, String title, String author, double price, int quantite) {
        this.ID = BookCounter++;
        this.ISBN = ISBN;
        this.Title = title;
        this.Author = author;
        this.Price = price;
        this.Quantite = quantite;
        System.out.println("(1) Row affected Successfully.");
    }

    @Override
    public String toString()
    {
        return  "Book with ID : " + this.ID +
                " His ISBN (International Standard Book Number) is " + this.ISBN + ", \n" +
                "Book Title : " + this.Title + ", Author : " + this.Author +
                ", His Price : " + this.Price +
                ", The Available Quantite is " + this.Quantite;
    }
}
