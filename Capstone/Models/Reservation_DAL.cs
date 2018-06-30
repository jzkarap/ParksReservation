using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Reservation_DAL : Master_DAL
    {
        private const string SQL_CreateReservation = "INSERT INTO (site_id, name, from_date, to_date, create_date)  " +
            "VALUES(@site_id, @name, @from_date, @to_date, @create_date);";

        private const string SQL_RetrieveMostRecentReservation = "SELECT MAX(reservation_id) FROM reservation;";

        // Create reservation
        public void CreateReservation(int siteID, string name, DateTime? arrivalDate, DateTime? departureDate)
        {
            try
            {
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();

                    var sqlCreateReservation = new SqlCommand(SQL_CreateReservation);

                    sqlCreateReservation.Parameters.AddWithValue("@site_id", siteID);
                    sqlCreateReservation.Parameters.AddWithValue("@name", name);
                    sqlCreateReservation.Parameters.AddWithValue("@from_date", arrivalDate);
                    sqlCreateReservation.Parameters.AddWithValue("@to_date", departureDate);
                    sqlCreateReservation.Parameters.AddWithValue("@create_date", DateTime.Now);
                }
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Sorry, an error occurred: " + ex.Message);
                throw;
            }
        }

        //  Get the most recent reservation by ID
        public int RetrieveMostRecentReservation()
        {
            int newestReservation = 0;

            try
            {
                using (var conn = new SqlConnection(dbConnectionString))
                {
                    conn.Open();
                }

                var cmd = new SqlCommand(SQL_RetrieveMostRecentReservation);

                var reader = cmd.ExecuteReader();

                // don't create a new object, just read the highest value
                while (reader.Read())
                {
                    newestReservation = Convert.ToInt32(reader);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Something went wrong");
            }

            return newestReservation;
        }


        // BONUS:  Get reservations within next 30 days
        // Returns a list of Reservations
    }
}
