using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class CampgroundCLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=CampgroundDB;Integrated Security=True";

        public void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");
            Console.WriteLine("Select a Park for Further Details:");
            Console.WriteLine();
            // Console.WriteLine($"{i + 1}  {parksList[i]} ");
            Console.WriteLine("Q) quit");
        }



        public void DisplayCampgrounds()
        {

        }

        private void PrintParkMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("1)  View Campgrounds");
            Console.WriteLine("2)  Search for Reservation");
            Console.WriteLine("3)  Return to Previous Screen");
        }

        private void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation system!");
        }
    }


}
