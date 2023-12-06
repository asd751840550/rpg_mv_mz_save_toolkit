﻿using rpg_save_toolkit.UI.ViewModels;
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
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage() 
        {
            InitializeComponent();
        }

        public HomePage(HomePageViewModel viewmodel)
        {
            InitializeComponent();
            this.DataContext = viewmodel;
        }


    }
}
