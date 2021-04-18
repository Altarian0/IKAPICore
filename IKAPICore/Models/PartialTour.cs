using System;

namespace IKAPICore.Models
{
    public partial class Tour
    {
        public bool IsPast => StartDate < DateTime.Now;
    }
}