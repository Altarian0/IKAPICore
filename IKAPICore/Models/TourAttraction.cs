using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class TourAttraction
    {
        public int TourId { get; set; }
        public int AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
