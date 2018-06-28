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

        int parkToDisplay = 0;

        // Gets list parksAvailable from parkDAL
        static Park_DAL parkDAL = new Park_DAL();
        IList<Park> parksAvailable = parkDAL.GetParks();

        public void RunCLI()
        {
            PrintHeader();
            Thread.Sleep(800);

            DisplayAvailableParks();
        }

        private void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation system!");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a list of parks available for selection
        /// </summary>
        private void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");

            string userChoice = "";

            List<int> validOptions = new List<int>();

            while (!validOptions.Contains(parkToDisplay))
            {
                try
                {
                    PrintParkMenu(validOptions);

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
                        // Gets index of park from parksAvailable by subtracting 1 from the user's choice
                        // (to account for 0-based index)
                        parkToDisplay = (int.Parse(userChoice) - 1);

                        Park selectedPark = parksAvailable[parkToDisplay];

                        PresentParkInfo(selectedPark);

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

        private void PrintParkMenu(List<int> validOptions)
        {
            Console.WriteLine("Select a Park for Further Details:");

            // Displays parks contained within parksAvailable
            for (var i = 0; i < parksAvailable.Count; i++)
            {
                int listEntryNumber = i + 1;
                string parkName = parksAvailable[i].Name;

                Console.WriteLine($"   {listEntryNumber}) {parkName}");

                validOptions.Add(listEntryNumber);
            }

            Console.WriteLine("   Q) Quit");
            Console.WriteLine();
        }

        private void PresentParkInfo(Park selectedPark)
        {
            Console.WriteLine($"{selectedPark.Name}");
            Console.WriteLine("Location".PadRight(20) + $"{selectedPark.Location}");
            Console.WriteLine("Established".PadRight(20) + $"{selectedPark.DateEstablished.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Area".PadRight(20) + $"{selectedPark.AreaInKmSquared} sq km");
            Console.WriteLine("Annual Visitors".PadRight(20) + $"{selectedPark.AnnualVisitorCount}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");
        }

        private void DisplayCampgrounds()
        {
            const string getCampgrounds = "1";
            const string searchForReservations = "2";
            const string returnToParks = "3";

            bool getUsOutTheLoop = true;
            CampgroundsCommandMenu();

            while (getUsOutTheLoop == true)
            {
                string command = GetUserInputString();

                switch (command.ToLower())
                {
                    case getCampgrounds:
                        GetCampgroundsByPark();
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

        private void GetCampgroundsByPark()
        {
            int parkForCampgrounds = parksAvailable[parkToDisplay].ParkID;
            string parkName = parksAvailable[parkToDisplay].Name;

            Campground_DAL campDAL = new Campground_DAL();

            IList<Campground> campgrounds = campDAL.GetCampgroundsByPark(parkForCampgrounds);

            Console.Clear();
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(parkName);
            Console.WriteLine();

            PresentCampgroundInfo(campgrounds);

            CampgroundSubMenu();
            CampsiteCommands(GetUserInputString());

            Console.WriteLine();
        }

        private void PresentCampgroundInfo(IList<Campground> campgrounds)
        {
            Console.WriteLine("Name".PadLeft(10).PadRight(40) + "Open".PadRight(10) + "Close".PadRight(15) + "Daily Fee");

            for (int i = 0; i < campgrounds.Count; i++)
            {
                Campground c = campgrounds[i];

                int campID = campgrounds[i].CampID;
                string campground = campgrounds[i].Name;
                string firstMonth = GetMonthFromSQLInt(c.FirstMonthOpen);
                string lastMonth = GetMonthFromSQLInt(c.LastMonthOpen);
                double dailyFee = campgrounds[i].DailyFee;

                // EXPAND INFO
                Console.WriteLine($"#{campID}".PadRight(6) + $"{campground}".PadRight(34) + $"{firstMonth}".PadRight(10) + $"{lastMonth}".PadRight(15) + $"{dailyFee:C2}");

            }
        }

        private void CampgroundSubMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command: ");
            Console.WriteLine("   1) Search for Available Reservation");
            Console.WriteLine("   2) Return to Previous Screen");
        }

        private void CampsiteCommands(string userInput)
        {
            Console.WriteLine();

            while (userInput != "1" ||
                    userInput != "2")
            {
                switch (userInput)
                {
                    case "1":
                       // SearchForReservations();
                        return;

                    case "2":
                        return;
                }

                userInput = GetUserInputString();
            }

        }

        private static void CampgroundsCommandMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("   1) View Campgrounds".PadLeft(4));
            Console.WriteLine("   2) Search for Reservation".PadLeft(4));
            Console.WriteLine("   3) Return to Previous Screen".PadLeft(4));
        }

        private string GetMonthFromSQLInt(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";

                case 2:
                    return "February";

                case 3:
                    return "March";

                case 4:
                    return "April";

                case 5:
                    return "May";

                case 6:
                    return "June";

                case 7:
                    return "July";

                case 8:
                    return "August";

                case 9:
                    return "September";

                case 10:
                    return "October";

                case 11:
                    return "November";

                case 12:
                    return "December";

                default:
                    return "N/A";
            }
        }














        private string GetUserInputString()
        {
            string userInput = Console.ReadLine();
            return userInput;
        }
    }


}
