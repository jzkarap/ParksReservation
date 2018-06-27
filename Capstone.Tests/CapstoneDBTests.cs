using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.IO;
using System.Data.SqlClient;

namespace Capstone.Tests
{
    /// <summary>
    /// Summary description for CapstoneDBTests
    /// </summary>
    [TestClass]
    public class CapstoneDBTests
    {
        public const string ConnectionString = @"Data Source=.\SQLExpress;Initial Catalog = CampgroundDB; Integrated Security = True";

        TransactionScope transaction;

        [TestInitialize]
        public void SetupData()
        {
            transaction = new TransactionScope();

            // Read sql from text file
            string sql = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, ""));

            // Run sql
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void CleanUpData()
        {
            transaction.Dispose();
        }
    }
}
