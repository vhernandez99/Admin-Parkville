using System;
using System.Collections.Generic;
using System.Text;

namespace Admin_Parkville.Models
{
    public class ReservationDetail
    {
        public int Id { get; set; }
        public DateTime ReservationTime { get; set; }
        public string CustomerName { get; set; }
        public string MovieName { get; set; }
        public string Email { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public DateTime PlayingDate { get; set; }
        public DateTime PlayingTime { get; set; }
        public string Phone { get; set; }
        public string IsPaid { get; set; }
        public string IsUsed { get; set; }

    }
}
