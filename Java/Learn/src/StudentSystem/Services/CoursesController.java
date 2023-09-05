package StudentSystem.Services;

import StudentSystem.Constants.Courses;

import java.util.Stack;

public class CoursesController {

    public static Boolean CheckCourses(Courses Course)
    {
        boolean Check;
        switch (Course) {
            case Math:
            case Science:
            case Arabic:
            case French:
            case English:
            case Physics:
            case EductionIslamique:
                Check = true;
                break;
            default:
                Check = false;
        }
        return Check;
    }
}
