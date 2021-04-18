using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Tour
    {
        public Tour()
        {
            TourAttractions = new HashSet<TourAttraction>();
            TourComments = new HashSet<TourComment>();
            UserTours = new HashSet<UserTour>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FromPlaceId { get; set; }
        public int? ToPlaceId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Price { get; set; }
        public int? AgentId { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Place FromPlace { get; set; }
        public virtual Place ToPlace { get; set; }
        public virtual ICollection<TourAttraction> TourAttractions { get; set; }
        public virtual ICollection<TourComment> TourComments { get; set; }
        public virtual ICollection<UserTour> UserTours { get; set; }
    }
}
