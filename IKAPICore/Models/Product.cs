using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductComments = new HashSet<ProductComment>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ShopId { get; set; }
        public double? Price { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
