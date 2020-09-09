using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Button:Widget
    {
        private string text;
        public Button(DialogDirector d)
        {
            _director = d;
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public string GetText()
        {
            return text;
        }

        public override void HandleMouse(MouseEvent mouseEvent)
        {
            //....
            Change();
        }
    }
}
