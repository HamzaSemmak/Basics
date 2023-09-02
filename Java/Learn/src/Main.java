import Banking.Account;
import algorithm.NumberAlgorithm;

import java.util.Scanner;

// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
        Account Account1 = new Account(1, 1000, "hamza");

        System.out.println(Account1.toString());

        Account1.deposit(100);

        System.out.println(Account1.toString());

        Account1.withdraw(50);

        System.out.println(Account1.toString());

    }
}