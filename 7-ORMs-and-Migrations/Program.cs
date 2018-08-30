﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataBoard = new DataBoard();
            var employees = Models.Employees.GetAll().ToList();
            dataBoard.DisplayData(employees);
            Console.ReadLine();
        }
        
    }
}
