package LearnJDBC.Helpers;

import java.sql.Timestamp;
import java.util.Date;

public class Carbon {
    private static final Date now = new Date();
    private static final Timestamp Timestamp = new Timestamp(now.getTime());

    public static String Timestamp()
    {
        return Timestamp.toString();
    }
}
