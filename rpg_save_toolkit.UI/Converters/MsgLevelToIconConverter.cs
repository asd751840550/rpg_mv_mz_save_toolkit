using MahApps.Metro.IconPacks;
using rpg_save_toolkit.UI.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace rpg_save_toolkit.UI.Converters
{
    public class MsgLevelToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EMsgLevel msglevel)
            {
                switch (msglevel)
                {
                    case EMsgLevel.Error:
                        return PackIconMaterialKind.WindowClose;
                    case EMsgLevel.Warning:
                        return PackIconMaterialKind.Alert;
                    case EMsgLevel.Info:
                        return PackIconMaterialKind.Information;
                }
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
