using rpg_mv_mz_save_toolkit.Pages;
using rpg_save_toolkit.UI.ViewModels;
using rpg_save_toolkit.Util.Assists;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace rpg_mv_mz_save_toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            if(this.TryFindResource("InstanceVM") is MainWindowViewModel viewmodel)
            {
                viewmodel.Pages.Add(GeneratePage<HomePageViewModel, HomePage>()!);
                viewmodel.Pages.Add(GeneratePage<EditPageViewModel, EditPage>()!);
                MainWindowViewModel.Show<HomePageViewModel>();
            }
            
            //JsPakoAssist.Instance.Init();
            //var tmpJson = JsPakoAssist.Instance.DeCompressionJson(File.ReadAllBytes(@"D:\BaiduNetdiskDownload\Night Stroll\Night Stroll\save\file1.rmmzsave"));
            //var tmpzipdata = JsPakoAssist.Instance.CompressionJson(tmpJson);
            //File.WriteAllBytes(@"D:\BaiduNetdiskDownload\Night Stroll\Night Stroll\save\file1.rmmzsave", tmpzipdata);
            //int i = 0;
            //JsPakoAssist.Instance.Dispose();

        }

        private BasePageViewModel? GeneratePage<VM, V>() 
            where VM : BasePageViewModel 
            where V : UserControl
        {
            BasePageViewModel? ret = Activator.CreateInstance(typeof(VM)) as BasePageViewModel;
            ret!.Content = Activator.CreateInstance(typeof(V));
            if(ret!.Content is UserControl userControl)
            {
                userControl.DataContext = ret;
            }
            return ret;
        }
        //public MainWindowViewModel? InstanceVM { get; private set; }
    }
}
