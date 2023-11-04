package LearnJDBC.Service.Auth;

import LearnJDBC.Database.Config;
import LearnJDBC.Model.User;
import Logger.log4j;

import java.sql.ResultSet;
import java.sql.SQLException;

public class LoginService {
    /*
    |--------------------------------------------------------------------------
    | Login Service
    |--------------------------------------------------------------------------
    |
    | This service handles authenticating users for the application and
    | redirecting them to your home screen. The service uses a trait
    | to conveniently provide its functionality to your applications.
    |
    */
    public static Boolean isLogged = false;

    public static User user;

    private static boolean Select(User user)
    {
        try {
            String Query = "select * from users where email = '" + user.getEmail() + "' and password like '" + user.getPassword() + "'; ";
            ResultSet resultSet = Config.DAO().Statement().executeQuery(Query);
            if (resultSet.next()) {
                isLogged = true;
                User(resultSet);
            }
        } catch (SQLException e) {
            log4j.error("Error occurred in LoginService; \n" + e);
        }
        return isLogged;
    }

    private static void User(ResultSet Request)
    {
        try {
            user = new User(
                    Request.getInt("id"),
                    Request.getString("name"),
                    Request.getString("email"),
                    Request.getString("password"),
                    Request.getString("created_at")
            );
        } catch (SQLException e) {
            log4j.error("Error occurred in LoginService(Request Reader); \n" + e);
        }
    }

    public static void Login(User user)
    {
        if(Select(user))
        {
            log4j.info("Welcome " + user.getEmail());
        }
        else {
            log4j.error("Email or password is invalid, Please try again. ");
        }
    }
}
