using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderRooms = new HashSet<OrderRoom>();
            OrderServices = new HashSet<OrderService>();
        }

        public int Id { get; set; }
        public decimal? Total { get; set; }
        public DateTime? DateBook { get; set; }
        public DateTime? DateCheckout { get; set; }
        public string? Status { get; set; }
        public int? UId { get; set; }

        public virtual User? UIdNavigation { get; set; }
        public virtual ICollection<OrderRoom> OrderRooms { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
    }
}
