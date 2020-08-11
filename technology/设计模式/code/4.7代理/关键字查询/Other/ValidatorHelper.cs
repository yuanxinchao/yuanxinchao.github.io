using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ValidatorHelper {
        /**
         * 敏感词黑名单
         */
        private static HashSet<string> blackList = new HashSet<string>
        {
            "jack123",
            "json898",
            "nancy",

        };
 
        /**
         * 敏感词验证
         * 如果keyword在黑名单blackList里, 那么返回false
         * 如果keyword不在黑名单blackList里, 那么返回true
         */
        public static bool validate(string userId) {
            return !blackList.Contains(userId.Trim());
        }
    }
}
