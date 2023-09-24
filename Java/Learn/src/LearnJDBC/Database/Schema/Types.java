package LearnJDBC.Database.Schema;

public class Types {

    public static final String ID = Types.INT + Types.AUTO_INCREMENT + ", " + Types.PRIMARY("id");
    public static final String INT = "int ";
    public static final String TEXT = "text ";
    public static final String BOOLEAN = "boolean ";
    public static final String FLOAT = "float ";
    public static final String DOUBLE = "double ";
    public static final String DATE = "date ";
    public static final String TIMESTAMP = "TIMESTAMP ";
    public static final String AUTO_INCREMENT= "AUTO_INCREMENT ";
    public static String VARCHAR(int size) { return "varchar(" + size + ") "; };
    public static String CHAR(int size) { return "char(" + size + ") "; };
    public static String FLOAT(int size) { return "float(" + size + ") "; };
    public static String DOUBLE(int size) { return "double(" + size + ") "; };
    public static String PRIMARY(String Column) { return "PRIMARY KEY (" + Column + ") "; };
    public static String FOREIGN(String Column, String Type, String Table, String newColumn) { return Column + " " + Type + ", FOREIGN KEY (" + Column + ") REFERENCES " + Table + "(" + newColumn + ") "; };
    public static String DEFAULT(String Default) { return "DEFAULT " + Default + " "; };
    public static String Query(String Query) { return  Query; };
}