using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.GlyphSpace;

namespace ConsoleApplication1.VisitorSpace
{
    public class SpellingCheckingVisitor:Visitor
    {
        
        private const int MAX_WORD_SIZE = 1024;

        private List<char> _currentWord = new List<char>();
        private List<List<char>> _misspellings = new List<List<char>>();
        public override void VisitCharacter(Character c)
        {
            var ch = c.GetCharCode();
            if (IsAlpha(ch))
            {
                //append alphabetic character to currentWord
                _currentWord.Add(ch);
            }
            else
            {
                //遇到了一个非字母，这时候可以检查拼写
                if (IsMisspelled(_currentWord))
                {
                    _misspellings.Add(_currentWord);
                }
                //重置_currentWord
                _currentWord = new List<char>();
            }

        }

        public override void VisitRow(Row r)
        {
        }

        public override void VisitImage(Image I)
        {
        }

        private bool IsAlpha(char c)
        {
            if (c > 'A' && c < 'z')
                return true;
            return false;
        }
        protected virtual bool IsMisspelled(List<char> s)
        {
            return false;
        }
    }
}
