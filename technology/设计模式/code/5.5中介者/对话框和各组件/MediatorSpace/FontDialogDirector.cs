using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class FontDialogDirector:DialogDirector
    {
        private Button _ok;
        private Button _cancel;
        private ListBox _fontList;
        private EntryField _fontName;



        public FontDialogDirector()
        {

        }

        public override void CreateWidgets()
        {
            _ok = new Button(this);
            _cancel = new Button(this);
            _fontList = new ListBox(this);
            _fontName = new EntryField(this);
        }

        public override void WidgetChanged(Widget theChangeWidget)
        {
            if (theChangeWidget == _fontList)
            {
                //两个组件的通信放到了中介者里
                _fontName.SetText(_fontList.GetSelection());
            }
            else if(theChangeWidget == _ok)
            {
                //设置字体变化
                //...
            }else if (theChangeWidget == _cancel)
            {
                //关闭窗口
            }

        }
    }
}
