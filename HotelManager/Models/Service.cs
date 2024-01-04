using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Service
    {
        public Service()
        {
            OrderServices = new HashSet<OrderService>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public decimal? Price { get; set; }
        public string? Type { get; set; }
        public string? Image { get; set; }
        public int? HId { get; set; }

        public virtual Hotel? HIdNavigation { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
    }
}
