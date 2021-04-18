using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class Company
    {
        public Company()
        {
            Agents = new HashSet<Agent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Agent> Agents { get; set; }
    }
}
