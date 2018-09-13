using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class SearchUserCommand : BaseUserCommand, IUserCommandOption
    {
        #region "Enumeration"
        private enum SearchOptions
        {
            Name = 1,
            JobTitle = 2,
            PensionFundProvider = 3
        }
        #endregion
        
        #region "Public Methods"
        public void Run()
        {
            var dataBoard = new DataBoard();
            var data = GetData();
            dataBoard.DisplayData(data);
        }
        #endregion

        #region "Private Methods"

        private List<Models.IDataboardObject> GetData()
        {
            var newRowData = GetDataboardObjects();
            RowData = newRowData;
            return newRowData;
        }

        private List<Models.IDataboardObject> GetDataboardObjects()
        {
            DisplayOptions();
            var output = new List<Models.IDataboardObject>();
            int userInput = GetUserInput();
            string query = GetUserQueryText();
            var timer = new Stopwatch();
            timer.Start();
            switch(userInput)
            {
                case (int)SearchOptions.JobTitle:
                    output.AddRange(Models.EmployeeOverview.SearchByJobTitle(query));
                    break;
                case (int)SearchOptions.Name:
                    output.AddRange(Models.EmployeeOverview.SearchByName(query));
                    break;
                case (int)SearchOptions.PensionFundProvider:
                    output.AddRange(Models.EmployeePensionData.SearchByPensionFundProvider(query));
                    break;
            }
            timer.Stop();
            Console.WriteLine(String.Format("{0:N}", timer.ElapsedMilliseconds));
            return output;
        }

        private string GetUserQueryText()
        {
            string queryText = string.Empty;
            while(string.IsNullOrEmpty(queryText))
            {
                Console.WriteLine("Please enter your search query");
                Console.Write(" :");
                queryText = Console.ReadLine();
            }
            return queryText;
        }

        private void DisplayOptions()
        {
            Console.WriteLine("Search Options");
            Console.WriteLine("----------------");
            Enum.GetValues(typeof(SearchOptions)).Cast<SearchOptions>().ToList().ForEach(n => Console.WriteLine(String.Format(" {0} - {1}", (int)n, n)));
        }
        #endregion
    }
}
