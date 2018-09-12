using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class EmployeePensionData : DataBaseObject, IDataboardObject
    {
        #region "Properties"
        public Guid EmployeeNumber { get; }
        public string EmployeeName { get; }
        public Decimal PensionFundSize { get; }
        public string PensionFundProvider { get; }

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

        public static IEnumerable<EmployeePensionData> GetEmployeePensionDataView()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeePensionData>(
                  "SELECT * FROM EmployeePensionData");
            }
        }        

        public static IEnumerable<EmployeePensionData> SearchByPensionFundProvider(string pensionFundProvider)
        {
            string query = LikeQuery(pensionFundProvider);
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<EmployeePensionData>(
                  "SELECT * FROM EmployeePensionData WHERE PensionFundProvider LIKE @query",
                  new { query });
            }
        }
        #endregion

        #region "Interface Members"
        public string GetName() => this.EmployeeName;
        public string GetJobTitle() => this.Employee.JobPosition.Title;
        public decimal GetSalary() => this.Employee.EmployeeSalary;
        public Employees GetEmployee() => this.Employee;
        public decimal? GetPensionFundContributions() => this.Employee.PensionFund?.ContributionAmount;        
        #endregion
    }
}
