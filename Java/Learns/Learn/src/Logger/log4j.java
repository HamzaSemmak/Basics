package Logger;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;

public class log4j {
    
    private static BufferedWriter writer;

    public log4j() {

    }

    private static void logger(String level, String message)
    {
        try {
            FileWriter fileWriter = new FileWriter(log4jController.logPattern(), true); // Open file in append mode
            writer = new BufferedWriter(fileWriter);
            writer.write(
                    log4jController.logDate() +
                            " [" + Thread.currentThread() + "]" +
                            " [" + level + "]" +
                            " [" + log4jController.logger() + "]" +
                            " " + message +
                            "\n"
            );
            Console(level, message);
            writer.flush();
            writer.close();
        } catch (IOException e) {
            throw new Error("An error occurred while writing to the log file." + "\n" + e);
        }
    }

    private static void Console(String level, String message)
    {
        if(level.equals("INFO") || level.equals("WARNING")) System.out.println(message);
        if(level.equals("ERROR")) System.err.println(message);
    }

    public static void info(String message)
    {
        logger("INFO", message);
    }

    public static void error(String message)
    {
        logger("ERROR", message);
    }

    public static void warning(String message)
    {
        logger("WARNING", message);
    }
}
