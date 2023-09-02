package algorithm;

import java.lang.reflect.Array;

public class NumberAlgorithm {
    public int number;
    private int x = 1;


    public NumberAlgorithm(int number)
    {
        if (number < 0) {
            throw new Error("Please define a new number!");
        }
        this.number = number;
    }

    public void Factorial()
    {
        for(int i = 1; i <= this.number; i++) {
            x = i * x;
        }
        System.out.println("Factorial of " + this.number + " is : " + x);
    }

    public void Factors()
    {
        System.out.printf("Factors of " + this.number + " is : ");
        for(int i = 1; i <= this.number; i++) {
            System.out.printf(i + " ");
        }
        System.out.println(" ");
    }

    public void Fibonacci()
    {
        int a = 0;
        int b = 1;
        int c = 0;
        System.out.printf("Fibonacci of " + this.number + " is : ");
        for(int i = 0; i <= this.number; i++) {
            c = a + b;
            System.out.printf(c + " ");
            a = b;  b = c;
        }
    }

    public int Reverse()
    {
        StringBuilder reversed = new StringBuilder(String.valueOf(this.number));
        String str = reversed.reverse().toString();

        return Integer.parseInt(str);
    }

    public void Palindrome()
    {
        if(this.number == Reverse())
        {
            System.out.println("true");
        }
        else
        {
            System.out.println("false");
        }
    }

    public void Pattern()
    {

    }
}
