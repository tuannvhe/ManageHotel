using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Province
    {
        public Province()
        {
            Hotels = new HashSet<Hotel>();
        }

        public int Id { get; set; }
        public string? ProvinceName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
