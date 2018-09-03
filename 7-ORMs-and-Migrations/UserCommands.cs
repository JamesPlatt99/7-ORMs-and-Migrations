﻿using System;
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
            Exit = 9
        }
        #endregion

        #region "Properties"
        private List<Models.Employees> _employees;
        private List<Models.EmployeePensionData> _employeePensionData;
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
            switch(userInput)
            {
                case (int) UserOptions.ShowData:
                    DisplayDataOption();
                    break;
                case (int) UserOptions.DeleteUser:
                    DeleteUserOption();
                    break;
                case (int) UserOptions.IncreasePensionFunds:
                    IncreasePensionFundsOption();
                    break;
            }
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
            DisplayDataOption();
        }

        private void DisplayDataOption()
        {
            var dataBoard = new DataBoard();
            //_employees = Models.Employees.GetAll().ToList();
            //dataBoard.DisplayData(_employees);
            _employeePensionData = Models.EmployeePensionData.GetEmployeePensionDataView().ToList();
            dataBoard.DisplayData(_employeePensionData);
        }

        private void DeleteUserOption()
        {
            DisplayDataOption();
            Console.WriteLine();
            Console.WriteLine("Please enter the index of the user you would like to delete or enter -1 to cancel.");
            int userInput = GetUserInput();
            if(userInput != -1)
            {
                DeleteUser(userInput);
            }
        }

        private void DeleteUser(int userIndex)
        {
            Models.Employees employee = GetEmployeeAtIndex(userIndex);
            Console.Write(String.Format("Are you sure you want to delete the user {0} {1}? (y/n)", employee.FirstName, employee.LastName));
            if(Console.ReadLine().ToUpper() == "Y")
            {
                employee.Delete();
            }
        }

        private Models.Employees GetEmployeeAtIndex(int i)
        {
            if(_employees != null && i < _employees.Count)
            {
                return _employees[i];
            }
            if(_employeePensionData != null && i < _employeePensionData.Count)
            {
                return _employeePensionData[i].Employee;
            }
            return null;
        }
        #endregion
    }
}
