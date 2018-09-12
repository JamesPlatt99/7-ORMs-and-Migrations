using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations
{
    class UserCommands
    {
        #region "Enumeration"
        private enum UserOptions
        {
            ShowData = 1,
            DeleteUser = 2,
            IncreasePensionFunds = 3,
            UpdateName = 4,
            Exit = 9
        }
        #endregion

        #region "Properties"
        public static List<Models.IDataboardObject> RowData { get; set; }
        #endregion

        #region "Public Methods"
        public void Run()
        {
            int userInput = 0;
            while(userInput != 9)
            {
                DisplayOptions();
                userInput = GetUserInput();
                DoWhatTheUserSaid(userInput);
            }

        }
        #endregion

        #region "Private Methods"
        private void DoWhatTheUserSaid(int userInput)
        {
            UserCommandOptions.IUserCommandOption userCommandOption = null;
            switch(userInput)
            {
                case (int) UserOptions.ShowData:
                    userCommandOption = new UserCommandOptions.DisplayDataboardOption();
                    break;
                case (int) UserOptions.DeleteUser:
                    DeleteUserOption();
                    break;
                case (int) UserOptions.IncreasePensionFunds:
                    IncreasePensionFundsOption();
                    break;
                case (int) UserOptions.UpdateName:
                    UpdateNameOption();
                    break;
            }
            userCommandOption?.Run();
        }
        private int GetUserInput()
        {
            int userInput = -2;
            while(userInput == -2)
            {
                Console.Write(" :");
                int.TryParse(Console.ReadLine(), out userInput);
            }
            return userInput;
        }

        private void DisplayOptions()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("----------------");
            Enum.GetValues(typeof(UserOptions)).Cast<UserOptions>().ToList().ForEach(n => Console.WriteLine(String.Format(" {0} - {1}", (int) n, n)));
        }

        private void IncreasePensionFundsOption()
        {
            Models.PensionFund.IncreasePensionFunds();
            DisplayData();
        }

        private void DisplayData()
        {
            var displayDataOption = new UserCommandOptions.DisplayDataboardOption();
            displayDataOption.Run();
        }

        private void DeleteUserOption()
        {
            Console.WriteLine();
            var employee = UserSelectEmployee();
            if(employee != null)
            {
                DeleteUser(employee);
            }
        }

        private void UpdateNameOption()
        {
            var employee = UserSelectEmployee();
            if(employee != null)
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

        private void DeleteUser(Models.Employees employee)
        { 
            if(employee != null)
            {
                Console.Write(String.Format("Are you sure you want to delete the user {0} {1}? (y/n)", employee.EmployeeFirstName, employee.EmployeeLastName));
                if(Console.ReadLine().ToUpper() == "Y")
                {
                    employee.Delete();
                }
            }
        }

        private Models.Employees UserSelectEmployee()
        {
            DisplayData();
            Console.WriteLine("Please enter the index of the user you would like to delete or enter -1 to cancel.");
            Console.WriteLine();
            int userInput = GetUserInput();
            if(userInput > 0)
            {
                return GetEmployeeAtIndex(userInput);
            }
            return null;
        } 

        private Models.Employees GetEmployeeAtIndex(int i)
        {
            if(RowData != null && i < RowData.Count)
            {
                return RowData[i].GetEmployee();
            }
            return null;
        }
        #endregion
    }
}
