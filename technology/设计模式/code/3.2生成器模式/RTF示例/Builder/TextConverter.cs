using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Builder
{
    public abstract class TextConverter
    {
        public virtual void ConverCharacter(char c){}
        public virtual void ConverFontChange(Font f){}
        public virtual void ConverParagraph(){}

    }
    public class ASCIIConverter:TextConverter
    {
        private ASCIIText _text;

        public override void ConverCharacter(char c)
        {
            _text.Set(c);
        }

        public ASCIIText GetASCIIText()
        {
            return _text;
        }
    }
    public class TeXConverter:TextConverter
    {
        private TexText _texText;
        public override void ConverFontChange(Font f)
        {

        }

        public override void ConverCharacter(char c)
        {
            base.ConverCharacter(c);
        }

        public override void ConverParagraph()
        {
            base.ConverParagraph();
        }

        public TexText GetTexText()
        {
            return _texText;
        }
    }
    public class TextWidgetConverter:TextConverter
    {
        private TextWidget _textWidget;
        public override void ConverParagraph()
        {

        }

        public TextWidget GetTextWidget()
        {
            return _textWidget;
        }

        public Widget GetWidget()
        {
            return new Widget();
        }
    }

    public class ASCIIText
    {
        public ASCIIText(char c)
        {

        }

        public void Set(char c)
        {
        }
    }
    public class TexText
    {

    }
    public class TextWidget
    {
        private Widget _widget;
    }

    public class Widget
    {

    }
    public class Font
    {

    }

    public class Paragraph
    {

    }

}
