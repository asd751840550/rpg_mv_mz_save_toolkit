using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace rpg_save_toolkit.UI.Converters.MultiBindingConverters
{
    public class BooleanToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility ret = Visibility.Collapsed;
            foreach (var v in values)
            {
                if(v is bool b)
                {
                    ret = b ? Visibility.Visible : Visibility.Collapsed;
                    if(ret == Visibility.Visible)
                    { break; }
                }
            }

            return ret;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
