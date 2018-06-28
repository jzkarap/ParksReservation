using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class Campsite_DAL : Master_DAL
    {
        private const string SQL_GetUnbookedCampsites = "SELECT TOP 5 site.site_id, site_number, max_occupancy, max_rv_length, utilities, campground_id, accessible FROM site INNER JOIN reservation ON reservation.site_id = site.site_id WHERE site.campground_id = 3 AND reservation.from_date > '2018-06-24' AND reservation.from_date > '2018-06-26' GROUP BY site.site_id, site.site_number, site.campground_id, site.max_occupancy, site.accessible, site.max_rv_length, site.utilities;";

        // GetCampsitesByPark
        // 5 results per campground

        // GetCampsitesByRequirement

        // ADD OFF-SEASON FILTER
        /// <summary>
        /// Gets a list of unbooked campsites
        /// </summary>
        /// <param name="campgroundID">Campground from whence to pull sites</param>
        /// <param name="startDate">Desired day to begin reservation</param>
        /// <param name="endDate">Desired day to end reservation</param>
        /// <returns></returns>
        public List<Campsite> GetCampsitesByCampground(int campgroundID, DateTime startDate, DateTime endDate)
        {
            List<Campsite> sites = new List<Campsite>();

            try
            {
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();

                    var cmd = new SqlCommand(SQL_GetUnbookedCampsites, conn);

                    cmd.Parameters.AddWithValue("@campground_id", campgroundID);
                    cmd.Parameters.AddWithValue("@from_date", startDate);
                    cmd.Parameters.AddWithValue("@to_date", endDate);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campsite s = new Campsite();
                        s.Accessible = Convert.ToBoolean(reader["accessible"]);
                        s.CampgroundID = Convert.ToInt32(reader["campground_id"]);
                        s.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                        s.MaxRVLength = Convert.ToInt32(reader["max_rv_length"]);
                        s.SiteID = Convert.ToInt32(reader["site_id"]);
                        s.SiteNumber = Convert.ToInt32(reader["site_number"]);
                        s.UtilityAccess = Convert.ToBoolean(reader["utilities"]);

                        sites.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Sorry, an error occurred: " + ex.Message);
                throw;
            }

            return sites;
        }
    }
}
