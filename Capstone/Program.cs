﻿using System;
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
    }
}
