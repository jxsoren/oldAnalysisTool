using Microsoft.Data.Sqlite;

namespace AnalysisTool.Helpers
{
    public class DbHelpers : IDbHelpers
    {
        public readonly string dbFilePath = "C:\\Users\\analyst7\\Documents\\rates.db";
        private SqliteConnection inMemoryConnection = null;

        public DbHelpers()
        {

        }

        public SqliteConnection GetPhysicalDbConnection()
        {
            var dbConnection = new SqliteConnection("Data Source = " + dbFilePath + ";Mode=ReadWrite");
            dbConnection.Open();
            return dbConnection;
        }

        public SqliteConnection GetInMemoryDbConnection()
        {
            if (inMemoryConnection == null)
            {
                inMemoryConnection = new SqliteConnection("Data Source = :memory:");
                inMemoryConnection.Open();
                return inMemoryConnection;
            }
            return inMemoryConnection;
        }
    }

    public interface IDbHelpers
    {
        SqliteConnection GetPhysicalDbConnection();
        SqliteConnection GetInMemoryDbConnection();
    }
}
