using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class CampgroundCLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

        /// <summary>
        /// Displays a list of parks available for selection
        /// </summary>
        public void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");
            ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnection);

            // Gets dictionary parksAvailable from parkDAL
            IDictionary<int, Park> parksAvailable = parkDAL.GetParks();

            int parkToDisplay = 0;

            List<int> validOptions = new List<int>();

            while (!validOptions.Contains(parkToDisplay))
            {
                try
                {
                    Console.WriteLine("Select a Park for Further Details:");

                    // Displays parks contained within parksAvailable
                    foreach (var kvp in parksAvailable)
                    {
                        validOptions.Add(kvp.Key);
                        int selection = kvp.Key;
                        Park park = kvp.Value;
                        Console.WriteLine($"{selection}) {park.Name}");
                    }

                    Console.WriteLine("Q) Quit");
                    Console.WriteLine();

                    parkToDisplay = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Please select a valid park!");
                    Console.WriteLine();
                }
            }




            parkDAL.GetParkToDisplay(parkToDisplay);
        }



        public void DisplayCampgrounds()
        {

        }

        /// <summary>
        /// Display all Parks to the user
        /// </summary>
        //public void DisplayParkInfo()
        //{
        //    Console.WriteLine("Park Information Screen");

        //    int parkToDisplay = int.Parse(Console.ReadLine());

        //    if (parksAvailable.ContainsKey(parkToDisplay))
        //    {
        //        Park parkSelected = parksAvailable[parkToDisplay].Value.Remove();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Please select a valid park.");
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine(parksList[i].Description);
        //    Console.WriteLine();
        //}

        private void PrintParkMenu()
        {
            Console.WriteLine("Select a Command");
            Console.WriteLine("1)  View Campgrounds");
            Console.WriteLine("2)  Search for Reservation");
            Console.WriteLine("3)  Return to Previous Screen");
        }

        public void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation system!");
            Console.WriteLine();
        }
    }


}
