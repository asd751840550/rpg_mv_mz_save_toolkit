using Newtonsoft.Json.Linq;
using rpg_save_toolkit.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.Util.SaveFileAssist
{
    public interface IRpg
    {
        public string Filter { get; set; }
        //public string OpenFileToJson();
        //public KKeyValue OpenFileToObject();
        public string? ReadFileToJson(string filename);
        public JToken? ReadFileToObject(string filename);

        //public void SaveFileFromJson(string json);
        //public void SaveFileFromObject(KKeyValue obj);
        public void WrteFileFromJson(string filename, string json);
        public void WriteFileFromObject(string filename, JToken obj);
    }
}
