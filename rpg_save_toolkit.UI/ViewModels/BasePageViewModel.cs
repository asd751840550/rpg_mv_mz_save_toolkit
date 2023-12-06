using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.UI.ViewModels
{
    public partial class BasePageViewModel : ObservableObject
    {
        [ObservableProperty]
        public object? _content;
        [ObservableProperty]
        public string? _title;
    }
}
