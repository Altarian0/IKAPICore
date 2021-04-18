using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class ProductComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Rating { get; set; }
        public int? ProductId { get; set; }
        public int? AuthorId { get; set; }

        public virtual Product Product { get; set; }
    }
}
