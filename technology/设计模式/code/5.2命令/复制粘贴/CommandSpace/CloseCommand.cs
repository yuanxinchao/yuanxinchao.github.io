using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class CloseCommand:Command
    {
        private Document _document;
        private Application _ap;
        public CloseCommand(Document d,Application ap)
        {
            _document = d;
            _ap = ap;
        }

        public override void Excute()
        {
            if (_document != null)
            {
                _document.Close();
                _ap.Remove(_document);
            }

        }

    }
}
