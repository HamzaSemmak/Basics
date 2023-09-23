package HotelReservationSystem.Database.Migration;

import HotelReservationSystem.Database.Schema.Migration;
import HotelReservationSystem.Database.Schema.Types;

import java.util.HashMap;
import java.util.Map;

public class Run {
    public static Migration Migration = new Migration();

    public static void main(String[] args) {
        Map<String, String> Columns = new HashMap<String, String>() {{
            put("id", Types.ID);
            /* Start Columns */

            /* Start Columns */
            put("created_at", Types.TIMESTAMP);
        }};

        Migration.up("", Columns);
    }
}
