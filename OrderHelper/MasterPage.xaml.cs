using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class MasterPage : Page
    {
        public MasterPage()
        {
            this.InitializeComponent();
            this.navListView.SelectedIndex = 0;
            this.contentFrame.Navigate(typeof(MainPage));
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            rootSplitView.IsPaneOpen = !rootSplitView.IsPaneOpen;
        }

        private void navListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (navListView.SelectedIndex)
            {
                case 0:
                    contentFrame.Navigate(typeof(MainPage));
                    break;
                case 1:
                    contentFrame.Navigate(typeof(OrderIndexPage));
                    break;
                default:
                    break;
            }
        }
    }
}
