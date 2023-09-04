package LearnAlgorithm;

import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;

public class DataStructures {
    public static void main(String[] args) {
        //Arrays :
        String[] Colors = new String[5];
        Colors[0] = "Red";
        Colors[1] = "Blue";
        Colors[2] = "Green";
        Colors[3] = "White";
        Colors[4] = "Black";

        System.out.println(Arrays.toString(Colors));

        for(int i = 0; i < Colors.length; i++) {
            System.out.println(Colors[i]);
        }

        for(String Color : Colors)
        {
            System.out.println(Color);
        }

        Arrays.stream(Colors).forEach(System.out::println);

        char[][] Chars = new char[2][2];
        for (int i = 0; i < 2; i++) {
            for (int j = 0; j < i; j++) {
                Chars[i][j] = 'd';
            }
        }

        System.out.println(Arrays.deepToString(Chars));

        List<String> colors = new LinkedList<>();
        colors.add("Hello");
        colors.add("Hello");
        colors.add("Hello");
        colors.add("Hello");
        colors.add("Hello");
        colors.add("Hello");
        colors.add("Hello");

        System.out.println(colors);
    }
}
