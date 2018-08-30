using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class PensionFundProvider
    {
        public int ID { get; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
