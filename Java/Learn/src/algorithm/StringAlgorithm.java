package algorithm;

public class StringAlgorithm {

    public String str;

    public StringAlgorithm(String str)
    {
        this.str = str;
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
}
