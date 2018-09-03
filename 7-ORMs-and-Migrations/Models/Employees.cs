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
    class Employees : DataBaseObject
    {
        #region "Properties"
        public Guid ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JobPositionID { get; set; }        
        public Guid? PensionFundID { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }

        private JobPosition _jobPosition;
        public JobPosition JobPosition
        {
            get
            {
                if(_jobPosition == null)
                {
                    _jobPosition = Models.JobPosition.GetSingle(this.JobPositionID);
                }
                return _jobPosition;
            }
            set
            {
                _jobPosition = value;
                this.JobPositionID = value.ID;
            }
        }

        private PensionFund _pensionFund;
        public PensionFund PensionFund
        {
            get
            {
                if (_pensionFund == null && this.PensionFundID != null)
                {
                    _pensionFund = Models.PensionFund.GetSingle(this.PensionFundID.Value);
                }
                return _pensionFund;
            }
            set
            {
                _pensionFund = value;
                this.PensionFundID = value.ID;
            }
        }
        #endregion

        #region "Public Methods"
        public void Save()
        {
            throw new NotImplementedException();
            using (var connection = new SqlConnection(_connectionString))
            {
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
    }
}
