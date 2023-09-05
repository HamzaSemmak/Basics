package StudentSystem;


import StudentSystem.Constants.Courses;
import StudentSystem.Services.CoursesController;
import StudentSystem.Services.StudentController;

import java.util.HashMap;
import java.util.Map;

public class Student {
    private static int newStudentID = 1;
    private int ID;

    private String FirstName;

    private String LastName;

    private String DateOfBirth;

    private String Email;

    private String Phone;

    private String Address;

    private Map<Courses, Integer> EnrolledCourses = new HashMap<Courses, Integer>();


    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getFirstName() {
        return FirstName;
    }

    public void setFirstName(String firstName) {
        FirstName = firstName;
    }

    public String getLastName() {
        return LastName;
    }

    public void setLastName(String lastName) {
        LastName = lastName;
    }

    public String getDateOfBirth() {
        return DateOfBirth;
    }

    public void setDateOfBirth(String dateOfBirth) {
        DateOfBirth = dateOfBirth;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getPhone() {
        return Phone;
    }

    public void setPhone(String phone) {
        Phone = phone;
    }

    public String getAddress() {
        return Address;
    }

    public void setAddress(String Address) {
        Address = Address;
    }

    public Map<Courses, Integer> getEnrolledCourses() {
        return EnrolledCourses;
    }

    public void setEnrolledCourses(Map<Courses, Integer> enrolledCourses) {
        EnrolledCourses = enrolledCourses;
    }

    public Student(String firstName, String lastName, String dateOfBirth, String email, String phone, String Address, Map<Courses, Integer> enrolledCourses)
    {
        this.ID = newStudentID++;

        this.FirstName = firstName;
        this.LastName = lastName;
        this.DateOfBirth = dateOfBirth;
        this.Email = email;
        this.Phone = phone;
        this.Address = Address;
        this.EnrolledCourses = enrolledCourses;

        System.out.println("(1) Row affected Successfully.");
    }

    @Override
    public String toString()
    {
        return "Student N°: " + ID + "`\n Student FullName is " + FirstName + " " + LastName +
                ", Date Of Birth is " + DateOfBirth + " and his Contact is : \n" +
                "Email : " + Email + "\n" +
                "Phone : " + Phone + "\n" +
                "Address : " + Address + "\n" +
                "And this the list of His Enrolled Courses : \n" +
                EnrolledCourses;
    }

    public void AddEnrolledCourses(Courses Course, Integer Note)
    {
        if(!CoursesController.CheckCourses(Course))
        {
            throw new Error("Error : Invalid Course in our Lists");
        }
        else {
            if(Note >= 0 && Note <= 5) {
                this.EnrolledCourses.put(Course, Note);
                System.out.println("(1) Row affected Successfully.");
            }
            else {
                throw  new Error("Error : Invalid Note, Please try Again!");
            }
        }
    }

    public void RemoveEnrolledCourses(Courses Course)
    {
        this.EnrolledCourses.remove(Course);
        System.out.println("(1) Row deleted Successfully.");
    }

    public void CalculateGPA()
    {
        double GPA = 0;
        for (Map.Entry<Courses, Integer> entry : EnrolledCourses.entrySet()) {
            Courses course = entry.getKey();
            Integer grade = entry.getValue();

            GPA += grade;
        }
        GPA /= EnrolledCourses.size();
        System.out.println("Student N°:" + ID + " is " + StudentController.GPAGrades(GPA));
    }

    public void StudentTranscript()
    {
        System.out.println("Student Student N°: " + ID);
        for (Map.Entry<Courses, Integer> entry : EnrolledCourses.entrySet()) {
            Courses course = entry.getKey();
            Integer grade = entry.getValue();
            System.out.println("Course : " + course + ", Note : " + grade + ", Grade that he got is " + StudentController.GPAGrades(grade));
        }
    }
}

