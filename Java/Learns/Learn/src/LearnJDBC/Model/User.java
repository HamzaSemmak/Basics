package LearnJDBC.Model;

public class User {
    private int ID;
    private String name;
    private String email;
    private String password;
    private String TIMESTAMP;

    public int getID() {
        return ID;
    }
    public void setID(int ID) {
        this.ID = ID;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public String getEmail() {
        return email;
    }
    public void setEmail(String email) {
        this.email = email;
    }
    public String getPassword() {
        return password;
    }
    public void setPassword(String password) {
        this.password = password;
    }
    public String getTIMESTAMP() {
        return TIMESTAMP;
    }
    public void setTIMESTAMP(String TIMESTAMP) {
        this.TIMESTAMP = TIMESTAMP;
    }

    public User() { }

    public User(int id, String name, String email, String password, String timestamp) {
        this.ID = id;
        this.name = name;
        this.email = email;
        this.password = password;
        this.TIMESTAMP = timestamp;
    }

    public User(String email, String password) {
        this.email = email;
        this.password = password;
    }

    @Override
    public String toString() {
        return "User{" +
                "ID=" + ID +
                ", name='" + name + '\'' +
                ", email='" + email + '\'' +
                ", password='" + password + '\'' +
                ", TIMESTAMP='" + TIMESTAMP + '\'' +
                '}';
    }
}
