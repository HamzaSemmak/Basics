package LearnJDBC.Database;

import Logger.log4j;

import java.sql.*;

public class Config {
    private static Config DB;
    private static final String DB_URL = "jdbc:mysql://localhost:3306/hotel_reservation_system";
    private static final String USERNAME = "root";
    private static final String PASSWORD = "";
    private Connection connection;
    private Statement statement;

    public static Config DAO() {
        if (DB == null) {
            DB = new Config();
        }
        return DB;
    }

    private Config() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            log4j.info("Connection...");
        } catch (ClassNotFoundException e) {
            log4j.error("MySQL JDBC driver not found \n" + e);
        }
    }
    public Connection Connection() {
        try {
            if (connection == null || connection.isClosed()) {
                connection = DriverManager.getConnection(DB_URL, USERNAME, PASSWORD);
            }
        } catch (SQLException e) {
            log4j.error("Database connection error \n" + e);
        }
        return connection;
    }

    public void Close() {
        try {
            if (connection != null && !connection.isClosed()) {
                connection.close();
            }
        } catch (SQLException e) {
            log4j.error("Error closing the database connection \n" + e);
        }
    }

    public Statement Statement()
    {
        try {
            statement = this.Connection().createStatement();
        } catch (SQLException e) {
            log4j.error("Error in Statement \n" + e);
        }
        return statement;
    }
}
