using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class PensionFund
    {
        public Guid ID { get; }
        public int PensionFundProviderID { get; set; }
        public decimal ContributionAmount { get; set; }
        public DateTime LastContributionDate { get; set; }
    }
}
