using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CrewmanDesktopApp.Models;

namespace CrewmanDesktopApp.Data
{
    /// <summary>Pristup šifrarnicima: rangovi (dbo.Ranks) i brodovi (dbo.Vessels).</summary>
    public static class LookupRepository
    {
        public static List<Rank> GetRanks()
        {
            var ranks = new List<Rank>();

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand("SELECT Id, Name FROM dbo.Ranks ORDER BY Name;", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ranks.Add(new Rank { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }

            return ranks;
        }

        public static List<Vessel> GetVessels()
        {
            var vessels = new List<Vessel>();

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand("SELECT Id, Name FROM dbo.Vessels ORDER BY Name;", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    vessels.Add(new Vessel { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                }
            }

            return vessels;
        }

        /// <summary>Dodaje novi rang. Ako rang s istim nazivom već postoji, vraća njegov Id.</summary>
        public static int AddRank(string name)
        {
            return AddLookupValue("dbo.Ranks", name);
        }

        /// <summary>Dodaje novi brod. Ako brod s istim nazivom već postoji, vraća njegov Id.</summary>
        public static int AddVessel(string name)
        {
            return AddLookupValue("dbo.Vessels", name);
        }

        private static int AddLookupValue(string tableName, string name)
        {
            // tableName dolazi isključivo iz koda (konstante iznad), nikad od korisnika
            string sql =
                "IF NOT EXISTS (SELECT 1 FROM " + tableName + " WHERE Name = @Name)\n" +
                "    INSERT INTO " + tableName + " (Name) VALUES (@Name);\n" +
                "SELECT Id FROM " + tableName + " WHERE Name = @Name;";

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = name.Trim();
                return (int)command.ExecuteScalar();
            }
        }
    }
}
