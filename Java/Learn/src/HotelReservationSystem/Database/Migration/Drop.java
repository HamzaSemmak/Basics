package HotelReservationSystem.Database.Migration;

import HotelReservationSystem.Database.Schema.Migration;
import HotelReservationSystem.Database.Schema.Types;

import java.util.HashMap;
import java.util.Map;

public class Drop {
    public static Migration Migration = new Migration();

    public static void main(String[] args) {
        String Table = ""; /* Name Of the Table that you want to delete */
        Migration.down(Table);
    }
}
