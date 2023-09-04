package LearnAlgorithm;

public class Algorithms {
    public int number;

    public String string;
    private int x = 1;

    public Algorithms(int number)
    {
        if (number < 0) {
            throw new Error("Please define a new number!");
        }
        this.number = number;
    }

    public Algorithms(String str)
    {
        this.string = str;
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

    public void commonSequences(String input1, String input2)
    {
        for (int i = 0; i < input1.length(); i++) {
            for (int j = 0; j < input2.length(); j++) {
                if(input1.charAt(i) == input2.charAt(j))
                {
                    System.out.printf(input1.charAt(i) + " ");
                }
            }
        }
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
        for (int i = 0; i < this.number; i++) {
            for (int j = 0; j < i; j++) {
                System.out.print("* ");
            }
            System.out.println(" ");
        }
    }

    public void ReversePattern()
    {
        for (int i = this.number; i > 0; i--) {
            for (int j = i; j > 0; j--) {
                System.out.print("* ");
            }
            System.out.println(" ");
        }
    }

    public void Swap(int a, int b)
    {
        System.out.println("First Value : ");
        System.out.println("A => " + a + ", B => " + b);
        System.out.println("Swap...");
        int c = a;
        a = b;
        b = c;
        System.out.println("A => " + a + ", B => " + b);
    }
}
