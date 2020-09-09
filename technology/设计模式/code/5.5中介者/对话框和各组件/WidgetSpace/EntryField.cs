using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class EntryField:Widget
    {
        private string text;
        public EntryField(DialogDirector d)
        {
            
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
        }
    }
}
