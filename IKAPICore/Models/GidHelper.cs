using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class GidHelper
    {
        public int Id { get; set; }
        public int? Gidid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
