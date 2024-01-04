using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Room
    {
        public Room()
        {
            OrderRooms = new HashSet<OrderRoom>();
        }

        public int Id { get; set; }
        public string? RoomName { get;  set; }
        public string? Status { get; set; }
        public int HId { get; set; }
        public decimal? Price { get; set; }
        public string? RoomType { get; set; }

        public virtual Hotel HIdNavigation { get; set; } = null!;
        public virtual ICollection<OrderRoom> OrderRooms { get; set; }
    }
}
