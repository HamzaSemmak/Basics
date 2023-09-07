package StudentSystem;

import StudentSystem.Constants.Courses;

import java.util.HashMap;
import java.util.Map;

public class Program {

    public static void main(String[] args) {
        Map<Courses, Integer> enrolledCourses = new HashMap<Courses, Integer>();

        Student S1 = new Student(
                "Hamza", "Semmak", "2001-07-28",
                "Hamza@gmail.com", "066778655", "IMM 7 APPT 13 CYM, RABAT",
                enrolledCourses
        );

        System.out.println("---------------- Student -------------------");
        S1.AddEnrolledCourses(Courses.Math, 5);
        S1.AddEnrolledCourses(Courses.Science, 5);
        S1.AddEnrolledCourses(Courses.Arabic, 5);
        S1.AddEnrolledCourses(Courses.French, 5);
        S1.AddEnrolledCourses(Courses.English, 5);
        S1.AddEnrolledCourses(Courses.Physics, 5);
        S1.AddEnrolledCourses(Courses.EductionIslamique, 5);
        System.out.println("\n");
        System.out.println(S1.toString());
        System.out.println("\n");
        System.out.println("Now let's remove One Course from list");
        S1.RemoveEnrolledCourses(Courses.Math);
        System.out.println("\n");
        System.out.println("Add The Removed Courses");
        S1.AddEnrolledCourses(Courses.Math, 5);
        System.out.println("\n");
        S1.CalculateGPA();
        System.out.println("\n");
        S1.StudentTranscript();

        System.out.println("\n");

        System.out.println("---------------- Instructor -------------------");
        Instructor instructor = new Instructor("Omar", "Amaoune", "Omar@gmail.com", "0667786595");
        System.out.println(instructor.toString());

        System.out.println("\n");

        System.out.println("---------------- Course -------------------");
        Course Math = new Course(Courses.Math, "Credits Math", instructor);
        Course Physics = new Course(Courses.Physics, "Credits Physics", instructor);
        Course Arabic = new Course(Courses.Arabic, "Credits Arabic", instructor);
        Course English = new Course(Courses.English, "Credits English", instructor);
        Course French = new Course(Courses.French, "Credits French", instructor);
        Course Science = new Course(Courses.Science, "Credits English", instructor);
        Course EductionIslamique = new Course(Courses.EductionIslamique, "Credits Eduction Islamique", instructor);
        System.out.println("\n");
        System.out.println(Math.toString());
        System.out.println(Physics.toString());
        System.out.println(EductionIslamique.toString());

        System.out.println("\n");

        System.out.println("---------------- StudentSystem -------------------");
        StudentSystem studentSystem = new StudentSystem();
        Student student1 = new Student(
                "Ella", "Williams", "2001-04-15",
                "ella.williams@example.com", "555-222-1111", "888 Oak St",
                enrolledCourses
        );
        Student student2 = new Student(
                "James", "Taylor", "2000-09-05",
                "james.taylor@example.com", "555-333-4444", "777 Elm St",
                enrolledCourses
        );
        Student student3 = new Student(
                "Alice", "Johnson", "2000-03-12",
                "alice.johnson@example.com", "555-987-6543", "789 Elm St",
                enrolledCourses
        );
        Student student4 = new Student(
                "Bob", "Wilson", "2001-08-25",
                "bob.wilson@example.com", "555-222-3333", "456 Oak St",
                enrolledCourses
        );
        Student student5 = new Student(
                "Emily", "Davis", "1999-11-03",
                "emily.davis@example.com", "555-111-2222", "101 Pine St",
                enrolledCourses
        );

        studentSystem.newStudent(student1);
        studentSystem.newStudent(student2);
        studentSystem.newStudent(student3);
        studentSystem.newStudent(student4);
        studentSystem.newStudent(student5);
        System.out.println("\n");
        studentSystem.getAllStudent();
        System.out.println("\n");
        studentSystem.newCourse(Math);
        studentSystem.newCourse(Physics);
        studentSystem.newCourse(Science);
        studentSystem.newCourse(Arabic);
        studentSystem.newCourse(French);
        studentSystem.newCourse(English);
        studentSystem.newCourse(EductionIslamique);
        System.out.println("\n");
        studentSystem.getAllCourses();
        System.out.println("\n");
        studentSystem.calculateGPAForEveryStudent();
        System.out.println("\n");
        studentSystem.generateTranscriptsForEveryStudentInLists();
        System.out.println("\n");
        studentSystem.getAllInformationAboutStudent();

        System.out.println("\n \n");
        System.out.println("Test 1 passed Successfully, (Class Student).");
        System.out.println("Test 2 passed Successfully, (Class Course).");
        System.out.println("Test 3 passed Successfully, (Class Instructor).");
        System.out.println("Test 4 passed Successfully, (Class Student System).");
        System.out.println("Done!");
    }
}
