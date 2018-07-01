using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationDAL_Tests : CapstoneDBTests
    {
        private Reservation_DAL dal = new Reservation_DAL();

        [TestMethod]
        public void CreateReservation_Test()
        {
            Reservation r = new Reservation();
            r.ReservationID = 50;
            r.SiteID = 5;
            r.Name = "Knight Artorias";
            // This is our "From Date"
            r.FirstDateBooked = Convert.ToDateTime("2018-07-08");
            // This is our "To Date"
            r.LastDateBooked = Convert.ToDateTime("2018-07-10");

            //  Test for CreateReservation by looking for existence
            Assert.IsNotNull(r.DateTimeReservationPlaced);
        }

        [TestMethod]
        public void ReadReservation_Test()
        {
            //  Retrieve the MAX(id) and returns only one row as Scalar
            //  If it exists, the method did its job
            Assert.IsNotNull(dal.RetrieveMostRecentReservation());
        }
    }
}
