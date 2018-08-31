using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class PensionFund : DataBaseObject
    {
        #region "Properties"
        public Guid ID { get; }
        public int PensionFundProviderID { get; set; }
        public decimal ContributionAmount { get; set; }
        public DateTime LastContributionDate { get; set; }

        private PensionFundProvider _pensionFundProvider;
        public PensionFundProvider PensionFundProvider
        {
            get
            {
                if (_pensionFundProvider == null)
                {
                    _pensionFundProvider = Models.PensionFundProvider.GetSingle(this.PensionFundProviderID);
                }
                return _pensionFundProvider;
            }
            set
            {
                _pensionFundProvider = value;
                this.PensionFundProviderID = value.ID;
            }
        }
        #endregion

        #region "Public Methods"
        public void Save()
        {
            using (var connection = new SqlConnection(_connectionString))
            {

            }
        }

        public void Delete()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Query<Employees>("DELETE FROM PensionFund WHERE ID = @id"
                    , new { this.ID });
            }
        }
        #endregion


        #region "Static Methods"
        public static IEnumerable<PensionFund> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PensionFund>("SELECT * FROM PensionFund");
            }
        }

        public static PensionFund GetSingle(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PensionFund>(
                  "SELECT * FROM PensionFund WHERE ID = @id",
                  new { id }).Single();
            }
        }
        #endregion
    }
}
