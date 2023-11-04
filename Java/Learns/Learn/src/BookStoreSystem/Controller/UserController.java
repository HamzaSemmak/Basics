package BookStoreSystem.Controller;

import BookStoreSystem.Model.User;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;

public class UserController {
    private List<User> listOfUsers;

    public List<User> getListOfUsers() {
        return listOfUsers;
    }

    public void setListOfUsers(List<User> listOfUsers) {
        this.listOfUsers = listOfUsers;
    }

    public UserController()
    {
        this.listOfUsers = new ArrayList<>();
    }

    public void addNewUser(User user)
    {
        this.listOfUsers.add(user);
    }

    public void getTheListOfUsers()
    {
        System.out.println("This is the list Of Users : ");
        for (User user : listOfUsers)
        {
            System.out.println(user.toString());
        }
        System.out.println("\n");
    }

    public boolean authentication(String userName, String password)
    {
        List<User> Users = listOfUsers.stream()
                .filter(user -> Objects.equals(user.getUserName(), userName.toLowerCase()))
                .collect(Collectors.toList());

        for (User user : Users)
        {
            System.out.println(user.toString());
        }

        return !Users.isEmpty();
    }

    public void login(String userName, String password)
    {
        if(this.authentication(userName, password))
        {
            System.out.println("Welcome");
        }
        else {
            System.out.println("You are note welcome.");
        }
    }

}
