using System;
using System.Collections.Generic;

#nullable disable

namespace IKAPICore.Models
{
    public partial class User
    {
        public User()
        {
            TourComments = new HashSet<TourComment>();
            UserTours = new HashSet<UserTour>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public byte[] AvatarImage { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? GenderId { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual Role Role { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual ICollection<TourComment> TourComments { get; set; }
        public virtual ICollection<UserTour> UserTours { get; set; }
    }
}
