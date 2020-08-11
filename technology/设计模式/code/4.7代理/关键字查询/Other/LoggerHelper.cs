using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class LoggerHelper {
        /**
         * 记录日志
         */
        public static void log(String keyword) {
            Console.WriteLine("模拟log4j记录日志 >> info >> login >> keyword: [" + keyword + "]");
        }
    }
}
