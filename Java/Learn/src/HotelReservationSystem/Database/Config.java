package HotelReservationSystem.Database;

import Logger.log4j;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class Config {
    private static Config DB;
    private static final String DB_URL = "jdbc:mysql://localhost:3306/hotel_reservation_system";
    private static final String USERNAME = "root";
    private static final String PASSWORD = "";
    private Connection connection;

    public static Config DB() {
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
            log4j.error("MySQL JDBC driver not found");
            e.printStackTrace();
        }
    }
    public Connection getConnection() {
        try {
            if (connection == null || connection.isClosed()) {
                connection = DriverManager.getConnection(DB_URL, USERNAME, PASSWORD);
            }
        } catch (SQLException e) {
            log4j.error("Database connection error");
            e.printStackTrace();
        }
        return connection;
    }

    public void Close() {
        try {
            if (connection != null && !connection.isClosed()) {
                connection.close();
            }
        } catch (SQLException e) {
            log4j.error("Error closing the database connection");
            e.printStackTrace();
        }
    }
}
