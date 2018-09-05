using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    interface IDataboardObject
    {
        string GetName();
        string GetJobTitle();
        decimal GetSalary();
        decimal? GetPensionFundContributions();
        Employees GetEmployee();

    }
}
