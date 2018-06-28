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
    public class CLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

        public void RunCLI()
        {
            PrintHeader();
            Thread.Sleep(800);

            DisplayAvailableParks();
        }

        public void PresentParkInfo(Park selectedPark)
        {
            Console.WriteLine($"{selectedPark.Name}");
            Console.WriteLine("Location".PadRight(20) + $"{selectedPark.Location}");
            Console.WriteLine("Established".PadRight(20) + $"{selectedPark.DateEstablished.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Area".PadRight(20) + $"{selectedPark.AreaInKmSquared} sq km");
            Console.WriteLine("Annual Visitors".PadRight(20) + $"{selectedPark.AnnualVisitorCount}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");
        }

        /// <summary>
        /// Displays a list of parks available for selection
        /// </summary>
        public void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");
            Park_DAL parkDAL = new Park_DAL();

            // Gets list parksAvailable from parkDAL
            IList<Park> parksAvailable = parkDAL.GetParks();

            string userChoice = "";
            int parkToDisplay = 0;

            List<int> validOptions = new List<int>();

            while (!validOptions.Contains(parkToDisplay))
            {
                try
                {
                    Console.WriteLine("Select a Park for Further Details:");

                    // Displays parks contained within parksAvailable
                    for (var i = 0; i < parksAvailable.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) {parksAvailable[i].Name}");
                        validOptions.Add(i + 1);
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

                        return;
                    }
                    else
                    {
                        parkToDisplay = (int.Parse(userChoice) + 1);

                        PresentParkInfo(parksAvailable[parkToDisplay]);

                        Console.WriteLine();

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
