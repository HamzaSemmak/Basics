package StudentSystem;

import StudentSystem.Constants.Courses;

import java.nio.file.FileStore;
import java.util.Map;

public class Instructor {
    private static int newInstructor = 1;
    private int ID;
    private String FirstName;
    private String LastName;
    private String Email;
    private String Phone;

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

    public Instructor(String firstName, String lastName, String email, String phone)
    {
        this.ID = newInstructor++;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.Phone = phone;
    }

    public String toString()
    {
        return  "Instruction N°:" + ID + "\n" +
                "Full Name : " + FirstName + " " + LastName + "\n" +
                "Phone : " + Phone + "\n" +
                "Email : " + Email;
    }
}
