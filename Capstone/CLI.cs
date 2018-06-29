using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone
{
    public class CLI
    {
        const string DatabaseConnection = @"Data Source=.\SQLExpress;Initial Catalog=Campground;Integrated Security=True";

        /// <summary>
        /// User selection for park, will be available throughout class
        /// </summary>
        static int parkToDisplay;

        /// <summary>
        /// User selected park, which will be used to find campgrounds
        /// </summary>
        static Park selectedPark;

        // Gets list parksAvailable from parkDAL
        static Park_DAL parkDAL = new Park_DAL();
        static IList<Park> parksAvailable = parkDAL.GetParks();

        Campground_DAL campDAL = new Campground_DAL();

        static IList<Campground> campgrounds;

        static List<Campsite> campsites;

        /// <summary>
        /// Initiates CLI
        /// </summary>
        public void RunCLI()
        {
            PrintHeader();
            Thread.Sleep(800);

            DisplayAvailableParks();
        }

        /// <summary>
        /// Prints program greeting
        /// </summary>
        private void PrintHeader()
        {
            Console.WriteLine("Welcome to the National Parks Reservation system!");
            Console.WriteLine();
        }

        /// <summary>
        /// Generates list of valid park choices, presents park selection screen
        /// </summary>
        private void DisplayAvailableParks()
        {
            Console.WriteLine("View Parks Interface");

            string userChoice = "";

            List<int> validOptions = new List<int>();

            // Sets parkToDisplay to 0 so choice can be reselected by user through menu regression
            parkToDisplay = 0;

            while (!validOptions.Contains(parkToDisplay))
            {
                try
                {
                    PrintParkSelection(validOptions);

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

                        selectedPark = parksAvailable[parkToDisplay];

                        PresentParkInfo(selectedPark);

                        Console.WriteLine();

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

        /// <summary>
        /// Prints each park in numbered park list
        /// </summary>
        /// <param name="validOptions">Accepts only selections within range of possible choices</param>
        private void PrintParkSelection(List<int> validOptions)
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

        /// <summary>
        ///  Generates information about a specific park
        /// </summary>
        /// <param name="selectedPark">The user-selected park</param>
        private void PresentParkInfo(Park selectedPark)
        {
            Console.Clear();
            Console.WriteLine($"{selectedPark.Name}");
            Console.WriteLine("Location".PadRight(20) + $"{selectedPark.Location}");
            Console.WriteLine("Established".PadRight(20) + $"{selectedPark.DateEstablished.ToString("dd/MM/yyyy")}");
            Console.WriteLine("Area".PadRight(20) + $"{selectedPark.AreaInKmSquared} sq km");
            Console.WriteLine("Annual Visitors".PadRight(20) + $"{selectedPark.AnnualVisitorCount}");
            Console.WriteLine();
            Console.WriteLine($"{selectedPark.Description}");

            ParkSubmenuSelection();
        }

        /// <summary>
        /// Gets user selection for options while park info is displayed
        /// </summary>
        private void ParkSubmenuSelection()
        {
            const string getCampgrounds = "1";
            const string searchForReservations = "2";
            const string returnToParks = "3";

            bool getUsOutTheLoop = true;
            CampgroundCommandMenu();

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

        /// <summary>
        /// Creates list of campgrounds within park selected by user
        /// </summary>
        private void GetCampgroundsByPark()
        {
            int parkForCampgrounds = parksAvailable[parkToDisplay].ParkID;
            string parkName = parksAvailable[parkToDisplay].Name;

            campgrounds = campDAL.GetCampgroundsByPark(parkForCampgrounds);

            Console.Clear();
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine(parkName);
            Console.WriteLine();

            PresentCampgroundInfo(campgrounds);

            CampgroundSubMenu();
            CampsiteCommands(GetUserInputString());

            Console.WriteLine();
        }

        /// <summary>
        /// Goes through list of campgrounds, printing relevant info to console
        /// </summary>
        /// <param name="campgrounds"></param>
        private void PresentCampgroundInfo(IList<Campground> campgrounds)
        {
            Console.WriteLine("Name".PadLeft(10).PadRight(40) + "Open".PadRight(10) + "Close".PadRight(15) + "Daily Fee");

            for (int i = 0; i < campgrounds.Count; i++)
            {
                Campground c = campgrounds[i];

                int listNumber = i + 1;
                string campground = campgrounds[i].Name;
                string firstMonth = GetMonthFromSQLInt(c.FirstMonthOpen);
                string lastMonth = GetMonthFromSQLInt(c.LastMonthOpen);
                double dailyFee = campgrounds[i].DailyFee;

                Console.WriteLine($"#{listNumber})".PadRight(6) + $"{campground}".PadRight(34) + $"{firstMonth}".PadRight(10) + $"{lastMonth}".PadRight(15) + $"{dailyFee:C2}");

            }
        }

        /// <summary>
        /// Prints campground options to screen
        /// </summary>
        private static void CampgroundCommandMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("   1) View Campgrounds".PadLeft(4));
            Console.WriteLine("   2) Search for Reservation".PadLeft(4));
            Console.WriteLine("   3) Return to Previous Screen".PadLeft(4));
        }

        /// <summary>
        /// Prints submenu when campgrounds are displayed
        /// </summary>
        private void CampgroundSubMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select a Command: ");
            Console.WriteLine("   1) Search for Available Reservation");
            Console.WriteLine("   2) Return to Previous Screen");
        }

        /// <summary>
        /// Gets user selection for campsite options
        /// </summary>
        /// <param name="userInput">user input 1 or 2</param>
        private void CampsiteCommands(string userInput)
        {
            Console.WriteLine();

            try
            {
                while (userInput != "1" ||
                    userInput != "2")
                {
                    switch (userInput)
                    {
                        case "1":
                            GetSitesAvailableForReservation();
                            return;

                        case "2":
                            PresentParkInfo(selectedPark);
                            return;
                    }

                    Console.WriteLine("Please select a valid option!");
                    Thread.Sleep(1000);

                    // these two methods clean up the space
                    ClearCurrentConsoleLine();
                    ClearCurrentConsoleLine();

                    // re-assign the user input to correctly break out
                    userInput = Console.ReadLine();
                }


            }
            catch (Exception)
            {
                Console.WriteLine("Please select a valid option!");
                Thread.Sleep(1000);
                ClearCurrentConsoleLine();
                userInput = GetUserInputString();
            }
        }

        private void GetSitesAvailableForReservation()
        {
            int userSelection = 1;
            string temporaryArrivalDateString;
            string temporaryDepartureDateString;
            DateTime? arrivalDate = null;
            DateTime? departureDate = null;

            Exception BrokenDateRange = new Exception();

            while (arrivalDate == null ||
                   departureDate == null)
            {
                try
                {
                    userSelection = GetCampgroundSelection();

                    {
                        if (userSelection == 0)
                        {
                            Cancel();
                            break;
                        }
                    }

                    // both while loops validate the user's input
                    while (arrivalDate == null)
                    {
                        Console.Write("What is the arrival date? (mm/dd/yyyy) ");
                        temporaryArrivalDateString = Console.ReadLine();

                        if (temporaryArrivalDateString == "0")
                        {
                            ClearCurrentConsoleLine();
                            Cancel();
                            break;
                        }

                        arrivalDate = DateTimeTranslation(temporaryArrivalDateString);
                    }

                    while (departureDate == null)
                    {
                        try
                        {
                            Console.Write("What is the departure date? (mm/dd/yyyy) ");
                            temporaryDepartureDateString = Console.ReadLine();

                            if (temporaryDepartureDateString == "0")
                            {
                                ClearCurrentConsoleLine();
                                ClearCurrentConsoleLine();
                                Cancel();
                                break;
                            }

                            departureDate = DateTimeTranslation(temporaryDepartureDateString);


                            if (departureDate < arrivalDate)
                            {
                                departureDate = null;
                                throw BrokenDateRange;
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Please enter a valid date range.");
                            Thread.Sleep(1000);
                            ClearCurrentConsoleLine();
                            ClearCurrentConsoleLine();
                        }

                    }

                    SearchForAvailableCampsites(campgrounds[userSelection - 1], arrivalDate, departureDate);
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a valid input.");
                    Thread.Sleep(1000);
                    ClearCurrentConsoleLine();
                    ClearCurrentConsoleLine();
                }

            }

        }

        /// <summary>
        /// Collects the site to reserve
        /// </summary>
        private int GetCampsiteToReserve()
        {
            Console.WriteLine();
            Console.Write("Which site should be reserved (enter 0 to cancel)? ");

            int? desiredCampsite = null;
            string temporaryDesiredCampsite = "";
            Campsite chosenSite = null;

            //  custom exception to maintain loop and inform user
            Exception invalidInput = new Exception();

            while (desiredCampsite == null)
            {
                for (int i = 0; i < campsites.Count; i++)
                {
                    try
                    {
                        temporaryDesiredCampsite = Console.ReadLine();

                        //  if we don't have a match,
                        if (!Regex.IsMatch(temporaryDesiredCampsite, @"^[0-9]+$"))
                        {
                            // throw and catch
                            throw invalidInput;
                        }

                        desiredCampsite = int.Parse(temporaryDesiredCampsite);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid site selected. Please select a valid site.");
                        Thread.Sleep(1000);
                        ClearCurrentConsoleLine();
                        ClearCurrentConsoleLine();
                        Console.Write("Which site should be reserved (enter 0 to cancel)? ");
                    }

                    //  when the desiredCampsite matches an ID on the list
                    if (desiredCampsite == campsites[i].SiteID)
                    {
                        chosenSite = campsites[i];
                        break;
                    }
                }
            }
            //  we need this campsite's ID to make a reservation
            return chosenSite.SiteID;
        }

        private string GetNameForReservation()
        {
            Console.Write("What name should the reservation be made under? ");

            string name = Console.ReadLine();
            var regex = Regex.IsMatch(name, @"^[a-zA-Z]+$");

            while (!regex)
            {
                Console.WriteLine("Invalid input.  Please enter a valid name.");
                Thread.Sleep(1000);
                ClearCurrentConsoleLine();
                name = Console.ReadLine();
            }

            return name;
        }

        private int GetCampgroundSelection()
        {
            List<int> validOptions = new List<int>();

            for (int i = 0; i < campgrounds.Count; i++)
            {
                validOptions.Add(i + 1);
            }

            int userSelection = 0;

            Console.Write("Which campground (enter 0 to cancel)? ");
            userSelection = Convert.ToInt32(Console.ReadLine());

            if (userSelection == 0)
            {
                return userSelection;
            }
            else
            {
                while (!validOptions.Contains(userSelection))
                {
                    Console.WriteLine("Please select a valid campground.");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    ClearCurrentConsoleLine();
                    ClearCurrentConsoleLine();
                    ClearCurrentConsoleLine();
                    Console.Write("Which campground (enter 0 to cancel)? ");

                    userSelection = Convert.ToInt32(Console.ReadLine());
                }
            }

            return userSelection;
        }

        private void SearchForAvailableCampsites(Campground selectedCampground, DateTime? startDate, DateTime? endDate)
        {
            Campsite_DAL site_DAL = new Campsite_DAL();

            campsites = site_DAL.GetCampsitesByCampground(selectedCampground.CampID, startDate, endDate);

            Console.WriteLine();
            Console.WriteLine("Results Matching Your Search Criteria");
            Console.WriteLine("Site No.".PadRight(15) + "Max Occup.".PadRight(15) + "Accessible?".PadRight(15) + "Max RV Length".PadRight(20) + "Utility".PadRight(15) + "Cost");

            foreach (var site in campsites)
            {
                Console.WriteLine($"{site.SiteNumber}".PadRight(15) + $"{site.MaxOccupancy}".PadRight(15) + $"{BoolChecker(site.Accessible)}".PadRight(15) + $"{RVChecker(site.MaxRVLength)}".PadRight(20) + $"{BoolChecker(site.UtilityAccess)}".PadRight(15) + $"{selectedCampground.DailyFee:c}");
            }

            BookReservation(startDate, endDate);
        }


        private void BookReservation(DateTime? arrivalDate, DateTime? departureDate)
        {
            int campSite = GetCampsiteToReserve();
            string name = GetNameForReservation();

            Reservation_DAL reservationDAL = new Reservation_DAL();

            reservationDAL.CreateReservation(campSite, name, arrivalDate, departureDate);
        }



        private void Cancel()
        {
            // Keep the cursor at a consistent height
            ClearCurrentConsoleLine();
            ClearCurrentConsoleLine();
            CampsiteCommands(GetUserInputString());
        }

        private DateTime? DateTimeTranslation(string dateToTranslate)
        {
            DateTime date = new DateTime();

            if (DateTime.TryParse(dateToTranslate, out date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("This date is in an incorrect format. Please try again.");
                Console.WriteLine();
                Thread.Sleep(1000);
                ClearCurrentConsoleLine();
                Console.SetCursorPosition(0, Console.CursorTop);
                ClearCurrentConsoleLine();
                ClearCurrentConsoleLine();
                return null;
            }

        }

        /// <summary>
        /// Translates SQL data to month as string
        /// </summary>
        /// <param name="month">integer value from SQL, representing month</param>
        /// <returns>month as string</returns>
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

        /// <summary>
        /// Gets user input
        /// </summary>
        /// <returns>String representing user input</returns>
        private string GetUserInputString()
        {
            string userInput = Console.ReadLine();
            ClearCurrentConsoleLine();
            return userInput;

        }

        // Sets cursor to beginning of line and rewrites line
        // This allows input field to be reset without bumping down to new line
        private void ClearCurrentConsoleLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        /// <summary>
        /// Converts boolean response to Yes/No
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string BoolChecker(bool value)
        {
            string boolRepresentation = "";

            if (value == true)
            {
                boolRepresentation = "Yes";
            }
            if (value == false)
            {
                boolRepresentation = "No";
            }

            return boolRepresentation;
        }

        /// <summary>
        /// Checks if RVs are allowed,
        /// Gives "N/A" if not
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        static string RVChecker(int maxLength)
        {
            string RVInfo = "";

            if (maxLength == 0)
            {
                RVInfo = "N/A";
            }
            else
            {
                RVInfo = maxLength.ToString();
            }

            return RVInfo;
        }
    }
}
