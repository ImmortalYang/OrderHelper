using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderHelper.Models
{
    public class OrderDetail: INotifyPropertyChanged
    {
        private string _remarks;
        private decimal _unitPrice;
        private int _quantity;
        //Properties
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                OnPropertyChanged();
            }
        }
        public decimal UnitPrice {
            get { return _unitPrice; }
            set {
                _unitPrice = value;
                OnPropertyChanged();
            }
        }
        public int Quantity {
            get { return _quantity; }
            set {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        //Navigation Properties
        public Product Product { get; set; }
        public Order Order { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
