using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations
{
    class DataBoard
    {
        #region "Properties"

        public List<DataBoardRow> Rows = new List<DataBoardRow>();
        #endregion

        #region "Public Methods"

        public void DisplayData(List<Models.IDataboardObject> employees)
        {
            DataBoardRow.PrintHeader();
            PopulateRows(employees);
            PrintBody();
        }
        #endregion

        #region "Private Methods"
        private void PopulateRows(List<Models.IDataboardObject> employees)
        {
            for(int i = 0; i < employees.Count; i++)
            {
                Rows.Add(new DataBoardRow(employees[i], i));
            }
        }

        private void PrintBody()
        {
            Rows.ForEach(n => n.PrintLine());
        }
        #endregion
    }
}
