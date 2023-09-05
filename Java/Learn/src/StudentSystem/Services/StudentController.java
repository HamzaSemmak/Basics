package StudentSystem.Services;

public class StudentController {

    public static String GPAGrades(double GPA)
    {
        String Grade = (GPA > 4 && GPA <= 5) ? "A" :
                (GPA > 3 && GPA <= 4) ? "B" :
                        (GPA > 2 && GPA <= 3) ? "C" :
                                (GPA > 1 && GPA <= 2) ? "D" :
                                        (GPA >= 0 && GPA <= 1) ? "F" :
                                                "(Invalid Calculate for GPA)";

        return Grade;
    }

}
