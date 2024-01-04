using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class OrderRoom
    {
        public int Id { get; set; }
        public int? OId { get; set; }
        public int? RId { get; set; }

        public virtual Order? OIdNavigation { get; set; }
        public virtual Room? RIdNavigation { get; set; }
    }
}
