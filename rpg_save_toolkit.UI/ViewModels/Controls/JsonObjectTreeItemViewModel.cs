using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.UI.ViewModels.Controls
{
    public partial class JsonObjectTreeItemViewModel : ObservableObject
    {
        public JsonObjectTreeItemViewModel(JToken src) 
        {
            Source = src;
            var tmpProperty = (src as JProperty);
            if (tmpProperty != null)
            {
                _path = tmpProperty.Path;
                _key = tmpProperty.Name;
                _value = tmpProperty.Value?.ToString(Newtonsoft.Json.Formatting.None) ?? string.Empty;
                _originValue = Value;
            }
            //var property = src.fi.Properties().FirstOrDefault();
            //if (property != null)
            //{
            //    Key = property.Name;
            //    Value = property.Value?.ToString() ?? string.Empty;
            //}

        }
        [ObservableProperty]
        private string _path = string.Empty;
        [ObservableProperty]
        private string _key = string.Empty;
        [ObservableProperty]
        private string _value = string.Empty;
        [ObservableProperty]
        private bool _isChanged = false;

        private string _originValue = string.Empty;
        partial void OnValueChanged(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                return;
            }
            var tmpProp = Source as JProperty;
            if (tmpProp != null)
            {
                var tmpJToken = JValue.Parse(value);
                if (tmpJToken != null)
                {
                   tmpProp.Value = tmpJToken;
                }
            }
            IsChanged = _originValue != Value;
        }
        
        public JToken? Source { get; set; }
    }
}
