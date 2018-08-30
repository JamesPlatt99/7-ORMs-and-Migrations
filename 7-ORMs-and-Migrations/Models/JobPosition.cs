using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class JobPosition : DataBaseObject
    {
        #region "Properties"
        public int ID { get; }
        public string Title { get; set; }
        #endregion

        #region "Static Methods"
        public static IEnumerable<JobPosition> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<JobPosition>("SELECT * FROM JobPosition");
            }
        }

        public static JobPosition GetSingle(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<JobPosition>(
                  "SELECT * FROM JobPosition WHERE ID = @id",
                  new { id }).Single();
            }
        }
        #endregion
    }
}
