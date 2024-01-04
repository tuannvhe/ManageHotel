using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Recharge
    {
        public int Id { get; set; }
        public string? BankName { get; set; }
        public int? BankId { get; set; }
        public int? Phone { get; set; }
        public int? DepositCode { get; set; }
        public decimal? RechargeValue { get; set; }
        public int? UId { get; set; }

        public virtual User? UIdNavigation { get; set; }
    }
}
