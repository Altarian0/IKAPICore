using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class TourComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }
        public int TourId { get; set; }
        public int? AuthorId { get; set; }

        public virtual User Author { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
