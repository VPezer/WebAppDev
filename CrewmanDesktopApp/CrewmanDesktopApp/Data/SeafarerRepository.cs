using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CrewmanDesktopApp.Models;

namespace CrewmanDesktopApp.Data
{
    /// <summary>Sav pristup tablici dbo.Seafarers - parametrizirani T-SQL upiti preko ADO.NET-a.</summary>
    public static class SeafarerRepository
    {
        /// <summary>Najveći broj riječi iz polja za pretragu koji se koristi u upitu.</summary>
        private const int MaxSearchTerms = 5;

        /// <summary>
        /// Usporedba neosjetljiva na velika/mala slova i dijakritike: "peric" pronalazi "Perić",
        /// "korcula" pronalazi "Korčula" (zadane kolacije SQL Servera razlikuju č/c, š/s ...).
        /// </summary>
        private const string SearchCollation = " COLLATE Latin1_General_100_CI_AI LIKE ";

        private const string SelectListSql =
            "SELECT s.Id, s.FirstName, s.LastName, r.Name AS RankName, v.Name AS VesselName, s.EmbarkationDate\n" +
            "FROM dbo.Seafarers AS s\n" +
            "INNER JOIN dbo.Ranks AS r ON r.Id = s.RankId\n" +
            "LEFT JOIN dbo.Vessels AS v ON v.Id = s.VesselId\n";

        /// <summary>
        /// Pretraga pomoraca. Svaka riječ iz <paramref name="searchText"/> mora se pojaviti u imenu,
        /// prezimenu, nazivu ranga ili nazivu broda; <paramref name="rankId"/> i <paramref name="vesselId"/>
        /// dodatno sužavaju rezultat (null = bez filtra).
        /// </summary>
        public static List<SeafarerListItem> Search(string searchText, int? rankId, int? vesselId)
        {
            var conditions = new List<string>();
            var parameters = new List<SqlParameter>();

            if (rankId.HasValue)
            {
                conditions.Add("s.RankId = @RankId");
                parameters.Add(new SqlParameter("@RankId", SqlDbType.Int) { Value = rankId.Value });
            }

            if (vesselId.HasValue)
            {
                conditions.Add("s.VesselId = @VesselId");
                parameters.Add(new SqlParameter("@VesselId", SqlDbType.Int) { Value = vesselId.Value });
            }

            string[] terms = (searchText ?? string.Empty)
                .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .Take(MaxSearchTerms)
                .ToArray();

            for (int i = 0; i < terms.Length; i++)
            {
                string p = "@Term" + i;
                conditions.Add(
                    "(s.FirstName" + SearchCollation + p + " OR s.LastName" + SearchCollation + p +
                    " OR r.Name" + SearchCollation + p + " OR v.Name" + SearchCollation + p + ")");
                parameters.Add(new SqlParameter(p, SqlDbType.NVarChar, 200)
                {
                    Value = "%" + EscapeLike(terms[i]) + "%"
                });
            }

            string sql = SelectListSql;
            if (conditions.Count > 0)
            {
                sql += "WHERE " + string.Join("\n  AND ", conditions) + "\n";
            }
            sql += "ORDER BY s.LastName, s.FirstName, s.Id;";

            var items = new List<SeafarerListItem>();

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters.ToArray());

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new SeafarerListItem
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            RankName = reader.GetString(3),
                            VesselName = reader.IsDBNull(4) ? null : reader.GetString(4),
                            EmbarkationDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        });
                    }
                }
            }

            return items;
        }

        /// <summary>Vraća pomorca prema Id-u ili null ako ne postoji.</summary>
        public static Seafarer GetById(int id)
        {
            const string sql =
                "SELECT Id, FirstName, LastName, DateOfBirth, Nationality, Email, RankId, VesselId, EmbarkationDate\n" +
                "FROM dbo.Seafarers\n" +
                "WHERE Id = @Id;";

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        return null;
                    }

                    return new Seafarer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        DateOfBirth = reader.GetDateTime(3),
                        Nationality = reader.GetString(4),
                        Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                        RankId = reader.GetInt32(6),
                        VesselId = reader.IsDBNull(7) ? (int?)null : reader.GetInt32(7),
                        EmbarkationDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8)
                    };
                }
            }
        }

        /// <summary>Ubacuje novog pomorca i vraća dodijeljeni Id.</summary>
        public static int Insert(Seafarer seafarer)
        {
            const string sql =
                "INSERT INTO dbo.Seafarers (FirstName, LastName, DateOfBirth, Nationality, Email, RankId, VesselId, EmbarkationDate)\n" +
                "VALUES (@FirstName, @LastName, @DateOfBirth, @Nationality, @Email, @RankId, @VesselId, @EmbarkationDate);\n" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                AddDataParameters(command, seafarer);
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>Sprema izmjene postojećeg pomorca.</summary>
        public static void Update(Seafarer seafarer)
        {
            const string sql =
                "UPDATE dbo.Seafarers\n" +
                "SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth,\n" +
                "    Nationality = @Nationality, Email = @Email, RankId = @RankId,\n" +
                "    VesselId = @VesselId, EmbarkationDate = @EmbarkationDate\n" +
                "WHERE Id = @Id;";

            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = seafarer.Id;
                AddDataParameters(command, seafarer);

                if (command.ExecuteNonQuery() == 0)
                {
                    throw new InvalidOperationException("Pomorac više ne postoji u bazi - možda ga je netko u međuvremenu obrisao.");
                }
            }
        }

        /// <summary>Briše pomorca. Brisanje nepostojećeg Id-a nije greška.</summary>
        public static void Delete(int id)
        {
            using (SqlConnection connection = Db.OpenConnection())
            using (var command = new SqlCommand("DELETE FROM dbo.Seafarers WHERE Id = @Id;", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        private static void AddDataParameters(SqlCommand command, Seafarer s)
        {
            command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = s.FirstName;
            command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = s.LastName;
            command.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = s.DateOfBirth;
            command.Parameters.Add("@Nationality", SqlDbType.NVarChar, 100).Value = s.Nationality;
            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = (object)s.Email ?? DBNull.Value;
            command.Parameters.Add("@RankId", SqlDbType.Int).Value = s.RankId;
            command.Parameters.Add("@VesselId", SqlDbType.Int).Value = (object)s.VesselId ?? DBNull.Value;
            command.Parameters.Add("@EmbarkationDate", SqlDbType.Date).Value = (object)s.EmbarkationDate ?? DBNull.Value;
        }

        /// <summary>Znakovi %, _ i [ imaju posebno značenje u LIKE-u pa ih treba "pobjeći" da bi se tražili doslovno.</summary>
        private static string EscapeLike(string value)
        {
            return value.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
        }
    }
}
