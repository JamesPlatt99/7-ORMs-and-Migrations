using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class DisplayDataboardOption : BaseUserCommand, IUserCommandOption
    {
        private const int _pageLength = 10;
        private int curPage;

        public void Run()
        {
            curPage = 0;
            do
            {
                DisplayData();
                curPage = GetNextPage();
            } while (curPage >= 0);

        }

        new private void DisplayData()
        {
            var dataBoard = new DataBoard();
            var data = GetData();
            dataBoard.DisplayData(data);
        }

        private List<Models.IDataboardObject> GetData()
        {
            var rowData = new List<Models.IDataboardObject>();
            rowData.AddRange(Models.EmployeeOverview.GetNAfterIndex(_pageLength, curPage * _pageLength));
            RowData = rowData;
            return rowData;
        }

        private void DisplayOptions()
        {
            if(curPage > 0)
            {
                Console.WriteLine(" 1 - previous page");
            }
            Console.WriteLine(" 2 - next page");
            Console.WriteLine(" Any other key to exit");
        }

        private int GetNextPage()
        {
            DisplayOptions();
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    return Math.Max(0, curPage - 1);
                case "2":
                    return curPage + 1;
                default:
                    return -1;
            }    
        }
    }
}
