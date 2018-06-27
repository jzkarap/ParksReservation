using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;

        private Dictionary<int, Park> availableParks = new Dictionary<int, Park>();

        // Single parameter constructor
        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all parks
        /// </summary>
        /// <returns></returns>
        public IDictionary<int, Park> GetParks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int parkSelection = 1;

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.Park_Id = Convert.ToInt32(reader["park_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                        p.Area = Convert.ToInt32(reader["area"]);
                        p.Visitors = Convert.ToInt32(reader["visitors"]);
                        p.Description = Convert.ToString(reader["description"]);

                        availableParks.Add(parkSelection, p);

                        parkSelection = parkSelection + 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return availableParks;
        }

        public void GetParkToDisplay(int parkToDisplay)
        {

            if (availableParks.ContainsKey(parkToDisplay))
            {
                Park parkSelected = availableParks[parkToDisplay];

                Console.Clear();
                Console.WriteLine("Park Information Screen");
                Console.WriteLine($"{parkSelected.Name}");
                Console.WriteLine("Location:".PadRight(20) + $"{parkSelected.Location}");
                Console.WriteLine("Established:".PadRight(20) + $"{parkSelected.EstablishDate.ToString("MM/dd/yyyy")}");
                Console.WriteLine("Area:".PadRight(20) + $"{parkSelected.Area} sq km");
                Console.WriteLine("Annual Visitors:".PadRight(20) + $"{parkSelected.Visitors}");
                Console.WriteLine();
                Console.WriteLine($"{parkSelected.Description}");
            }
            else
            {
                Console.WriteLine("**** NO RESULTS ****");
            }
        }
    }
}
