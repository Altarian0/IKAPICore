using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class AttractionComment
    {
        public int Id { get; set; }
        public int? AttractionId { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
