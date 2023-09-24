package LearnJDBC.Service.Auth;

import LearnJDBC.Database.Config;
import LearnJDBC.Helpers.Carbon;
import LearnJDBC.Model.User;
import Logger.log4j;

import java.sql.SQLException;

public class RegisterService {

    /*
    |--------------------------------------------------------------------------
    | Register Service
    |--------------------------------------------------------------------------
    |
    | This service handles the registration of new users as well as their
    | validation and creation. By default this service uses a trait to
    | provide this functionality without requiring any additional code.
    |
    */
    public static void Create(User user)
    {
        try {
            String Query = "insert into users(name, email, password, created_at) values" +
                    "('" + user.getName() + "', '" + user.getEmail() + "', '" + user.getPassword() + "', '" + Carbon.Timestamp() + "')";
            Config.DAO().Statement().executeUpdate(Query);
            log4j.info("(1) Row affected Successfully.");
        } catch (SQLException e) {
            log4j.error("Error occurred in RegisterService; \n" + e);
        }
    }

}
