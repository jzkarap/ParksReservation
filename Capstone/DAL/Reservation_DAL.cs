using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
	public class Reservation_DAL : Master_DAL
	{
		private const string SQL_CreateReservation = "INSERT INTO reservation (site_id, name, from_date, to_date, create_date)  " +
			"VALUES(@site_id, @name, @from_date, @to_date, @create_date);";

		private const string SQL_RetrieveMostRecentReservation = "SELECT MAX(reservation_id) FROM reservation;";

		// Create reservation
		public void CreateReservation(int siteID, string name, DateTime? arrivalDate, DateTime? departureDate)
		{
			try
			{
				using (var conn = new SqlConnection(dbConnectionString))
				{
					using (SqlCommand cmd = new SqlCommand(SQL_CreateReservation))
					{
						cmd.Parameters.AddWithValue("@site_id", siteID);
						cmd.Parameters.AddWithValue("@name", name);
						cmd.Parameters.Add("@from_date", SqlDbType.Date).Value = arrivalDate;
						cmd.Parameters.Add("@to_date", SqlDbType.Date).Value = departureDate;
						cmd.Parameters.AddWithValue("@create_date", DateTime.Now);

						conn.Open();

						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException)
			{
				Console.WriteLine("Sorry, your reservation could not be created.");
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

					SqlCommand cmd = new SqlCommand(SQL_RetrieveMostRecentReservation, conn);

					SqlDataReader reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						newestReservation = Convert.ToInt32(reader["(No column name)"]);
					}
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Your reservation could not be retrieved.");
			}

			return newestReservation;
		}


		// BONUS:  Get reservations within next 30 days
		// Returns a list of Reservations
	}
}
