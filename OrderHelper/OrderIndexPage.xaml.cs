using Microsoft.EntityFrameworkCore;
using OrderHelper.Data;
using OrderHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OrderHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderIndexPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders {
            get { return _orders; }
            set {
                _orders = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateOfOrders;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public OrderIndexPage()
        {
            this.DateOfOrders = DateTime.Today;
            using(var db = new OrderHelperContext())
            {
                var orderList = db.Orders
                    .Where(o => o.OrderDate.Date == DateOfOrders.Date)
                    .OrderBy(o => o.OrderDate)
                    .ToList();
                this.Orders = new ObservableCollection<Order>(orderList);
            }
            this.InitializeComponent();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Print the receipt of selected order
        private async void btn_Print_Click(object sender, RoutedEventArgs e)
        {
            var orderID = (sender as Button).TabIndex;
            using(var db = new OrderHelperContext())
            {
                var orderToPrint = await db.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Product)
                    .SingleOrDefaultAsync(o => o.ID == orderID);
                if (orderToPrint == null)
                    return;
                await orderToPrint.Print();
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            int orderID = (sender as Button).TabIndex;
            var orderToDelete = Orders
                .SingleOrDefault(o => o.ID == orderID);
            if (orderToDelete == null)
                return;
            using(var db = new OrderHelperContext())
            {
                db.Entry(orderToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.SaveChangesAsync();
            }
            this.Orders.Remove(orderToDelete);
        }
    }
}
