package LearnJDBC.Database.Migration;

import LearnJDBC.Database.Schema.Migration;

public class Drop {
    public static Migration Migration = new Migration();

    public static void main(String[] args) {
        /* Name Of the Table that you want to delete */
        Migration.down("");
    }
}
