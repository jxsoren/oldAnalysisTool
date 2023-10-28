using AnalysisTool.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace AnalysisTool
{
    public class USPSRates
    {
        private SqliteConnection conn;
        public void Initialize(SqliteConnection c)
        {
            conn = c;
        }

        public decimal FC(string table, string zone, string weight)
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

        public decimal PM(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
            {
                return Query(table, zone, weight);
            }
            else return 0M;
        }

        public decimal PMCubic(string table, string zone, string cube)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(cube))
            {
                return Cubic(table + "Cubic", zone, cube.ToString());
            }
            else return 0M;
        }

        public decimal PME(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight) && float.Parse(weight) <= 70)
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal PS(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight) && float.Parse(weight) <= 70)
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal PSCubic(string table, string zone, decimal cube)
        {
            if (cube <= 1.0M && cube > 0 && !string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone))
            {
                if (table == "uspsCSPSCubic")
                {
                    return Cubic(table, zone, cube.ToString());
                }
                else
                {
                    return 0M;
                }
            }
            else
            {
                return 0M;
            }
        }

        public decimal FCPI(string table, string zone, string weight)
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

        public decimal PMI(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight) && float.Parse(weight) <= 70)
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal PMEI(string table, string zone, string weight)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight) && float.Parse(weight) <= 70)
            {
                return Query(table, zone, weight);
            }
            else
            {
                return 0M;
            }
        }

        public decimal Cubic(string table, string zone, string cube)
        {
            if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(cube))
            {
                var c = Query(table, zone, null, cube);

                return c;
            }
            else
            {
                return 0M;
            }
        }

        public USPSIntInfo USPSInt(string cc)
        {
            USPSIntInfo intInfo = new();

            if (!string.IsNullOrEmpty(cc))
            {
                var query = @$"SELECT PMI, PMIweight, PMEI, PMEIweight, FCPI FROM USPSint WHERE cc IS '{cc}'";

                using SqliteCommand command = new(query, conn);
                using SqliteDataReader reader = command.ExecuteReader();

                reader.Read();

                if (decimal.TryParse(reader.GetString(0), out decimal cost))
                {
                    intInfo.pmiZone = reader.GetString(0);
                    intInfo.pmiWeight = int.Parse(reader.GetString(1));
                    intInfo.pmeiZone = reader.GetString(2);
                    intInfo.pmeiWeight = int.Parse(reader.GetString(3));
                    intInfo.fcpiZone = reader.GetString(4);

                    return intInfo;
                }
                else return new USPSIntInfo();
            }
            else
            {
                return new USPSIntInfo();
            }
        }

        public decimal Query(string table, string? zone = null, string? weight = null, string? cube = null)
        {
            try
            {
                if (weight != "" && weight != null)
                {
                    if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(zone) && !string.IsNullOrEmpty(weight))
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
                    else
                    {
                        return 0M;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(cube))
                    {
                        var query = @$"SELECT zone{zone} FROM {table} WHERE cube IS {cube}";

                        using SqliteCommand command = new(query, conn);
                        using SqliteDataReader reader = command.ExecuteReader();

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 0M;
            }
        }
    }
}
