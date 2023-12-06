using CommunityToolkit.Mvvm.ComponentModel;
using rpg_save_toolkit.UI.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace rpg_save_toolkit.UI.ViewModels
{
    public partial class MainWindowViewModel : BasePageViewModel
    {

        public MainWindowViewModel()
        {
            
        }
        [ObservableProperty]
        private ICommand _windowStateCommand = new CustomCommand((p) => { return true; }, (p) =>
        {
            if (p is MainWindowViewModel window)
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        });

        [ObservableProperty]
        private string _windowTitle = "RPG Save Editor";
        //[ObservableProperty]
        //private string _pageTitle = "Home";
        [ObservableProperty]
        private WindowState _windowState = WindowState.Maximized;
        [ObservableProperty]
        private BasePageViewModel? _selectedItem = null;
        

        public IList<BasePageViewModel> Pages { get; } = new List<BasePageViewModel>();
        //internal void InternalReturn()
        //{
        //    if(Pages.Count > 1)
        //    {
        //        Pages.Pop();
        //        Content = Pages.Peek();
        //    }
        //}

        internal void InternalShow(BasePageViewModel page)
        {
            SelectedItem = page;
        }

        //public static void Return()
        //{
        //    if(Application.Current.MainWindow.DataContext is MainWindowViewModel viewmodel)
        //    {
        //        viewmodel.InternalReturn();
        //    }
        //}
        public static void Show(BasePageViewModel page)
        {
            if (Application.Current.MainWindow.DataContext is MainWindowViewModel viewmodel)
            {
                foreach (var p in viewmodel.Pages)
                {
                    if (page == p)
                    {
                        viewmodel.InternalShow(page);
                        break;
                    }
                }
            }
        }
        public static void Show<T>()
        {
           
            if (Application.Current.MainWindow.DataContext is MainWindowViewModel viewmodel)
            {
                foreach (var page in viewmodel.Pages)
                {
                    if (typeof(T) == page.GetType())
                    {
                        viewmodel.InternalShow(page);
                        break;
                    }
                }
            }
        }

        public static T? GetViewModel<T>() where T : BasePageViewModel
        {
            if (Application.Current.MainWindow.DataContext is MainWindowViewModel viewmodel)
            {
                foreach (var page in viewmodel.Pages)
                {
                    if (typeof(T) == page.GetType())
                    {
                        return page as T;
                    }
                }
            }
            return null;
        }
    }
}
