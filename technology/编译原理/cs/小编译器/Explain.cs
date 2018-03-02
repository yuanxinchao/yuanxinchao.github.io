using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Explain
{
    public static class Explain
    {
        private static string[] _tokens;
        //lexical analysis 词法分析
        public static string[] Tokenize(string text)
        {
            _tokens = text.Replace("(", " ( ").Replace(")", " ) ").Split(" \t\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return _tokens;
        }

        //生成抽象语法树
        public static SExpression ParseToTree(this string text)
        {
            SExpression current = new SExpression();
            foreach (var lex in Tokenize(text))
            {
                if (lex == "(")
                {
                    SExpression newNode = new SExpression { Parent = current,Value ="w"};
                    current.Childs.Add(newNode);
                    current = newNode;
                }
                else if (lex == ")")
                {
                    current = current.Parent;
                }
                else
                {
                    SExpression newNode = new SExpression { Parent = current, Value = lex };
                    current.Childs.Add(newNode);
                }
            }
            return current.Childs[0];
        }

        public static string ShowToken()
        {
            return _tokens.Aggregate(string.Empty, (current, token) => current + token + ",");
        }
    }

    public class SExpression
    {
        public string Value;
        public SExpression Parent;
        public List<SExpression> Childs = new List<SExpression>(3);
        /// <summary>
        /// 遍历所有子节点,广度优先
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ret = Value;
            Queue<SExpression> queue = new Queue<SExpression>(Childs);
            while (queue.Count> 0)
            {
                ret = ret + "下一层";
                int count = queue.Count;
                for (int i = 0; i < count; i++)
                {
                    SExpression top = queue.Dequeue();
                    ret = ret + top.Value + " ";
                    foreach (var child in top.Childs)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
            return ret;
        }

        public object Evaluate(SScope scope)
        {
            if (Childs.Count == 0) //最底层的节点
            {
                int num;
                if (int.TryParse(Value, out num))
                    return num;
                return scope.Find(Value);
            }
            SExpression first = Childs[0];
            if (first.Value == "if")
            {
                object condition = Childs[1].Evaluate(scope);
                if ((bool) condition)
                    return Childs[2].Evaluate(scope);
                return Childs[3].Evaluate(scope);
            }
            if (first.Value == "def")
            {
               return scope.Define(Childs[1].Value,Childs[2].Evaluate(new SScope(scope)));
            }
            if(SScope.BuiltinFunctions.ContainsKey(first.Value))
            {
                List<object> argument = Childs.Skip(1).Select(node => node.Evaluate(scope)).ToList();
                return SScope.BuiltinFunctions[first.Value](argument);

            }
            throw new Exception("cant find Value  " + first.Value);
        }
    }
//    Scope1 (dic<k,v>)
//    |
//    Scope2 (dic<k,v>)
//    |
//    Scope3 (dic<k,v>)
//    每一个层级的定义域都有一个对应key 和 value 的dic 对于下一层的key如果没有value对应就会向上查找
    //作用域
    public class SScope
    {
        public SScope Parent;
        public SScope(SScope scope)
        {
            Parent = scope;
        }
        public static readonly Dictionary<string, Func<List<object>, object>> BuiltinFunctions =
        new Dictionary<string, Func<List<object>, object>>();

        public Dictionary<string,object> VariableTable = new Dictionary<string, object>(3); 

        public SScope BuildIn(string name, Func<List<object>, object> builtinFuntion)
        {
            BuiltinFunctions.Add(name, builtinFuntion);
            return this;
        }

        public object Define(string name, object value)
        {
            if (VariableTable.ContainsKey(name))
                VariableTable[name] = value;
            else
                VariableTable.Add(name, value);
            return value;
        }

        public object Find(string name)
        {
            SScope current = this;
            while (current!=null)
            {
                if (current.VariableTable.ContainsKey(name))
                    return current.VariableTable[name];
                current = current.Parent;
            }
            throw new Exception("find no Variable " + name);
        }

        public object Evaluate(string texts)
        {
            return texts.ParseToTree().Evaluate(this);
        }

        public SScope Init()
        {
            BuildIn("+", list => list.Aggregate((current, item) => (int)current + (int)item))
                .BuildIn("-", list => list.Aggregate((current, item) => (int)current - (int)item))
                .BuildIn("*", list => list.Aggregate((current, item) => (int)current * (int)item))
                .BuildIn("/", list =>
                {
                    return list.Aggregate((current, item) =>
                    {
                        if ((int)item == 0)
                            throw new Exception("b should not 0");
                        return (int)current / (int)item;
                    });
                })
                .BuildIn("==", list => (int)list[0] == (int)list[1])
                .BuildIn("<", list => (int)list[0] < (int)list[1])
                .BuildIn(">", list => (int)list[0] > (int)list[1])
                .BuildIn("&&", list => list.All(v => (bool)v))
                .BuildIn("||", list => list.Any(v=>(bool)v==false))
                .BuildIn("!", list => !(bool)list[0]);
            return this;
        }
    }
}