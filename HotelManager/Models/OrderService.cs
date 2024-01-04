using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class OrderService
    {
        public int Id { get; set; }
        public int? OId { get; set; }
        public int? SId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order? OIdNavigation { get; set; }
        public virtual Service? Service { get; set; }
        //public virtual Album Album { get; set; } = null!;
    }
}
