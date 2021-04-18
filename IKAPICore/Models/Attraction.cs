using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Attraction
    {
        public Attraction()
        {
            AttractionComments = new HashSet<AttractionComment>();
            AttractionImages = new HashSet<AttractionImage>();
            TourAttractions = new HashSet<TourAttraction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PlaceId { get; set; }

        public virtual Place Place { get; set; }
        public virtual ICollection<AttractionComment> AttractionComments { get; set; }
        public virtual ICollection<AttractionImage> AttractionImages { get; set; }
        public virtual ICollection<TourAttraction> TourAttractions { get; set; }
    }
}
