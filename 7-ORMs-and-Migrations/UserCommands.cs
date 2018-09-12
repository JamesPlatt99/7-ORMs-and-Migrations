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
                    userCommandOption = new UserCommandOptions.DeleteUserOption();
                    break;
                case (int) UserOptions.IncreasePensionFunds:
                    userCommandOption = new UserCommandOptions.IncreasePensionFundsOption();
                    break;
                case (int) UserOptions.UpdateName:
                    userCommandOption = new UserCommandOptions.UpdateEmployeeNameOption();
                    break;
            }
            userCommandOption?.Run();
        }

        private void DisplayOptions()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("----------------");
            Enum.GetValues(typeof(UserOptions)).Cast<UserOptions>().ToList().ForEach(n => Console.WriteLine(String.Format(" {0} - {1}", (int) n, n)));
        }

        protected int GetUserInput()
        {
            int userInput = -2;
            while (userInput == -2)
            {
                Console.Write(" :");
                int.TryParse(Console.ReadLine(), out userInput);
            }
            return userInput;
        }

        #endregion
    }
}
