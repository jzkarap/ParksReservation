using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone
{
    public class CampgroundCLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

        public void RunCLI()
        {
            PrintHeader();
            Thread.Sleep(800);

            DisplayAvailableParks();
        }

        /// <summary>
        /// Displays a list of parks available for selection
        /// </summary>
        public void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");
            ParkSqlDAL parkDAL = new ParkSqlDAL(DatabaseConnection);

            // Gets dictionary parksAvailable from parkDAL
            IDictionary<int, Park> parksAvailable = parkDAL.GetParks();

            string userChoice;
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

                    userChoice = Console.ReadLine().ToUpper();

                    Console.Clear();

                    if (userChoice == "Q")
                    {
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        Console.WriteLine();
                        break;
                    }
                    else
                    {
                        parkToDisplay = int.Parse(userChoice);

                        parkDAL.GetParkToDisplay(parkToDisplay);

                        DisplayCampgrounds();

                        return;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Please select a valid park!");
                    Console.WriteLine();
                }
            }
        }

        public void DisplayCampgrounds()
        {
            const string getCampgrounds = "1";
            const string searchForReservations = "2";
            const string returnToParks = "3";

            bool getUsOutTheLoop = true;

            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1) View Campgrounds".PadLeft(4));
            Console.WriteLine("2) Search for Reservation".PadLeft(4));
            Console.WriteLine("3) Return to Previous Screen".PadLeft(4));

            while (getUsOutTheLoop == true)
            {
                string command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case getCampgrounds:
                        //campgroundDAL.GetCampgrounds();
                        getUsOutTheLoop = false;
                        break;

                    case searchForReservations:
                        //reservationDAL.GetAvailableReservations();
                        getUsOutTheLoop = false;
                        break;

                    case returnToParks:
                        Console.Clear();
                        DisplayAvailableParks();
                        getUsOutTheLoop = false;
                        break;
                }

            }
        }

        private void PrintParkMenu()
        {

        }

        public void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation system!");
            Console.WriteLine();
        }
    }


}
