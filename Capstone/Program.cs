using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.RunCLI();
        }

        /// <summary>
        /// Converts boolean response to Yes/No
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static string boolChecker(bool value)
        {
            string boolRepresentation = "";

            if (value == true)
            {
                boolRepresentation = "Yes";
            }
            if (value == false)
            {
                boolRepresentation = "No";
            }

            return boolRepresentation;
        }

        //FOR CLI
        /// <summary>
        /// Checks if RVs are allowed,
        /// Gives "N/A" if not
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        static string RVChecker(int maxLength)
        {
            string RVInfo = "";

            if (maxLength == 0)
            {
                RVInfo = "N/A";
            }
            else
            {
                RVInfo = maxLength.ToString();
            }

            return RVInfo;
        }
    }
}
