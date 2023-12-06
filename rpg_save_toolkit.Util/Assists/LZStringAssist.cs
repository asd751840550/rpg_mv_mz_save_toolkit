using LZStringCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg_save_toolkit.Util.Assists
{
    public class LZStringAssist
    {
        public static string? CompressionToBase64(string json)
        {
            return LZString.CompressToBase64(json);
        }

        public static string? DecompressionFromBase64(string lzstring)
        {
            return LZString.DecompressFromBase64(lzstring);
        }
    }
}
