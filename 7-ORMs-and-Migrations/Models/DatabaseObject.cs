using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Dapper;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using System.Text;

namespace _7_ORMs_and_Migrations.Models
{
    public class DataBaseObject
    {
        private Dictionary<string, string> _updates;

        protected static string LikeQuery(string query)
        {
            query = query.Replace("[", "[[]").Replace("%", "[%]");
            return string.Format("%{0}%", query);
        }

        protected static readonly string _connectionString =
          new SqlConnectionStringBuilder
          {
              DataSource = "DT283",
              IntegratedSecurity = true,
              InitialCatalog = "Employees"
          }.ConnectionString;
        
        protected void ValueUpdated(string databaseColumn, string value)
        {
            _updates = _updates ?? new Dictionary<string, string>();
            if (_updates.ContainsKey(databaseColumn))
            {
                _updates[databaseColumn] = value;
            }
            else
            {
                _updates.Add(databaseColumn, value);
            }
        }

        protected string GetUpdatedValuesQueryString()
        {
            var updates = new List<string>();
            foreach(var update in _updates)
            {
                updates.Add(string.Format("{0} = '{1}'", update.Key, update.Value));
            }
            return string.Join(", ", updates);
        }
    }
}