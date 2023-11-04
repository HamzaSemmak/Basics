package StudentSystem;

import StudentSystem.Constants.Courses;

import java.util.ArrayList;
import java.util.List;

public class StudentSystem {

    private List<Student> listOfStudent = new ArrayList<Student>();

    private List<Course> listOfCourses = new ArrayList<Course>();

    public StudentSystem()
    {

    }

    public void newStudent(Student student)
    {
        this.listOfStudent.add(student);
        System.out.println("(1) Row affected Successfully.");
    }

    public void newCourse(Course course)
    {
        this.listOfCourses.add(course);
        System.out.println("(1) Row affected Successfully.");
    }

    public void getAllStudent()
    {
        System.out.println("This is the list of Students : ");
        for (Student student : listOfStudent) {
            System.out.println(student.toString());
            System.out.println(" ");
        }
    }

    public void getAllCourses()
    {
        System.out.println("This is the list of Courses : ");
        for (Course course : listOfCourses) {
            System.out.println(course.toString());
            System.out.println(" ");
        }
    }

    public void calculateGPAForEveryStudent()
    {
        System.out.println("This is the list of Students GPA : ");
        for (Student student : listOfStudent) {
            student.CalculateGPA();
            System.out.println(" ");
        }
    }

    public void generateTranscriptsForEveryStudentInLists()
    {
        System.out.println("This is the list of Student Transcript : ");
        for (Student student : listOfStudent) {
            student.StudentTranscript();
            System.out.println("\n");
        }
    }

    public void getAllInformationAboutStudent()
    {
        System.out.println("This is the list of Student Transcript : ");
        for (Student student : listOfStudent) {
            System.out.println(student.toString());
            System.out.println("GPA Calculate : ");
            student.CalculateGPA();
            System.out.println("Generate Transcript : ");
            student.StudentTranscript();
            System.out.println("\n");
        }
    }
}
