using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.UI.ViewModels.Controls
{
    public partial class JsonObjectTreeTitleViewModel : ObservableObject
    {
        public JsonObjectTreeTitleViewModel() { }
        public JsonObjectTreeTitleViewModel(JToken src)
        {
            if(src == null)
            {
                return;
            }
            Root = src;
            
            var tmpProperty = src as JProperty;
            if (tmpProperty == null)
            {
                if (src == src.Root)
                {
                    Title = "root";
                }
                else if (src as JArray != null)
                {
                    Title = src.Path.Split('.').Last();
                }
                else if (src as JObject != null)
                {
                    Title = src.Path.Split('.').Last();
                }
                else
                {
                    ;
                }
            }
            else
            {
                Title = string.IsNullOrEmpty(tmpProperty.Name) ?
                    tmpProperty.Path
                    : tmpProperty.Name;
                if(src.Count() == 1)
                {
                    var firstChild = src.Children().First();
                    if (firstChild as JProperty == null && firstChild != null)
                    {
                        src = src.Children().First();
                    }
                }
            }
            foreach (var item in src)
            {
                if(!item.HasValues)
                {
                    continue;
                }
                ChildTitles.Add(new JsonObjectTreeTitleViewModel(item));
            }
            Description = src.Path;
        }
        [ObservableProperty]
        private string _title = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;

        public JToken? Root { get; set; }
        public ObservableCollection<JsonObjectTreeTitleViewModel> ChildTitles 
        { get; } = new ObservableCollection<JsonObjectTreeTitleViewModel>();
        //public ObservableCollection<JsonObjectTreeItemViewModel> Items 
        //{ get; } = new ObservableCollection<JsonObjectTreeItemViewModel>();
    }
}
