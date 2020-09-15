using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Application
    {
        private List<Document> _list = new List<Document>();
        public Document GetDocument(string savelogTxt)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].name == savelogTxt)
                    return _list[i];
            }

            return null;
        }

        public void Add(Document document)
        {
            _list.Add(document);
        }

        public void Remove(Document document)
        {
            _list.Remove(document);
        }
    }
}
