using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //选择框
    public class ListBox:Widget
    {
        private List<string> _list = new List<string>();
        private int selectIndex;
        public ListBox(WidgetChange d)
        {
            widgetChange = d;
        }

        public string GetSelection()
        {
            if (selectIndex < _list.Count)
            {
                return _list[selectIndex];
            }

            return string.Empty;
        }
        public void SetList(List<string> _listItems)
        {
            _list = _listItems;
        }
        public override void HandleMouse(MouseEvent mouseEvent)
        {
            selectIndex = 1;
        }
    }
}
