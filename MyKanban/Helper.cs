using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MyKanban
{
    public static class Helper
    {
        public static List<string> Glyphs;


        static Helper()
        {
            FontFamily family;
            GlyphTypeface glyph;
            IDictionary<int, ushort> characterMap;

            Glyphs = new List<string>();

            family = new FontFamily("Segoe MDL2 Assets");
            foreach (Typeface typeface in family.GetTypefaces())
            {
                if (!typeface.TryGetGlyphTypeface(out glyph)) continue;

                characterMap = glyph.CharacterToGlyphMap;

                foreach (KeyValuePair<int, ushort> kvp in characterMap)
                {
                    Glyphs.Add(((char)kvp.Key).ToString());
                }


            }
        }


    }
}
