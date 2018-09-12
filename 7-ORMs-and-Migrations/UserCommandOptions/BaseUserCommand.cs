using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class BaseUserCommand
    {

        #region "Protected Methods"
        protected static List<Models.IDataboardObject> RowData { get; set; }
        protected void DisplayData()
        {
            var displayDataBoardOption = new DisplayDataboardOption();
            displayDataBoardOption.Run();
        }
        protected Models.Employees UserSelectEmployee()
        {
            DisplayData();
            Console.WriteLine("Please enter the index of the user you would like to delete or enter -1 to cancel.");
            Console.WriteLine();
            int userInput = GetUserInput();
            if (userInput > 0)
            {
                return GetEmployeeAtIndex(userInput);
            }
            return null;
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

        #region "Private Methods"
        private Models.Employees GetEmployeeAtIndex(int index)
        {
            if (RowData != null && index < RowData.Count)
            {
                return RowData[index].GetEmployee();
            }
            return null;
        }
        #endregion
    }
}
