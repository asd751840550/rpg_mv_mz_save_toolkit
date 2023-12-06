using rpg_save_toolkit.UI.Enums;
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

namespace rpg_save_toolkit.UI.CustomControls
{
    /// <summary>
    /// KMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class KMessageBox : UserControl
    {
        public readonly static DependencyProperty MsgLevelProperty;
        public readonly static DependencyProperty TextProperty;

        static KMessageBox()
        {
            MsgLevelProperty = DependencyProperty.Register("MsgLevel", typeof(EMsgLevel), typeof(BlankElement)
                , new PropertyMetadata(EMsgLevel.Info));
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BlankElement)
               , new PropertyMetadata(string.Empty));
        }

        public KMessageBox(EMsgLevel level, string text)
        {
            InitializeComponent();
            MsgLevel = level;
            Text = text;
        }

        public EMsgLevel MsgLevel
        {
            get => (EMsgLevel)GetValue(MsgLevelProperty);
            set
            {
                SetValue(MsgLevelProperty, value);
            }
        }
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BlankElement.CloseDialog(true, this);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BlankElement.CloseDialog(false, this);
        }
    }
}
