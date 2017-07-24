using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OrderHelper
{
    public class DecimalConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return String.Format("{0:f2}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Decimal.Parse(value as string);
        }
    }

    public class ReverseBoolConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return !(bool)value;
        }
    }
}
