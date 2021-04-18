using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Agent
    {
        public Agent()
        {
            Tours = new HashSet<Tour>();
        }

        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }

        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
