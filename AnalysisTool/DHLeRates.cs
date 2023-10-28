using Microsoft.Data.Sqlite;
using System;

namespace AnalysisTool
{
    public class DHLeRates
    {
        private SqliteConnection conn;
        public void Initialize(SqliteConnection c)
        {
            conn = c;
        }

        public decimal Max(string table, string zone, string weight)
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

        public decimal Exp(string table, string zone, string weight)
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

        public decimal ExpP(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
            {
                var x = decimal.Parse(weight) / 16;
                var y = Math.Ceiling((decimal)x).ToString();
                return Query(table, zone, y);
            }
            else
            {
                return 0M;
            }
        }

        public decimal Ground(string table, string zone, string weight)
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

        public decimal GroundP(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
            {
                var x = decimal.Parse(weight) / 16;
                var y = Math.Ceiling((decimal)x).ToString();
                return Query(table, zone, y);
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

            reader.Read();
            if (decimal.TryParse(reader.GetString(0), out decimal cost))
            {
                return cost;
            }
            else return 0M;
        }
    }
}
