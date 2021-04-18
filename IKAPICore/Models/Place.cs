using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Place
    {
        public Place()
        {
            Attractions = new HashSet<Attraction>();
            Shops = new HashSet<Shop>();
            TourFromPlaces = new HashSet<Tour>();
            TourToPlaces = new HashSet<Tour>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
        public virtual ICollection<Tour> TourFromPlaces { get; set; }
        public virtual ICollection<Tour> TourToPlaces { get; set; }
    }
}
