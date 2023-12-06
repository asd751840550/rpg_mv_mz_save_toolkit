using Newtonsoft.Json.Linq;
using rpg_save_toolkit.Util.Assists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.Util.SaveFileAssist
{
    public class RpgMZ : IRpg
    {
        public string Filter { get; set; } = "RPGMZ(*.rmmzsave)|*.rmmzsave";
        public RpgMZ() 
        {
            JsPakoAssist.Instance.Init();
        }

        ~RpgMZ()
        {
            JsPakoAssist.Instance.Dispose();
        }

        public string? ReadFileToJson(string filename)
        {
            string? ret = null;
            ret = JsPakoAssist.Instance.DeCompressionJson(File.ReadAllBytes(filename));
            return ret;
        }

        public JToken? ReadFileToObject(string filename)
        {
            JToken? ret = null;
            var tmpJson = JsPakoAssist.Instance.DeCompressionJson(File.ReadAllBytes(filename));
            if(tmpJson != null)
            {
                ret = JToken.Parse(tmpJson);
            }
            return ret;
        }

        public void WriteFileFromObject(string filename, JToken obj)
        {
            if(obj == null)
            {
                return;
            }
            var tmpZipData = JsPakoAssist.Instance.CompressionJson(obj.ToString(Newtonsoft.Json.Formatting.None));
            if(tmpZipData != null)
            {
                File.WriteAllBytes(filename, tmpZipData);
            }
        }

        public void WrteFileFromJson(string filename, string json)
        {
            if(string.IsNullOrEmpty(json))
            {
                return;
            }
            var tmpZipData = JsPakoAssist.Instance.CompressionJson(json);
            if (tmpZipData != null)
            {
                File.WriteAllBytes(filename, tmpZipData);
            }
        }
    }
}
