using System;
using System.Collections.Generic;
using System.Text;

namespace Admin_Parkville.Models
{
    public class UserReservations
    {
        public int Price { get; set; }
        public string IsPaid { get; set; }
        public string MovieName { get; set; }
        public int CustomerId { get; set; }
        public DateTime PlayingDate { get; set; }
        public DateTime PlayingTime { get; set; }
        public string CustomerName { get; set; }
        public int ReservationId { get; set; }
        public DateTime ReservationTime { get; set; }
    }
}
