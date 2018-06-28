using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Campground
    {
        public int CampgID { get; set; }

        public int ParkID { get; set; }

        public string Name { get; set; }

        public int FirstMonthOpen { get; set; }

        public int LastMonthOpen { get; set; }

        public double DailyFee { get; set; }
    }
}
