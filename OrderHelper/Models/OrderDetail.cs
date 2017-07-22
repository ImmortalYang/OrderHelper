using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderHelper.Models
{
    class OrderDetail
    {
        //Properties
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        //Navigation Properties
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
