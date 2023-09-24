package LearnJDBC.Database.Schema;

import LearnJDBC.Database.Config;
import LearnJDBC.Service.MigrationService;
import Logger.log4j;

import java.sql.SQLException;
import java.util.Map;

public class Migration {
    protected final Config Config;
    protected final MigrationService MigrationService;

    public Migration()
    {
        Config = LearnJDBC.Database.Config.DAO();
        MigrationService = new MigrationService();
    }

    /**
     * Run the migrations.
     *
     * @return void
     */
    public void up(String Table, Map<String, String> Column)
    {
        try
        {
            String Query = "CREATE TABLE IF NOT EXISTS " + Table + " (" + MigrationService.MapPipe(Column) + ")";
            Config.Statement().executeUpdate(Query);
            log4j.info("Table " + Table + " created successfully.");
        }
        catch (SQLException e)
        {
            log4j.error("Error occurred in Migration(Create); \n" + e);
        }
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public void down(String Table)
    {
        try
        {
            String Query = "DROP TABLE " + Table;
            Config.Statement().executeUpdate(Query);
            log4j.info("Table " + Table + " deleted successfully.");
        }
        catch (SQLException e)
        {
            log4j.error("Error occurred in Migration(Drop); \n" + e);
        }
    }
}