using System;
using System.Collections.Generic;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    public class Park_DAL_Test : CapstoneDBTests
    {
        [TestMethod]
        public void GetParks_Test()
        {
            // Arrange
            Park_DAL testDAL = new Park_DAL();

            // Act
            IList<Park> testResults = testDAL.GetParks();

            int parksCount = testResults.Count;

            // Assert
            Assert.AreEqual(3, parksCount);
        }
    }
}
