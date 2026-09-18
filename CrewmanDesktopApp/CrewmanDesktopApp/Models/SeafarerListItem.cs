using System;

namespace CrewmanDesktopApp.Models
{
    /// <summary>Redak u tablici rezultata pretrage (pomorac s nazivom ranga i broda).</summary>
    public class SeafarerListItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RankName { get; set; }
        public string VesselName { get; set; }
        public DateTime? EmbarkationDate { get; set; }

        public string FullName
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }
    }
}
