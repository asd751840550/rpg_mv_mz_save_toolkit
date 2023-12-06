using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using rpg_save_toolkit.Util.Assists;
using rpg_save_toolkit.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.Util.SaveFileAssist
{
    public class RpgMV : IRpg
    {
        public string Filter { get; set; } = "RPGMV(*.rpgsave)|*.rpgsave";

        public string? ReadFileToJson(string filename)
        {
            string? ret = null;

            var fileContent = File.ReadAllText(filename, Encoding.UTF8);
            ret = LZStringAssist.DecompressionFromBase64(fileContent);
            return ret;
        }

        public JToken? ReadFileToObject(string filename)
        {
            JToken? ret = null;
            var fileContent = File.ReadAllText(filename, Encoding.UTF8);
            var tmpJson = LZStringAssist.DecompressionFromBase64(fileContent);
            if(string.IsNullOrEmpty(tmpJson))
            {
                goto END;
            }
            ret = JToken.Parse(tmpJson);
        END:
            return ret;
        }

        public void WriteFileFromObject(string filename, JToken obj)
        {
            string tmpJson = obj.ToString(Formatting.None);
            File.WriteAllText(filename, LZStringAssist.CompressionToBase64(tmpJson));
        }

        public void WrteFileFromJson(string filename, string json)
        {
            File.WriteAllText(filename, LZStringAssist.CompressionToBase64(json));
        }
    }
}
