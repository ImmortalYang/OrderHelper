using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHelper.Models
{
    public class Employee
    {
        // Properties
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IRDNumber { get; set; }

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
