using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public int SiteID { get; set; }

        public string Name { get; set; }

        public DateTime FirstDateBooked { get; set; }

        public DateTime LastDateBooked { get; set; }

        public DateTime DateTimeReservationPlaced { get; set; }

    }
}
