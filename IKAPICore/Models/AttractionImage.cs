using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class AttractionImage
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public int? AttractionId { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
