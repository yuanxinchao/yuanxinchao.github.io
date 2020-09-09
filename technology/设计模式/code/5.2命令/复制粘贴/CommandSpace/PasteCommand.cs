using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class PasteCommand:Command
    {
        private Application _application;
        private string _paste;
        private string _documentName;
        public PasteCommand(Application ap, string name,string paste)
        {
            _application = ap;
            _documentName = name;
            _paste = paste;
        }

        public override void Excute()
        {
            var doc = _application.GetDocument(_documentName);
            if (doc != null)
            {
                doc.Paste(_paste);

            }
        }
    }
}
