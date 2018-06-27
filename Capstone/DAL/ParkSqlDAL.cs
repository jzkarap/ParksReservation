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
            Dictionary<int, Park> availableParks = new Dictionary<int, Park>();

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

                       parkSelection = parkSelection++;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return availableParks;
        }


        
        /// <summary>
        /// Display all Parks to the user
        /// </summary>
        //public void DisplayParkInformation()
        //{
        //    // TODO : store parksList[i] as park
        //    Console.WriteLine("Park Information Screen");
        //    Console.WriteLine(parksList[i].Name);
        //    Console.WriteLine(parksList[i].Location);
        //    Console.WriteLine(parksList[i].EstablishedDate);
        //    Console.WriteLine(parksList[i].Area);
        //    Console.WriteLine(parksList[i].AnnualVisitorCount);
        //    Console.WriteLine();
        //    Console.WriteLine(parksList[i].Description);
        //    Console.WriteLine();
        //}

    }
}
