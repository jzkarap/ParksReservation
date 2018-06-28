using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class Campground_DAL : Master_DAL
    {
        private const string SQL_GetCampgroundsByPark = "SELECT * FROM campground WHERE park_id = @park_id;";

        /// <summary>
        /// Takes a park, gets a list of campgrounds
        /// </summary>
        /// <param name="parkID">The ID of a park object</param>
        /// <returns>A list of campgrounds associated with that park</returns>
        public IList<Campground> GetCampgroundsByPark(int parkID)
        {
            List<Campground> campgrounds = new List<Campground>();

            try
            {
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();

                    var cmd = new SqlCommand(SQL_GetCampgroundsByPark, conn);

                    cmd.Parameters.AddWithValue("@park_id", parkID);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground c = new Campground();
                        c.CampID = Convert.ToInt32(reader["campground_id"]);
                        c.DailyFee = Convert.ToDouble(reader["daily_fee"]);
                        c.FirstMonthOpen = Convert.ToInt32(reader["open_from_mm"]);
                        c.LastMonthOpen = Convert.ToInt32(reader["open_to_mm"]);
                        c.Name = Convert.ToString(reader["name"]);
                        c.ParkID = Convert.ToInt32(reader["park_id"]);

                        campgrounds.Add(c);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, an error occurred: " + ex.Message);
            }

            return campgrounds;
        }
    }
}
