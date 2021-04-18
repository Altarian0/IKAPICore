using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class UserTour
    {
        public int UserId { get; set; }
        public int TourId { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual User User { get; set; }
    }
}
