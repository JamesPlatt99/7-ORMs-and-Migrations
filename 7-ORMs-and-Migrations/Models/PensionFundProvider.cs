using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class PensionFundProvider : DataBaseObject
    {
        #region "Properties"
        public int ID { get; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        #endregion

        #region "Static Methods"
        public static IEnumerable<PensionFundProvider> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PensionFundProvider>("SELECT * FROM PensionFundProvider");
            }
        }

        public static PensionFundProvider GetSingle(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PensionFundProvider>(
                  "SELECT * FROM PensionFundProvider WHERE ID = @id",
                  new { id }).Single();
            }
        }
        #endregion
    }
}
