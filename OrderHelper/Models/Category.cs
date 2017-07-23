using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHelper.Models
{
    public class Category
    {
        //Properties
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public ICollection<Product> Products { get; set; }
    }
}
