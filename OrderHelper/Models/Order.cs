using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderHelper.Models
{
    public class Order: INotifyPropertyChanged
    {
        private decimal _subtotal;
        private decimal _discount;
        private decimal _total;

        //Properties
        public int ID { get; set; }
        public int? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public decimal Subtotal {
            get { return _subtotal; }
            set {
                _subtotal = value;
                if (_subtotal == 0)
                    _discount = 0;
                _total = _subtotal * (1 - _discount);
                OnPriceChanged();
            }
        }
        public decimal Discount {
            get { return _discount; }
            set {
                _discount = value;
                _total = _subtotal * (1 - _discount);
                OnPriceChanged();
            }
        }
        public decimal Total {
            get { return _total; }
            set {
                _total = value;
                if (_subtotal != 0)
                    _discount = 1 - _total / _subtotal;
                OnPriceChanged();
            }
        }
        public DateTime OrderDate { get; set; }
        public string Payment { get; set; }

        public string Address { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }

        //Navigation Properties
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnPriceChanged()
        {
            OnPropertyChanged("Subtotal");
            OnPropertyChanged("Discount");
            OnPropertyChanged("Total");
        }
    }
}
