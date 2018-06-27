using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;

        // Single parameter constructor
        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all parks
        /// </summary>
        /// <returns></returns>


        
        /// <summary>
        /// Display all Parks to the user
        /// </summary>
        public void DisplayParkInformation()
        {
            // TODO : store parksList[i] as park
            Console.WriteLine("Park Information Screen");
            Console.WriteLine(parksList[i].Name);
            Console.WriteLine(parksList[i].Location);
            Console.WriteLine(parksList[i].EstablishedDate);
            Console.WriteLine(parksList[i].Area);
            Console.WriteLine(parksList[i].AnnualVisitorCount);
            Console.WriteLine();
            Console.WriteLine(parksList[i].Description);
            Console.WriteLine();
        }

    }
}
