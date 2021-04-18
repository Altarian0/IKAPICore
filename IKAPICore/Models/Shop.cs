using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoImage { get; set; }
        public string Description { get; set; }
        public int? PlaceId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Telegram { get; set; }

        public virtual Place Place { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
