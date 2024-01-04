using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Recharges = new HashSet<Recharge>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Phone { get; set; }
        public string? Email { get; set; }
        public decimal? Money { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Recharge> Recharges { get; set; }
    }
}
