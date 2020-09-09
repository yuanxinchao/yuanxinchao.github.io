using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class OpenCommand:Command
    {
        private string _name;
        private Application _ap;
        public OpenCommand(Application ap, string name)
        {
            _ap = ap;
            _name = name;
        }

        public override void Excute()
        {
            if (!string.IsNullOrEmpty(_name))
            {
                Document document = new Document();
                _ap.Add(document);
                document.Open(_name);
            }
        }
    }
}
