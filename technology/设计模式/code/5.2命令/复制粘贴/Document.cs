using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApplication1
{
    public class Document
    {
        public string name;
        private FileStream fs;

        public void Open(string name)
        {
            this.name = name;
            fs = new FileStream(name,FileMode.Create);
        }

        public void Paste(string str)
        {
            byte [] data =new UTF8Encoding().GetBytes(str);
            fs.Write(data,0,data.Length);
        }

        public void Close()
        {
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }
    }
}
