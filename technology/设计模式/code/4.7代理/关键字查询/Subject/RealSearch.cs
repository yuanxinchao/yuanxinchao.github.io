using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class RealSearch : Searchable {
    public RealSearch() {
    }
 
    /**
     * 真实的查询
     */
    public string search(string keyword) {
        return "真正的查询: SELECT * FROM users WHERE keyword = " + keyword;
    }
    }
}
