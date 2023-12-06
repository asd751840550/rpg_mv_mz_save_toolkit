using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.Util.Assists
{
    public class JsPakoAssist
    {
        public class JSPakoOptions { public string? to { get; set; } public int? level { get; set; } }

        public static readonly string PATH_SCRIPT_PAKO = Path.Combine(AppContext.BaseDirectory, "Scripts", "pako.min.js");
        public static JsPakoAssist Instance { get; } = new JsPakoAssist();

        public V8ScriptEngine? JSScriptEngine = null;

        public void Init()
        {
            Dispose();
            if (JSScriptEngine == null)
            {
                JSScriptEngine = new V8ScriptEngine();
            }            
            JSScriptEngine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
            JSScriptEngine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full; // 这两行是为了允许加载js文件
            JSScriptEngine.Execute(JSScriptEngine.CompileDocument(PATH_SCRIPT_PAKO));
        }

        public void Dispose()
        {
            JSScriptEngine?.Dispose();
            JSScriptEngine = null;
        }

        public byte[]? CompressionJson(string json)
        {
            byte[]? ret = null;
            if (string.IsNullOrEmpty(json))
            {
                return ret;
            }

            var zipdata = JSScriptEngine?.Script.pako.deflate(json, new JSPakoOptions() { to = "string", level = 1 });
            if (zipdata is string data)
            {
                ret = Encoding.UTF8.GetBytes(data);
            }
            return ret;
        }

        public string? DeCompressionJson(byte[] gzipData)
        {
            string? ret = null;
            if (gzipData == null || gzipData.Length == 0)
            {
                return ret;
            }
            var data = JSScriptEngine?.Script.pako.inflate(Encoding.UTF8.GetString(gzipData), new JSPakoOptions() { to = "string" });
            return data as string;
        }
    }
}
