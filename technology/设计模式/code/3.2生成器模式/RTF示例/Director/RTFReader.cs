using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Builder;

namespace ConsoleApplication1.Director
{
    public class RTFReader
    {
        private TextConverter _builder;


        public RTFReader(TextConverter builder)
        {
            _builder = builder;
        }
        public void ParseRTF(string s)
        {
            List<Unit> _list = ParseToUnits(s);
            for (int i = 0; i < _list.Count; i++)
            {
                var u = _list[i];
                if (u.type == typeof(char))
                {
                    _builder.ConverCharacter(u.c);
                }
                if (u.type == typeof(Paragraph))
                {
                    _builder.ConverFontChange(u.font);
                }
                if (u.type == typeof(Paragraph))
                {
                    _builder.ConverParagraph();
                }
            }
        }

        private List<Unit> ParseToUnits(string s)
        {
            return null;
        }
    }
}

public class Unit
{
    public string s;
    public char c;
    public Font font;
    public Type type;
}

