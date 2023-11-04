package StudentSystem;

import StudentSystem.Constants.Courses;

public class Course {
    private static int newCourse = 1;

    private int ID;

    private Courses CourseName;

    private String Credits;

    private Instructor Instructor;

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public Courses getCourseName() {
        return CourseName;
    }

    public void setCourseName(Courses courseName) {
        CourseName = courseName;
    }

    public String getCredits() {
        return Credits;
    }

    public void setCredits(String credits) {
        Credits = credits;
    }

    public Instructor getInstructorName() {
        return Instructor;
    }

    public void setInstructorName(Instructor instructorName) { Instructor = Instructor; }

    public Course(Courses courseName, String credits, Instructor instructor)
    {
        this.ID = newCourse++;
        this.CourseName = courseName;
        this.Credits = credits;
        this.Instructor = instructor;

        System.out.println("(1) Row affected Successfully.");
    }

    @Override
    public String toString()
    {
        return "Course N°:" + ID + "\n" +
                "CourseName : " + CourseName +
                ", Credits : " + Credits +
                ", Instructor Information : " + Instructor.toString();
    }

}
