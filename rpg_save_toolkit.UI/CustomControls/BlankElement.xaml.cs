using rpg_save_toolkit.UI.Assists;
using rpg_save_toolkit.UI.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace rpg_save_toolkit.UI.CustomControls
{
    /// <summary>
    /// BlankElement.xaml 的交互逻辑
    /// </summary>
    public partial class BlankElement : UserControl, IDisposable
    {
        public readonly static DependencyProperty IsOpenProperty;
        public readonly static DependencyProperty ShowContentProperty;
        static BlankElement()
        {
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(BlankElement)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsOpenPropertyChangedCallback));
            ShowContentProperty = DependencyProperty.Register("ShowContent", typeof(object), typeof(BlankElement)
                , new PropertyMetadata(null));
        }

        public async static Task<object?> ShowDialog(Window window, object content)
        {
            BlankElement tmpBlankElement = new(window);
            return await tmpBlankElement.ShowInternal(content);
        }

        public static void CloseDialog(object? parameter, IInputElement input)
        {
            var blankElement = ControlAssist.GetParentObject<BlankElement>(input as DependencyObject);
            blankElement?.CloseInternal(parameter);
        }
        private static void IsOpenPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            BlankElement blankElemt = (BlankElement)dependencyObject;

            
            if (!blankElemt.IsOpen)
            {
                blankElemt._dialogTaskCompletionSource?.TrySetResult(blankElemt.CloseParameter);
                blankElemt?.Dispose();
            }
            //dialogHost.Dispatcher.InvokeAsync(() => dialogHost._restoreFocusDialogClose?.Focus(), DispatcherPriority.Input);
        }

        public object ShowContent
        {
            get => GetValue(ShowContentProperty);
            set
            {
                SetValue(ShowContentProperty, value);
            }
        }
        public bool IsOpen
        {
            get
            {
                return (bool)GetValue(IsOpenProperty);
            }
            set
            {
                SetValue(IsOpenProperty, value);
            }
        }
        public ICommand ExitCommand { get; } = new CustomCommand((p) => { return true; }, (p) =>
        {
            if (p is BlankElement be)
            {
                be.Dispose();
            }
        });
        internal async Task<object?> ShowInternal(object content)
        {
            ShowContent = content;
            _dialogTaskCompletionSource = new TaskCompletionSource<object?>();
            SetCurrentValue(IsOpenProperty, true);

            return await _dialogTaskCompletionSource!.Task;
        }
        internal void CloseInternal(object? param)
        {
            CloseParameter = param;
            SetCurrentValue(IsOpenProperty, false);
        }

        public object? CloseParameter { get; set; } = null;
        private TaskCompletionSource<object?>? _dialogTaskCompletionSource;
        private Window? _owner = null;
        private FrameworkElement? _parentContent = null;
        private Grid _mainGrid = new();
        private Point _mousePoint = new();

        public BlankElement(Window parent)
        {
            InitializeComponent();
            this.Width = double.NaN;
            this.Height = double.NaN;
            if (parent.Content is FrameworkElement fe)
            {
                _owner = parent;
                _parentContent = fe;
            }
            if (_parentContent != null && _owner != null)
            {
                _owner.Content = null;
                _mainGrid.Children.Add(_parentContent);
                _mainGrid.Children.Add(this);
                Grid.SetZIndex(this, 999);
                _owner.Content = _mainGrid;
            }
        }

        public void Dispose()
        {
            if (_parentContent != null && _owner != null)
            {
                _owner.Content = null;
                _mainGrid.Children.Clear();
                _owner.Content = _parentContent;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _mousePoint = e.GetPosition(this);
        }

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            Border? border = sender as Border;
            if (border != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point tmpPoint = e.GetPosition(this);
                double tmpX = tmpPoint.X - _mousePoint.X;
                double tmpY = tmpPoint.Y - _mousePoint.Y;
                border.Margin = new Thickness(border.Margin.Left + tmpX, border.Margin.Top + tmpY, border.Margin.Right, border.Margin.Bottom);
                _mousePoint = tmpPoint;
            }
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            if(sender is Border border)
            {
                var parent = border.Parent as Canvas;
                if(parent != null)
                {
                    border.Margin = new Thickness((parent.ActualWidth - border.ActualWidth) / 2, (parent.ActualHeight - border.ActualHeight) / 2, border.Margin.Right, border.Margin.Bottom);
                }
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(sender is Canvas canvas)
            {
                if(canvas.Children.Count > 0 && canvas.Children[0] is Border border)
                {
                    if (e.PreviousSize.Width == 0 || e.PreviousSize.Height == 0 || border.Margin.Left == 0 || border.Margin.Top == 0)
                    {
                        return;
                    }
                    double coffX = e.PreviousSize.Width / border.Margin.Left;
                    double coffY = e.PreviousSize.Height / border.Margin.Top;
                    double x = e.NewSize.Width - e.PreviousSize.Width;
                    double y = e.NewSize.Height - e.PreviousSize.Height;
                    border.Margin = new Thickness(Math.Min(Math.Max(0, e.NewSize.Width / coffX), canvas.ActualWidth - border.ActualWidth), Math.Min(Math.Max(0, e.NewSize.Height / coffY), canvas.ActualHeight - border.ActualHeight), border.Margin.Right, border.Margin.Bottom);
                }
            }
        }
    }
}
