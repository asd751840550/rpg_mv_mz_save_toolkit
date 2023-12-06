using Newtonsoft.Json.Linq;
using rpg_save_toolkit.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rpg_mv_mz_save_toolkit.Pages
{
    /// <summary>
    /// EditPage.xaml 的交互逻辑
    /// </summary>
    public partial class EditPage : UserControl
    {
        public EditPage()
        {
            InitializeComponent();
        }
        public EditPage(EditPageViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
            //if (this.TryFindResource("InstanceVM") is EditPageViewModel viewmodel)
            //{
            //    viewmodel.Load(src);
            //}
        }

    }
}
