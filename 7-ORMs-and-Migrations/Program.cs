using System;
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
            var employees = Models.Employees.GetAll();
            var employee = Models.Employees.GetSingle(employees.First().ID);
            Console.WriteLine(employee.FirstName);
            Console.ReadLine();
        }
    }
}
