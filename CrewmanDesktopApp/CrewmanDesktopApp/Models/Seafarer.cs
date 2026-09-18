using System;

namespace CrewmanDesktopApp.Models
{
    /// <summary>Pomorac - odgovara retku tablice dbo.Seafarers.</summary>
    public class Seafarer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public int RankId { get; set; }

        /// <summary>Brod na koji je pomorac ukrcan; null ako trenutno nije ukrcan.</summary>
        public int? VesselId { get; set; }

        /// <summary>Datum ukrcaja; null ako pomorac nije ukrcan.</summary>
        public DateTime? EmbarkationDate { get; set; }

        public string FullName
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }
    }
}
