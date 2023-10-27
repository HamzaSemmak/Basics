public class Algorithms {

    /**
     *  Factorial Calculation :
     *
     * Calculate the factorial of a number using a recursive method.
     * @param n The integer for which the factorial is to be calculated.
     * @return The factorial of the given integer.
     */
    public static void FactorialCalculation(Integer n)
    {
        int sum = 1;
        System.out.println("Calculate the factorial of " + n + " : ");
        System.out.printf(n + "! = ");
        for (int i = 1; i < n + 1; i++) {
            System.out.printf(i + " ");
            sum *= i;
        }
        System.out.println("= " + sum);
    }

    /**
     *   Fibonacci Sequence :
     *
     * Generate the Fibonacci sequence
     * @param n the integer for the fibonacci is to be calculated.
     * @return the fibonacci of the given integer.
     */
    public static void FibonacciSequence(Integer n)
    {
        int n1 = 0, n2 = 1, n3 = 0;
        System.out.printf("Generate the fibonacci sequence of " + n + " : ");
        for(int i = 1; i <= n; i++) {
            System.out.printf(n3 + " ");
            n3 = n1 + n2;
            n1 = n2;
            n2 = n3;
        }
    }
}
