using MahApps.Metro.IconPacks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace rpg_save_toolkit.UI.Converters.MultiBindingConverters
{
    public class TreeViewToIconPackConverter : IMultiValueConverter
    {
        public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter == null)
            { return null; }
            if(parameter.ToString() == "icon")
            {
                PackIconMaterialKind ret = PackIconMaterialKind.None;
                if (values.Length == 2 && values[0] is bool isExpended && values[1] is IList list)
                {
                    if (list.Count == 0)
                    {
                        ret = PackIconMaterialKind.Alphabetical;//PackIconMaterialKind.File;
                    }
                    else
                    {
                        ret = isExpended ? PackIconMaterialKind.FolderOpen : PackIconMaterialKind.Folder;
                    }
                }
                return ret;
            }
            else if(parameter.ToString() == "color")
            {
                Brush? ret = null;
                if (values.Length == 2 && values[0] is bool isExpended && values[1] is IList list)
                {
                    if (list.Count == 0)
                    {
                        ret = Brushes.DodgerBlue;
                    }
                    else
                    {
                        ret = Brushes.Orange;
                    }
                }
                return ret;
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
