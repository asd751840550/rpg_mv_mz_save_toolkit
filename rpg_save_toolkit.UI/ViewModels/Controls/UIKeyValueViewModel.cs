using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace rpg_save_toolkit.UI.ViewModels.Controls
{
    public partial class UIKeyValueViewModel :ObservableObject
    {
        [ObservableProperty]
        private string _key = string.Empty;
        [ObservableProperty]
        private string? _value;
        public UIKeyValueViewModel? Prev { get; set; }
        public UIKeyValueViewModel? Next { get; set; }
        
    }
}
