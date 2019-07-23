using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Program
    {
        public int NodeNum = 0;

        static void Main(string[] args)
        {
//            SaveByNode();
            SaveBySide();
        }
        private static void SaveBySide()
        {
            List<string> strs = new List<string>()
            {
                "0,1|2",
                "1,3|4",
                "2,0",
                "3,2",
                "4"
            };
            Dictionary<int, Node2> itemUse = new Dictionary<int, Node2>();
            //        0
            //        1
            //        2
            //        3
            //        4
            //算邻表

            for (int i = 0; i < strs.Count; i++)
            {
                string[] ss = strs[i].Split(',');
                int id = int.Parse(ss[0]);

                Node2 item;
                if (!itemUse.ContainsKey(id))
                {
                    item = new Node2 {id = id};
                    itemUse.Add(id, item);
                }
                else
                {
                    item = itemUse[id];
                }
                if (ss.Length > 1)
                {
                    string[] s = ss[1].Split('|');
                    Side temp = null;
                    for (int j = 0; j < s.Length; j++)
                    {
                        int getId = int.Parse(s[j]);

                        if (!itemUse.ContainsKey(getId))// 逗号","后面的节点，如果itemUse没有则添加
                        {
                            Node2 newItem = new Node2 { id = getId };
                            itemUse.Add(getId, newItem);
                        }

                        Side side = new Side
                        {
                            StartId = item.id,
                            EndId = getId
                        };
                        if (temp == null)
                        {
                            item.Right = side;
                            temp = side;
                        }
                        else
                        {
                            temp.Right = side;
                            temp = temp.Right;
                        }
                    }
                }
            }
            //            0->0-1->0-2
            //            1->1-3->1-4
            //            2->2-0
            //            3->3-2
            //            4


            //算逆邻表 
            //如上此时已经有了右半部分还需要左半部分 
            foreach (var item in itemUse)
            {
                //找id的逆邻
                int id = item.Key;
                Node2 node = item.Value;
                Side temp = null;
                foreach (var item2 in itemUse)//遍历所有右半部分边
                {
                    Side side = item2.Value.Right;//以边为节点的逆邻表不需要创建新节点
                    while (side != null)
                    {
                        if (side.EndId == id)//有一个边的id == 要寻找的id
                        {
                            if (temp == null)
                            {
                                node.Left = side;
                                temp = side;
                            }
                            else
                            {
                                temp.Left = side;
                                temp = temp.Left;//左移
                            }
                        }
                        side = side.Right;
                    }
                }
            }


            Console.WriteLine(Node2.Node2Num + "个节点 " + Side.SideNum + "条边");
            foreach (var item in itemUse)
            {
                Node2 n = item.Value;
                Console.Write(n.id);
                Side s = n.Right;
                while (s != null)
                {
                    Console.Write("->");
                    Console.Write(s.StartId+"-"+s.EndId);
                    s = s.Right;
                }
                Console.Write("\n");
            }

            foreach (var item in itemUse)
            {
                Node2 n = item.Value;
                Console.Write(n.id);
                Side s = n.Left;
                while (s != null)
                {
                    Console.Write("->");
                    Console.Write(s.StartId+"-"+s.EndId);
                    s = s.Left;
                }
                Console.Write("\n");
            }

        }

        private static void SaveByNode()
        {
            List<string> strs = new List<string>()
            {
                "0,1|2",
                "1,3|4",
                "2,0",
                "3,2",
            };



            Dictionary<int, Node> itemUse = new Dictionary<int, Node>();
            //        0
            //        1
            //        2
            //        3
            //        4
            //邻表
            for (int i = 0; i < strs.Count; i++)
            {
                string[] ss = strs[i].Split(',');
                int id = int.Parse(ss[0]);

                Node item;
                if (!itemUse.ContainsKey(id))
                {
                    item = new Node {id = id};
                    itemUse.Add(id, item);
                }
                else
                {
                    item = itemUse[id];
                }

                if (ss.Length > 1)
                {
                    string[] s = ss[1].Split('|');
                    for (int j = 0; j < s.Length; j++)
                    {
                        int getId = int.Parse(s[j]);

                        if (!itemUse.ContainsKey(getId))
                        {
                            var n = new Node { id = getId };
                            itemUse.Add(getId, n);
                        }

                        Node node = new Node {id = getId};
                        item.Right = node;
                        item = node;
                    }
                }
            }
            //            0->2->1
            //            1->3->4
            //            2->0
            //            3->2
            //            4

            //逆邻表
            //如上此时已经有了右半部分还需要左半部分
            foreach (var item in itemUse)
            {
                //找id的逆邻
                int id = item.Key;
                Node node = item.Value;
                foreach (var item2 in itemUse)//遍历所有右半部分(不包括主支)
                {

                    Node node2 = item2.Value.Right;
                    while (node2 != null)
                    {
                        if (node2.id == id)//有一个节点id == 要寻找的id
                        {
                            Node n = new Node();
                            n.id = item2.Key;//
                            node.Left = n;//添加节点

                            node = node.Left; //左移
                        }
                        node2 = node2.Right;
                    }
                }
            }
            //     2<-0->2->1
            //     0<-1->3->4
            //  3<-0<-2->0
            //     1<-3->2
            //     1<-4
            Console.WriteLine(Node.NodeNum+ "个节点");
            foreach (var item in itemUse)
            {
                Node n = item.Value;
                Console.Write(n.id);
                while (n.Right != null)
                {
                    Console.Write("->");
                    n = n.Right;
                    Console.Write(n.id);
                }
                Console.Write("\n");
            }
            foreach (var item in itemUse)
            {
                Node n = item.Value;
                Console.Write(n.id);
                while (n.Left != null)
                {
                    Console.Write("->");
                    n = n.Left;
                    Console.Write(n.id);
                }
                Console.Write("\n");
            }
        }
    }

    class Node
    {
        public static int NodeNum =0 ;
        public Node()
        {
            NodeNum++;
        }
        public int id;
        public Node Left;
        public Node Right;
    }



    class Node2
    {
        public static int Node2Num = 0;
        public Node2()
        {
            Node2Num++;
        }
        public int id;
        public Side Right;
        public Side Left;
    }
    class Side
    {
        public static int SideNum = 0;
        public Side()
        {
            SideNum++;
        }

        public int StartId;
        public int EndId;
        public Side Right;
        public Side Left;
    }
}
