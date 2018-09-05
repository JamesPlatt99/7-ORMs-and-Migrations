using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;

namespace _7_ORMs_and_Migrations.Models
{
    class Employees : DataBaseObject, IDataboardObject
    {
        #region "Properties"       
        public Guid ID { get; }

        private string FirstName;
        public string EmployeeFirstName
        {
            get
            {
                return FirstName;
            }
            set
            {
                if(FirstName != value)
                {
                    FirstName = value;
                    ValueUpdated("FirstName", value);
                }
            }
        }

        private string LastName;
        public string EmployeeLastName
        {
            get
            {
                return LastName;
            }
            set
            {
                if (LastName != value)
                {
                    LastName = value;
                    ValueUpdated("LastName", value);
                }
            }
        }

        private int JobPositionID;
        public int EmployeeJobPositionID
        {
            get
            {
                return JobPositionID;
            }
            set
            {
                if (JobPositionID != value)
                {
                    JobPositionID = value;
                    ValueUpdated("JobPositionID", value.ToString());
                }
            }
        }

        private Guid? PensionFundID;
        public Guid? EmployeePensionFundID
        {
            get
            {
                return PensionFundID;
            }
            set
            {
                if (PensionFundID != value)
                {
                    PensionFundID = value;
                    ValueUpdated("PensionFundID", value.ToString());
                }
            }
        }

        private decimal Salary;
        public decimal EmployeeSalary
        {
            get
            {
                return Salary;
            }
            set
            {
                if (Salary != value)
                {
                    Salary = value;
                    ValueUpdated("Salary", value.ToString());
                }
            }
        }
        private int Age;
        public int EmployeeAge
        {
            get
            {
                return Age;
            }
            set
            {
                if (Age != value)
                {
                    Age = value;
                    ValueUpdated("Age", value.ToString());
                }
            }
        }


        private JobPosition _jobPosition;
        public JobPosition JobPosition
        {
            get
            {
                if(_jobPosition == null)
                {
                    _jobPosition = Models.JobPosition.GetSingle(this.EmployeeJobPositionID);
                }
                return _jobPosition;
            }
            set
            {
                _jobPosition = value;
                this.EmployeeJobPositionID = value.ID;
            }
        }

        private PensionFund _pensionFund;
        public PensionFund PensionFund
        {
            get
            {
                if (_pensionFund == null && this.EmployeePensionFundID != null)
                {
                    _pensionFund = Models.PensionFund.GetSingle(this.EmployeePensionFundID.Value);
                }
                return _pensionFund;
            }
            set
            {
                _pensionFund = value;
                this.EmployeePensionFundID = value.ID;
            }
        }
        #endregion

        #region "Public Methods"
        public void Save()
        {
            var updateString = GetUpdatedValuesQueryString();
            if (!string.IsNullOrWhiteSpace(updateString))
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Query<Employees>(String.Format("UPDATE Employees SET {0} WHERE ID = @id", updateString)
                        , new { this.ID });
                }
            }
        }

        public void Delete()
        {
            // We must first delete the foreign key
            this.PensionFund?.Delete();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Query<Employees>("DELETE FROM Employees WHERE ID = @id"
                    , new { this.ID});
            }
        }
        #endregion

        #region "Static Methods"
        public static IEnumerable<Employees> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Employees>("SELECT * FROM Employees");
            }
        }

        public static Employees GetSingle(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Employees>(
                  "SELECT * FROM Employees WHERE ID = @id",
                  new { id }).Single();
            }
        }
        #endregion

        #region "Interface Members"
        public string GetName() => String.Format("{0} {1}", this.EmployeeFirstName, this.EmployeeLastName);
        public string GetJobTitle() => this.JobPosition.Title;
        public decimal GetSalary() => this.EmployeeSalary;
        public Employees GetEmployee() => this;
        public decimal? GetPensionFundContributions() => this.PensionFund?.ContributionAmount;
        #endregion
    }
}
