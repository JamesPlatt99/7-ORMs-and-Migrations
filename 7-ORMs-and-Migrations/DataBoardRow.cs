using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations
{
    class DataBoardRow
    {
        #region "Properties
        const int indexFieldLength = 5;
        const int nameFieldLength = 20;
        const int jobTitleFieldLength = 20;
        const int salaryFieldLength = 12;
        const int pensionFundContributionsFieldLength = 26;

        const string indexHeader = "Index";
        const string nameHeader = "Name";
        const string jobTitleHeader = "Job Title";
        const string salaryHeader = "Salary";
        const string pensionFundContributionsHeader = "Pension Fund Contributions";

        public int Index { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public decimal PensionFundContributions { get; set; }
        public Models.Employees Employee { get; set; }
        #endregion

        #region "Public Methods"
        public DataBoardRow(Models.Employees employee, int index)
        {
            this.Index = index;
            this.Name = String.Format("{0} {1}", employee.FirstName, employee.LastName);
            this.JobTitle = employee.JobPosition.Title;
            this.Salary = employee.Salary;
            this.PensionFundContributions = this.Employee.PensionFund.ContributionAmount;
            this.Employee = employee;
        }
        public DataBoardRow(Models.EmployeePensionData employeePensionDataView, int index)
        {
            this.Index = index;
            this.Name = employeePensionDataView.EmployeeName;
            this.Employee = employeePensionDataView.Employee;
            this.JobTitle = this.Employee.JobPosition.Title;
            this.Salary = this.Employee.Salary;
            this.PensionFundContributions = employeePensionDataView.PensionFundSize;
        }
        public void PrintLine()
        {
            string rowIndex = this.Index.ToString();
            string rowSalary = String.Format("£{0:n}", this.Salary);
            string rowPensionFundContributions = this.PensionFundContributions.ToString("£##,#.00") ?? "N/A";

            var output = new StringBuilder();
            output.AppendFormat("|{0}|", FormatCell(rowIndex, indexFieldLength));
            output.AppendFormat("{0}|", FormatCell(this.Name, nameFieldLength));
            output.AppendFormat("{0}|", FormatCell(this.JobTitle, jobTitleFieldLength));
            output.AppendFormat("{0}|", FormatCell(rowSalary, salaryFieldLength));
            output.AppendFormat("{0}|", FormatCell(rowPensionFundContributions, pensionFundContributionsFieldLength));
            Console.WriteLine(output.ToString());
        }
        #endregion

        #region "Static Methods"

        public static void PrintHeader()
        {
            Console.WriteLine("Employee database");
            Console.WriteLine(GetHorizontalLine());
            Console.WriteLine(GetColumnHeaders());
            Console.WriteLine(GetHorizontalLine());
        }

        private static string GetHorizontalLine()
        {
            // Total length of the table will be equal to the sum of the field lengths as well as a seperator for each field.
            int numFields = 5;
            int fieldLength = indexFieldLength + nameFieldLength + jobTitleFieldLength + salaryFieldLength + pensionFundContributionsFieldLength;
            int totalLength = numFields + fieldLength - 1;
            return "|" + String.Join("", Enumerable.Range(0, totalLength).Select(n => "-")) + "|";
        }

        private static string GetColumnHeaders()
        {
            var output = new StringBuilder();
            output.AppendFormat("|{0}|", FormatCell(indexHeader, indexFieldLength));
            output.AppendFormat("{0}|", FormatCell(nameHeader, nameFieldLength));
            output.AppendFormat("{0}|", FormatCell(jobTitleHeader, jobTitleFieldLength));
            output.AppendFormat("{0}|", FormatCell(salaryHeader, salaryFieldLength));
            output.AppendFormat("{0}|", FormatCell(pensionFundContributionsHeader, pensionFundContributionsFieldLength));
            return output.ToString();
        }

        private static string FormatCell(string text, int cellsize)
        {
            if (text.Length > cellsize)
            {
                text = text.Substring(0, cellsize - 4) + "...";
            }
            if (text.Length < cellsize)
            {
                text = text.PadRight(cellsize);
            }
            return text;
        }
        #endregion
    }
}
