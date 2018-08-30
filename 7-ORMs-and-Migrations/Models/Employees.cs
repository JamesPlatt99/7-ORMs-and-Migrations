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
