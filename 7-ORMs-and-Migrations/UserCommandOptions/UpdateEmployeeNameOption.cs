using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class UpdateEmployeeNameOption : BaseUserCommand, IUserCommandOption
    {
        public void Run()
        {
            var employee = UserSelectEmployee();
            if (employee != null)
            {
                string newName = GetName();
                employee.EmployeeFirstName = newName.Split()[0];
                employee.EmployeeLastName = newName.Split()[1];
                employee.Save();
            }
        }

        private string GetName()
        {
            var name = string.Empty;
            while (name.Split(' ').Count() != 2)
            {
                Console.WriteLine("Name must consist of a first and last name only");
                Console.Write("   New name: ");
                name = Console.ReadLine();
            }
            return name;
        }
    }
}
