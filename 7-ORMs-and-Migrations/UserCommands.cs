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
            Exit = 9
        }
        #endregion

        #region "Properties"
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
                    DisplayData();
                    break;
            }
        }
        private int GetUserInput()
        {
            int userInput = 0;
            while(userInput == 0)
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

        private void DisplayData()
        {
            var dataBoard = new DataBoard();
            var employees = Models.Employees.GetAll().ToList();
            dataBoard.DisplayData(employees);
        }
        #endregion
    }
}
