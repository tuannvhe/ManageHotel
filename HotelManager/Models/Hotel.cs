using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            Rooms = new HashSet<Room>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string? HotelName { get; set; }
        public int? PId { get; set; }

        public virtual Province? PIdNavigation { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
