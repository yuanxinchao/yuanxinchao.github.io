using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //相当于一个抽象父类
    public class HelpHandler
    {
        private HelpHandler _successor;//后继者
        private Topic _topic;

        protected HelpHandler(HelpHandler h, Topic t = Topic.Undefine)
        {
            _successor = h;
            _topic = t;
        }

        public bool HasHelp()
        {
            return _topic != Topic.Undefine;
        }

        public virtual void HandleHelp()
        {
            if(_successor != null)
                _successor.HandleHelp();
        }
        
        public void SetHandle(HelpHandler w, Topic t = Topic.Undefine)
        {
            _successor = w;
            _topic = t;
        }


        //封装参数的使用方式
        public virtual void HandleRequest(Request request)
        {
            var t = request.GetType().ToString();
            switch (t)
            {
                case "PrintRequest":
                    HandlePrint((PrintRequest) request);
                    break;
                case "HelpRequest":
                    break;
            }
        }

        public virtual void HandlePrint(PrintRequest request)
        {
        }
    }

    public enum Topic
    {
        Undefine = -1,
        Button =1,
        Dialog = 10,
        Applition = 100,
    }

    public abstract class Request
    {

    }


    public class HelpRequest : Request
    {

    }

    public class PrintRequest : Request
    {

    }
}
