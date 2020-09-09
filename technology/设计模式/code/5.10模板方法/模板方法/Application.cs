using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
     public abstract class Application
     {
        private List<Document> _docs;
        public void OpenDocument(string name)
        {
            if (!CanOpenDocument(name))
            {
                return;
            }

            Document doc = DoCreateDocument();
            if (doc != null)
            {
                _docs.Add(doc);
                AboutToOpenDocument(doc);
                doc.Open();
                doc.DoRead();
            }

        }

        protected abstract void AboutToOpenDocument(Document doc);

        protected abstract Document DoCreateDocument();

        protected abstract bool CanOpenDocument(string name);
    }

     public abstract class Document
     {
         public abstract void DoRead();

         public abstract void Open();
     }
}
