using System;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundSqlDAL_Tests : CapstoneDBTests
    {
        [TestMethod]
        public void GetCampgrounds_Test()
        {
            Campground_DAL testCampgroundDAL = new Campground_DAL();

            //  Stopping point:  pass the ParkID in as an argument
            IList<Campground> testResults = testCampgroundDAL.GetCampgroundsByPark();
        }
    }
}
