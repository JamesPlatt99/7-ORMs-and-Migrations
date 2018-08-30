using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ORMs_and_Migrations.Models
{
    class Employees
    {
        public Guid ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int JobPositionID { get; set; }
        public Guid? PensionFundID { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
    }
}
