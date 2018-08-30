using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Dapper;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;

namespace _7_ORMs_and_Migrations.Models
{
  public class DataBaseObject
  {
    protected static readonly string _connectionString =
      new SqlConnectionStringBuilder
      {
        DataSource = "DT283",
        IntegratedSecurity = true,
        InitialCatalog = "Employees"
      }.ConnectionString;
  }
}