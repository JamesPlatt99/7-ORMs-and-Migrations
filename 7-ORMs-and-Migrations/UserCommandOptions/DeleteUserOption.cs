using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class DeleteUserOption: BaseUserCommand, IUserCommandOption
    {
        public void Run()
        {
            Console.WriteLine();
            var employee = UserSelectEmployee();
            if (employee != null)
            {
                DeleteUser(employee);
            }
        }

        private void DeleteUser(Models.Employees employee)
        {
            if (employee != null)
            {
                Console.Write(String.Format("Are you sure you want to delete the user {0} {1}? (y/n)", employee.EmployeeFirstName, employee.EmployeeLastName));
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    employee.Delete();
                }
            }
        }
    }
}
