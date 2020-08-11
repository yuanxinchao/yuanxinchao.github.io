using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ExtendedHandler:HelpHandler
    {
        protected ExtendedHandler(HelpHandler h, Topic t = Topic.Undefine) : base(h, t)
        {
        }

        public override void HandleRequest(Request request)
        {
            var t = request.GetType().ToString();
            switch (t)
            {
                case "Preview":
                   //handle preview request
                    break;
                default:
                    base.HandleRequest(request);
                    break;
            }
        }
    }
}
