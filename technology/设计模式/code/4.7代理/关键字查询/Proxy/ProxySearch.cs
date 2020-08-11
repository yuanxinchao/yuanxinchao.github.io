using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ProxySearch : Searchable 
    {
    private RealSearch searcher;
 
    public ProxySearch() {
        searcher = new RealSearch();
    }
 
    /**
     * 查询前的授权操作
     */
    private bool check(String keyword) {
        if (ValidatorHelper.validate(keyword)) {
            Console.WriteLine("不是敏感词:" + keyword);
            return true;
        } else {
            Console.WriteLine("是敏感词:" + keyword);
            return false;
        }
    }
 
    /**
     * 查询操作模板
     */
    public String search(String keyword) {
        if (check(keyword)) {
            // 不是敏感词, 那就予以查询处理
            String result = searcher.search(keyword);
 
            // 查询成功再打一条日志记录一下
            LoggerHelper.log(keyword);
 
            // 返回查询结果
            return result;
        } else {
            // 是敏感词, 那么就无法被正常处理.
            return null;
        }
    }
    }
}
