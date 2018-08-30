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

        const int nameFieldLength = 20;
        const int jobTitleFieldLength = 20;
        const int salaryFieldLength = 12;
        const int pensionFundContributionsFieldLength = 26;

        const string nameHeader = "Name";
        const string jobTitleHeader = "Job Title";
        const string salaryHeader = "Salary";
        const string pensionFundContributionsHeader = "Pension Fund Contributions";
        #endregion

        #region "Public Methods"

        public void DisplayData(List<Models.Employees> employees)
        {
            WriteHeader();
            WriteBody(employees);
        }
        #endregion

        #region "Private Methods"
        private void WriteBody(List<Models.Employees> employees)
        {
            employees.ForEach(n => Console.WriteLine(GetLine(n)));
            Console.WriteLine(GetHorizontalLine());
        }

        private void WriteHeader()
        {
            Console.WriteLine("Employee database");
            Console.WriteLine(GetHorizontalLine());
            Console.WriteLine(GetColumns());
            Console.WriteLine(GetHorizontalLine());
        }

        private string GetHorizontalLine()
        {
            // Total length of the table will be equal to the sum of the field lengths as well as a seperator for each field.
            int numFields = 4;
            int fieldLength = nameFieldLength + jobTitleFieldLength + salaryFieldLength + pensionFundContributionsFieldLength;
            int totalLength = numFields + fieldLength - 1;
            return "|" + String.Join("", Enumerable.Range(0, totalLength).Select(n => "-")) + "|";
        }

        private string GetColumns()
        {
            var output = new StringBuilder();
            output.AppendFormat("|{0}|", FormatCell(nameHeader, nameFieldLength));
            output.AppendFormat("{0}|", FormatCell(jobTitleHeader, jobTitleFieldLength));
            output.AppendFormat("{0}|", FormatCell(salaryHeader, salaryFieldLength));
            output.AppendFormat("{0}|", FormatCell(pensionFundContributionsHeader, pensionFundContributionsFieldLength));
            return output.ToString();
        }

        private string GetLine(Models.Employees employee)
        {
            string name = String.Format("{0} {1}", employee.FirstName, employee.LastName);
            string jobTitle = employee.JobPosition.Title;
            string salary = String.Format("£{0:n}", employee.Salary);
            string pensionFundContributions = String.Format("£{0:n}", (employee.PensionFund?.ContributionAmount).GetValueOrDefault(0));

            var output = new StringBuilder();
            output.AppendFormat("|{0}|", FormatCell(name, nameFieldLength));
            output.AppendFormat("{0}|", FormatCell(jobTitle, jobTitleFieldLength));
            output.AppendFormat("{0}|", FormatCell(salary, salaryFieldLength));
            output.AppendFormat("{0}|", FormatCell(pensionFundContributions, pensionFundContributionsFieldLength));
            return output.ToString();
        }

        private string FormatCell(string text, int cellsize)
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
