using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Park
    {
        public int ParkID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime DateEstablished { get; set; }

        public int AreaInKmSquared { get; set; }

        public int AnnualVisitorCount { get; set; }

        public string Description { get; set; }
    }
}
