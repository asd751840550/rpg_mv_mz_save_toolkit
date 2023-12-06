using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using rpg_save_toolkit.UI.Commands;
using rpg_save_toolkit.UI.CustomControls;
using rpg_save_toolkit.UI.Enums;
using rpg_save_toolkit.UI.ViewModels.Controls;
using rpg_save_toolkit.Util.SaveFileAssist;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace rpg_save_toolkit.UI.ViewModels
{
    public partial class EditPageViewModel : BasePageViewModel
    {
        private IRpg? _rpgAssist = null;
        public EditPageViewModel()
        {
            Title = "Not load file";
        }

        [ObservableProperty]
        private JsonObjectTreeTitleViewModel? _selectedItem;
        [ObservableProperty]
        private string? _selectedItemJson;
        [ObservableProperty]
        private string? _searchText;
        public ObservableCollection<JsonObjectTreeTitleViewModel> JObjectTrees
        { get; } = new ObservableCollection<JsonObjectTreeTitleViewModel>();

        public ObservableCollection<JsonObjectTreeItemViewModel> JsonObjectsItems
        { get; } = new ObservableCollection<JsonObjectTreeItemViewModel>();

        public ICommand SearchCommand { get; } = new CustomCommand((p) =>
        {
            return true;
        }, 
        (p) =>
        {
            if(p is FrameworkElement fe && fe.DataContext is EditPageViewModel viewmodel)
            {
                viewmodel.BtnSearch_Clicked(fe, new RoutedEventArgs());
            }
        });

        public void Load(JObject src)
        {
            if(src == null)
            {
                return;
            }
            JObjectTrees.Clear();
            JObjectTrees.Add(new JsonObjectTreeTitleViewModel(src));
        }
        public bool Load(string filePath)
        {
            _rpgAssist = null;
            JToken? jsrc = null;
            string? tmpTitle = Title;
            if (filePath.EndsWith(".rpgsave"))
            {
                _rpgAssist = new RpgMV();
                tmpTitle = "Rpg MV";
            }
            else if (filePath.EndsWith(".rmmzsave"))
            {
                _rpgAssist = new RpgMZ();
                tmpTitle = "Rpg MZ";
            }
            if(_rpgAssist != null)
            {
                jsrc = _rpgAssist.ReadFileToObject(filePath);
                if(jsrc != null)
                {
                    Title = tmpTitle;
                    JObjectTrees.Clear();
                    JObjectTrees.Add(new JsonObjectTreeTitleViewModel(jsrc));
                }
            }
            return jsrc != null;
        }

        public bool Save(string filePath, JToken root)
        {
            bool ret = false;
            if(string.IsNullOrEmpty(filePath) || root == null || _rpgAssist == null)
            {
                goto END;
            }
            _rpgAssist.WriteFileFromObject(filePath, root);
            ret = true;
        END:
            return ret;
        }

        public void BtnExit_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel.Show<HomePageViewModel>();
        }

        public async void BtnLoad_Clicked(object sender, RoutedEventArgs e)
        {
            Window? window = Window.GetWindow(sender as DependencyObject);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Please select rpg save file.";
            openFileDialog.Filter = "RPGMV/MZ(*.rpgsave;*.rmmzsave)|*.rpgsave;*.rmmzsave|RPGMV(*.rpgsave)|*.rpgsave|RPGMZ(*.rmmzsave)|*.rmmzsave";
            if (!(openFileDialog.ShowDialog() ?? false))
            {
                //await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Warning, "Please select a file."));
                return;
            }
            if (!Load(openFileDialog.FileName))
            {
                await BlankElement.ShowDialog(window, "Not support file.");
            }
        }

        public async void BtnSave_Clicked(object sender, RoutedEventArgs e)
        {
            Window? window = Window.GetWindow(sender as DependencyObject);
            if (_rpgAssist == null || JObjectTrees.Count == 0)
            {
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Error, "Something went wrong, please reload file."));
                return;
            }
            SaveFileDialog saveFileDialog = new() { Filter = _rpgAssist.Filter 
            , Title = "Please select a file to save."};
            if(!(saveFileDialog.ShowDialog() ?? false))
            {
                //await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Warning, "Please select a file."));
                return;
            }
            if (!Save(saveFileDialog.FileName, JObjectTrees.First().Root!))
            {
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Error, "Save error."));
            }
        }

        public async void BtnSearch_Clicked(object sender, RoutedEventArgs e)
        {
            Window? window = Window.GetWindow(sender as DependencyObject);
            var root = JObjectTrees.FirstOrDefault();
            if (root == null)
            {
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Error, "Something went wrong, please reload file."));
                return;
            }
            if(string.IsNullOrEmpty(SearchText))
            {
                await BlankElement.ShowDialog(window, new KMessageBox(EMsgLevel.Error, "Please input search text."));
                return;
            }
            JsonObjectsItems.Clear();
            Stack<JsonObjectTreeTitleViewModel> tmpStack = new Stack<JsonObjectTreeTitleViewModel>();
            tmpStack.Push(root);
            while (tmpStack.Count > 0)
            {
                var tmpTitle = tmpStack.Pop();
                foreach (var item in tmpTitle.ChildTitles)
                {
                    if (item.ChildTitles.Count > 0)
                    {
                        tmpStack.Push(item);
                    }
                    else
                    {
                        var prop = item.Root! as JProperty;
                        if (prop != null && (prop.Name.Contains(SearchText) || (prop.Value?.ToString(Newtonsoft.Json.Formatting.None) ?? string.Empty).Contains(SearchText)))
                        {
                            JsonObjectsItems.Add(new JsonObjectTreeItemViewModel(item.Root!));
                        }
                    }
                }
            }
        }

        public void TreeJobject_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            JsonObjectsItems.Clear();
            if(e.NewValue is JsonObjectTreeTitleViewModel viewmodel)
            {
                SelectedItem = viewmodel;
                SelectedItemJson = viewmodel.Root!.ToString(Newtonsoft.Json.Formatting.Indented);
                RefreshItemList();
            }
            else
            {
                SelectedItem = null;
                SelectedItemJson = null;
            }
        }

        private void RefreshItemList()
        {
            if(SelectedItem != null)
            {
                if (SelectedItem.ChildTitles.Count > 0)
                {
                    Stack<JsonObjectTreeTitleViewModel> tmpStack = new Stack<JsonObjectTreeTitleViewModel>();
                    tmpStack.Push(SelectedItem);
                    while (tmpStack.Count > 0)
                    {
                        var tmpTitle = tmpStack.Pop();
                        foreach (var item in tmpTitle.ChildTitles)
                        {
                            if (item.ChildTitles.Count > 0)
                            {
                                tmpStack.Push(item);
                            }
                            else
                            {
                                JsonObjectsItems.Add(new JsonObjectTreeItemViewModel(item.Root!));
                            }
                        }
                    }
                }
                else
                {
                    JsonObjectsItems.Add(new JsonObjectTreeItemViewModel(SelectedItem.Root!));
                }
            }
        }
    }
}
