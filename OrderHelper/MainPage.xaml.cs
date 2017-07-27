using Microsoft.EntityFrameworkCore;
using OrderHelper.Data;
using OrderHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OrderHelper
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private ICollection<Category> Categories;
        private Order _order;
        private ObservableCollection<OrderDetail> _orderDetails;
        private bool _hasOrderItem;

        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails; }
            set
            {
                _orderDetails = value;
                OnPropertyChanged();
            }
        }
        public bool HasOrderItem
        {
            get { return _hasOrderItem; }
            set
            {
                _hasOrderItem = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public MainPage()
        {
            using (var db = new OrderHelperContext())
            {
                // Load all products under each category for menu to display
                Categories = db.Categories
                    .Include(c => c.Products)
                    .ToList();
            }
            this.InitializeOrders();
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Click product list view to add an order item
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (var db = new OrderHelperContext())
            {
                // Find product ID for the clicked item
                int clickedPrdID = (e.ClickedItem as Product).ID;
                // Check if there is existing order item for this product
                OrderDetail orderItem = OrderDetails
                    .Where(od => od.ProductID == clickedPrdID && String.IsNullOrEmpty(od.Remarks))
                    .FirstOrDefault();
                // If clicked product does not exist in current order items, create a new item for this product
                if (orderItem == null)
                {
                    var productToAdd = db.Products
                        .SingleOrDefault(p => p.ID == clickedPrdID);
                    if (productToAdd == null)
                        return;
                    var orderDetail = new OrderDetail
                    {
                        Quantity = 1,
                        ProductID = clickedPrdID,
                        UnitPrice = productToAdd.UnitPrice,
                        Product = productToAdd,
                    };
                    OrderDetails.Add(orderDetail);
                }
                // If clicked product alreay exist in order list, increase the product quantity by 1
                else
                {
                    orderItem.Quantity++;
                }
            }
        }

        // Click order details list view to cancel an order item
        private void OrderDetailsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            OrderDetail orderItemToRemove = e.ClickedItem as OrderDetail;
            if (--orderItemToRemove.Quantity == 0)
            {
                this.OrderDetails.Remove(orderItemToRemove);
            }
        }

        public void OrderDetailCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OrderDetail od in e.NewItems)
                {
                    od.Order = this.Order;
                    od.PropertyChanged += this.OrderDetailPropertyChangedHandler;
                }
            }
            this.CalculateSubtotal();
            this.HasOrderItem = this.OrderDetails.Any();
        }

        public void OrderDetailPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UnitPrice" || e.PropertyName == "Quantity")
            {
                // Unit price or quantity changed, re-calculate order subtotal
                this.CalculateSubtotal();
            }
        }

        // Calculate the subtotal of current order from order details
        private void CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (OrderDetail od in OrderDetails)
            {
                subtotal += od.UnitPrice * od.Quantity;
            }
            Order.Subtotal = subtotal;
        }

        // Reset current Order and Order Details
        private void InitializeOrders()
        {
            Order = new Order();
            OrderDetails = new ObservableCollection<OrderDetail>();
            OrderDetails.CollectionChanged += this.OrderDetailCollectionChangedHandler;
            HasOrderItem = false;
            Order.OrderDetails = this.OrderDetails;
        }

        private async void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cancelOrderDialog = new ContentDialog
            {
                Title = "Cancel Order",
                Content = "Are you sure you want to cancel the entire order?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };
            if (ContentDialogResult.Primary == await cancelOrderDialog.ShowAsync())
            {
                this.InitializeOrders();
            }
        }

        private async void btn_Checkout_Click(object sender, RoutedEventArgs e)
        {
            // Set order
            Order.OrderDate = DateTime.Now;
            Order.Payment = (sender as Button).Content.ToString();
            // Speak order
            await Order.Speak();
            // Print order
            try
            {
                await Order.Print();
            }
            catch(Exception ex)
            {
                this.errorMsg.Text = ex.Message;
                this.errorPopup.IsOpen = true;
            }
            // Save order
            using (var db = new OrderHelperContext())
            {
                foreach (OrderDetail od in OrderDetails)
                {
                    //db.Entry(od.Product).State = EntityState.Detached;
                    od.Product = null;
                }
                db.Add(this.Order);
                await db.SaveChangesAsync();
            }
            // Reset order
            this.InitializeOrders();
        }

    }
}
