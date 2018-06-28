using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class Park_DAL : Master_DAL
    {
        private const string SQL_GetParks = "SELECT * FROM PARK;";

        /// <summary>
        /// Gets a list of parks
        /// </summary>
        /// <returns>A list of parks!</returns>
        public IList<Park> GetParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParks, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.AnnualVisitorCount = Convert.ToInt32(reader["visitors"]);
                        p.AreaInKmSquared = Convert.ToInt32(reader["area"]);
                        p.DateEstablished = Convert.ToDateTime(reader["establish_date"]);
                        p.Description = Convert.ToString(reader["description"]);
                        p.Location = Convert.ToString(reader["location"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.ParkID = Convert.ToInt32(reader["park_id"]);

                        parks.Add(p);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Looks like there is a problem reading or parsing your database!");
            }

            return parks;
        }
    }
}
