using Microsoft.Data.Sqlite;

namespace AnalysisTool
{
    public class MIRates
    {
        private SqliteConnection conn;
        public void Initialize(SqliteConnection c)
        {
            conn = c;
        }
        public decimal PSLW(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal PSHW(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal Query(string table, string zone, string weight)
        {
            var query = @$"SELECT zone{zone} FROM {table} WHERE weight IS {weight}";

            using SqliteCommand command = new(query, conn);
            using SqliteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (decimal.TryParse(reader.GetString(0), out decimal cost))
                {
                    return cost;
                }
                else return 0M;
            }
            else
            {
                return 0M;
            }
        }
    }
}
