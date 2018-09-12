using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.UserCommandOptions
{
    class IncreasePensionFundsOption : BaseUserCommand, IUserCommandOption
    {
        public void Run()
        {
            Models.PensionFund.IncreasePensionFunds();
            DisplayData();
        }
    }
}
