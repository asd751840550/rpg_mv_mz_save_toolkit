using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using rpg_save_toolkit.UI.CustomControls;
using rpg_save_toolkit.UI.Enums;
using rpg_save_toolkit.Util.SaveFileAssist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace rpg_save_toolkit.UI.ViewModels
{
    public class HomePageViewModel : BasePageViewModel
    {
        public HomePageViewModel()
        {
            Title = "Home";
            Icon = new Uri("pack://application:,,,/rpg_mv_mz_save_toolkit;component/res/Home.gif");
        }
        public async void MainGrid_Drop(object sender, DragEventArgs e)
        {
            bool isSupportFile = false;
            EditPageViewModel? viewmodel = null;
            if (e.Data.GetData(DataFormats.FileDrop) is string[] filePaths)
            {
                var filePath = filePaths.FirstOrDefault();
                if(string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    goto END;
                }
                Window? tmpWindow = Window.GetWindow(sender as DependencyObject);
                if(tmpWindow == null)
                {
                    goto END;
                }
                isSupportFile = LoadFile(filePath, out viewmodel);
            }
        END:
            if (!isSupportFile)
            {
                Window? window = Window.GetWindow(sender as DependencyObject);
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Warning, "Not support file."));
                return;
            }

            if(viewmodel != null)
            {
                MainWindowViewModel.Show(viewmodel);
            }
        }

        public async void BtnSelectFile_Clicked(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Please select rpg save file.";
            openFileDialog.Filter = "RPGMV/MZ(*.rpgsave;*.rmmzsave)|*.rpgsave;*.rmmzsave|RPGMV(*.rpgsave)|*.rpgsave|RPGMZ(*.rmmzsave)|*.rmmzsave";
            if(!(openFileDialog.ShowDialog() ?? false))
            {
                Window? window = Window.GetWindow(sender as DependencyObject);
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Warning, "Please select a file."));
                return;
            }
            if (LoadFile(openFileDialog.FileName, out EditPageViewModel? viewmodel))
            {
                MainWindowViewModel.Show(viewmodel!);
            }
        }

        public bool LoadFile(string filePath, out EditPageViewModel? viewmodel)
        {
            viewmodel = MainWindowViewModel.GetViewModel<EditPageViewModel>();
            if(viewmodel == null)
            {
                return false; 
            }
            return viewmodel.Load(filePath);
        }
    }
}
