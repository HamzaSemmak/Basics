package ProblemSolvingAlgorithms;

import java.util.Arrays;

public class ProblemSolvingAlgorithms {
    //....
    public static void BubbleSort(int[] Array)
    {
        System.out.println("    - Bubble Sort : ");
        System.out.println("Initialize Array : " + Arrays.toString(Array));
        System.out.println("Sort...");
        for(int i = 0; i < Array.length; i++)
        {
            for (int j = 0; j < Array.length - i - 1; j++) {
                if (Array[j] > Array[j + 1]) {
                    int temp = Array[j];
                    Array[j] = Array[j + 1];
                    Array[j + 1] = temp;
                }
            }
        }
        System.out.println("Sorted Array => " + Arrays.toString(Array));
    }
}
