using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class EmployeeOverview : DataBaseObject, IDataboardObject
    {
        #region "Properties"
        public Guid EmployeeNumber { get; }
        public string EmployeeName { get; }
        public string JobTitle { get; }
        public decimal Salary { get; }
        public Decimal PensionFundSize { get; }

        private Employees _employee;
        public Employees Employee
        {
            get
            {
                if (_employee == null)
                {
                    _employee = Models.Employees.GetSingle(this.EmployeeNumber);
                }
                return _employee;
            }
        }
        #endregion        

        #region "Static Methods"

        public static IEnumerable<EmployeeOverview> GetEmployeePensionDataView()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeeOverview>(
                  "SELECT * FROM EmployeeOverview");
            }
        }

        public static IEnumerable<EmployeeOverview> SearchByName(string name)
        {
            string query = LikeQuery(name);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeeOverview>(
                  "SELECT * FROM EmployeeOverview WHERE EmployeeName LIKE @query",
                  new { query });
            }
        }

        public static IEnumerable<EmployeeOverview> SearchByJobTitle(string jobTitle)
        {
            string query = LikeQuery(jobTitle);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeeOverview>(
                  "SELECT * FROM EmployeeOverview WHERE JobTitle LIKE @query",
                  new { query });
            }
        }
        public static IEnumerable<EmployeeOverview> GetNAfterIndex(int count, int index)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeeOverview>("SELECT * FROM EmployeeOverview ORDER BY EmployeeNumber OFFSET @index ROWS FETCH NEXT @count ROWS ONLY; ",
                    new { index, count });
            }
        }
        #endregion

        #region "Interface Members"
        public string GetName() => this.EmployeeName;
        public string GetJobTitle() => this.JobTitle;
        public decimal GetSalary() => this.Salary;
        public decimal? GetPensionFundContributions() => this.PensionFundSize;
        public Employees GetEmployee() => this.Employee;
        #endregion
    }
}
