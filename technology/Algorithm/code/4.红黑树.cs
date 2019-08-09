using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            SearchTree<int> s = new SearchTree<int>((a,b)=> a-b);
            var n1 = new Node<int>() {value = 5};
            var n2 = new Node<int>() {value = 2};
            var n3 = new Node<int>() {value = 3};
            var n4 = new Node<int>() {value = 9};
            var n5 = new Node<int>() {value = 1};
            var n6 = new Node<int>() {value = 6};
            var n7 = new Node<int>() {value = 10};
            var n8 = new Node<int>() {value = 4};
            var n9 = new Node<int>() {value = 12};

            s.RB_INSERT(n1);

            s.RB_INSERT(n2);
            s.RB_INSERT(n3);
            s.RB_INSERT(n4);
            s.RB_INSERT(n5);
            s.RB_INSERT(n6);
            s.RB_INSERT(n7);
            s.RB_INSERT(n8);
            s.RB_INSERT(n9);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();
            s.RB_DELETE(s.head);
            s.PrintHeap();



        }

        
    }

}

public class SearchTree<T> where T : IComparable
{
    public Node<T> head = Nil;

    private Comparison<T> compare;
    private const int RED = 0;
    private const int BLACK = 1;
    private static readonly Node<T> Nil = new Node<T>{color = BLACK};

    public SearchTree(Comparison<T> compare)
    {
        this.compare = compare;
    }


    //中序遍历(递归)
    public void INORDER_TREE_WALK1(Node<T> x)
    {
        if (x != Nil)
        {
            INORDER_TREE_WALK1(x.left);
            Print(x.value);
            INORDER_TREE_WALK1(x.right);
        }
    }
    //中序遍历2(栈)  原则上 首先循环压入左侧，然后每次压入新右侧都循环压入左侧
    public void INORDER_TREE_WALK2(Node<T> x)
    {
        Stack<Node<T>> stack = new Stack<Node<T>>();
        bool done =false;

        while (!done)
        {
            if (x != Nil)
            {
                stack.Push(x);
                x = x.left;
            }
            else
            {
                if (stack.Count > 0)
                {
                    x = stack.Pop();
                    Print(x.value);
                    x = x.right;
                }
                else
                    done = true;
            }
        }
    }
    //先序遍历
    public void PREORDER_TREE_WALK1(Node<T> x)
    {
        if (x != Nil)
        {
            Print(x.value);
            PREORDER_TREE_WALK1(x.left);
            PREORDER_TREE_WALK1(x.right);
        }
    }
    //后续遍历
    public void POSTORDER_TREE_WALK1(Node<T> x)
    {
        if (x != Nil)
        {
            POSTORDER_TREE_WALK1(x.left);
            POSTORDER_TREE_WALK1(x.right);
            Print(x.value);

        }
    }

    public Node<T> TREE_SEARCH(Node<T> node,T k)
    {
        if (node == Nil || compare(k, node.value) == 0)
        {
            return node;
        }
        if (compare(k,node.value) < 0)
        {
            return TREE_SEARCH(node.left,k);
        }
        return TREE_SEARCH(node.right, k);
    }

    public Node<T> ITERACTIVE_TREE_SEARCH(Node<T> node, T k)
    {
        while (node != Nil && compare(k, node.value) != 0)
        {
            if (compare(k, node.value) < 0)
            {
                node = node.left;
            }
            else
            {
                node = node.right;
            }
        }
        return node;
    }
    //最小值
    public Node<T> TREE_MINIMUM(Node<T> node)
    {
        while (node.left != Nil)
        {
            node = node.left;
        }
        return node;
    }

    public Node<T> TREE_MAXIMUM()
    {
       return TREE_MAXIMUM(head);
    }

    //最大值
    private Node<T> TREE_MAXIMUM(Node<T> x)
    {
        while (x.right != Nil)
        {
            x = x.right;
        }
        return x;
    } 
    //中序遍历 的某结点后继
    public Node<T> TREE_SUCCESSOR(Node<T> x)
    {
        if (x.right != Nil)
            return TREE_MINIMUM(x.right);

        var y = x.p;
        while (y != Nil && x == y.right)
        {
            x = y;
            y = y.p;
        }
        return y;
    } 
    //中序遍历 的某结点前驱
    public Node<T> TREE_PREDECESSOR(Node<T> x)
    {
        if (x.left != Nil)
            return TREE_MAXIMUM(x.left);

        var y = x.p;
        while (y != Nil && x == y.left)
        {
            x = y;
            y = y.p;
        }
        return y;
    }

    public void RB_INSERT(Node<T> z)
    {
        var y = Nil;
        var x = head;
        while (x != Nil)
        {
            y = x;
            if (compare(z.value, x.value) < 0)
                x = x.left;
            else
                x = x.right;
        }
        z.p = y;
        if (y == Nil)
        {
            head = z;
        }
        else if(compare(z.value,y.value) < 0)
        {
            y.left = z;
        }
        else
        {
            y.right = z;
        }
        z.left = Nil;
        z.right = Nil;
        z.color = RED;
        RB_INSERT_FIXUP(z);
    }

    public void RB_INSERT_FIXUP(Node<T> z)
    {
        while (z.p.color == RED)
        {
            if (z.p == z.p.p.left)
            {
                var y = z.p.p.right;
                if (y.color == RED)
                {
                    z.p.color = BLACK;
                    y.color = BLACK;
                    z.p.p.color = RED;
                    z = z.p.p;
                }
                else

                {
                    if (z == z.p.right)
                    {
                        z = z.p;
                        LEFT_ROTATE(z);


                    }

                    z.p.color = BLACK;
                    z.p.p.color = RED;
                    RIGHT_ROTATE(z.p.p);
                }
            }
            else if (z.p == z.p.p.right)
            {
                var y = z.p.p.left;
                if (y.color == RED)
                {
                    z.p.color = BLACK;
                    y.color = BLACK;
                    z.p.p.color = RED;
                    z = z.p.p;
                }
                else
                {
                    if (z == z.p.left)
                    {
                        z = z.p;
                        RIGHT_ROTATE(z);
                    }

                    z.p.color = BLACK;
                    z.p.p.color = RED;
                    LEFT_ROTATE(z.p.p);
                }
            }
        }
        head.color = BLACK;
    }

    // 红黑树 嫁接(删除节点中需要的一个操作)
    public void RB_TRANSPLANT(Node<T> u,Node<T> v)
    {
        if (u.p == Nil)
            head = v;
        else if (u == u.p.left)
            u.p.left = v;
        else if (u == u.p.right)
            u.p.right = v;
        
        v.p = u.p;
    }
    //红黑树 删除结点
    public void RB_DELETE(Node<T> z)
    {
        var y = z;
        Node<T> x;
        var y_original_color = y.color;
        if (z.left == Nil)
        {
            x = z.right;
            RB_TRANSPLANT(z,z.right);
        }
        else if (z.right == Nil)
        {
            x = z.left;
            RB_TRANSPLANT(z, z.left);
        }
        //有两孩子
        else
        {
            y = TREE_MINIMUM(z.right);
            y_original_color = y.color;
            x = y.right;
            if (y.p == z)
            {
                x.p = y;
            }
            else
            {
                RB_TRANSPLANT(y,y.right);
                y.right = z.right;
                y.right.p = y;
            }
            RB_TRANSPLANT(z,y);
            y.left = z.left;
            y.left.p = y;
            y.color = z.color;
        }
        if(y_original_color == BLACK)
            RB_DELETE_FIXUP(x);
    }
    //红黑树 结点删除后fixup
    private void RB_DELETE_FIXUP(Node<T> x)
    {
        while (x != head  && x.color == BLACK)
        {
            if (x == x.p.left)
            {
                var w = x.p.right;
                if (w.color == RED)                                     //case 1
                {
                    w.color = BLACK;
                    x.p.color = RED;
                    LEFT_ROTATE(x.p);
                    w = x.p.right;
                }
                if (w.left.color == BLACK && w.right.color == BLACK)    //case 2
                {
                    w.color = RED;
                    x = x.p;
                }
                else
                {
                    if (w.right.color == BLACK)                         //case 3
                    {
                        w.left.color = BLACK;
                        w.color = RED;
                        RIGHT_ROTATE(w);
                        w = x.p.right;
                    }
                    w.color = x.p.color;                                    //case 4
                    x.p.color = BLACK;
                    w.right.color = BLACK;
                    LEFT_ROTATE(x.p);
                    x = head;
                }

            }
            else if(x== x.p.right)
            {
                var w = x.p.left;
                if (w.color == RED)
                {
                    w.color = BLACK;
                    x.p.color = RED;
                    RIGHT_ROTATE(x.p);
                    w = x.p.left;
                }
                if (w.right.color == BLACK && w.left.color == BLACK)
                {
                    w.color = RED;
                    x = x.p;
                }
                else
                {
                    if (w.left.color == BLACK)
                    {
                        w.right.color = BLACK;
                        w.color = RED;
                        LEFT_ROTATE(w);
                        w = x.p.left;
                    }
                    w.color = x.p.color;
                    x.p.color = BLACK;
                    w.left.color = BLACK;
                    RIGHT_ROTATE(x.p);
                    x = head;
                }

            }
        }
        x.color = BLACK;
    }

    public void Print(T value)
    {
        Console.Write(value.ToString() + "  ");
    }
    //左旋和右旋都默认 x，y结点不为Nil
    public void LEFT_ROTATE(Node<T> x)
    {
        var y = x.right;
        x.right = y.left;
        if (y.left != Nil)
            y.left.p = x;
        y.p = x.p;
        if (x.p == Nil)
            head = y;
        else if (x == x.p.left)
            x.p.left = y;
        else
            x.p.right = y;

        y.left = x;
        x.p = y;
    }

    public void RIGHT_ROTATE(Node<T> y)
    {
        var x = y.left;
        y.left = x.right;
        if (x.right != Nil)
            x.right.p = y;
        x.p = y.p;
        if (y.p == Nil)
            head = x;
        else if (y == y.p.left)
            y.p.left = x;
        else
            y.p.right = x;
        x.right = y;
        y.p = x;
    }

    #region Print GraphHeap
    private  Dictionary<Node<T>,int> nodeH = new Dictionary<Node<T>, int>();//记录每个节点的h
    public void PrintHeap()
    {
        if (head == Nil)
            return;

        Console.SetBufferSize(1080,720);
        int nodeDistance = 2;//兄弟节点距离
        int nodeLength = 4;//节点长度
        int h = GetHeight();
        int headPosY = GetPosY(nodeDistance, nodeLength, h);


        Queue<Node<T>> queue = new Queue<Node<T>>();
        Dictionary<Node<T>,NodePos>  dic = new Dictionary<Node<T>, NodePos>();//节点的位置 (中点)
        queue.Enqueue(head);
        while (queue.Count > 0)
        {
            Node<T> node = queue.Dequeue();
            if (node == head)
            {
                dic.Add(head, new NodePos { x = headPosY, y = Console.CursorTop+1});
                var printX = dic[node].x - node.value.ToString().Length/2;
                Console.SetCursorPosition(printX, dic[node].y);
            }
            else
            {
                if (node == node.p.left)
                {
                    var newX = dic[node.p].x - (int)(Math.Pow(2, GetNodeH(node) - 1) * (nodeDistance + nodeLength));
                    var newY = dic[node.p].y + 2;
                    dic.Add(node, new NodePos { x = newX, y = newY });

                    var printX = dic[node].x - node.value.ToString().Length / 2;
                    var parentX = dic[node.p].x - node.p.value.ToString().Length / 2;
                    for (int i = parentX; i > printX; i--)
                    {
                        Console.SetCursorPosition(i, dic[node.p].y + 1);
                        Console.Write("-");
                    }
                    Console.SetCursorPosition(printX, dic[node.p].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(dic[node.p].x - node.p.value.ToString().Length / 2, dic[node.p].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(printX, dic[node].y);
                }      
                else
                {
                    var newX = dic[node.p].x + (int)(Math.Pow(2, GetNodeH(node) - 1) * (nodeDistance + nodeLength));
                    var newY = dic[node.p].y + 2;
                    dic.Add(node, new NodePos { x = newX, y = newY });

                    var printX = dic[node].x - node.value.ToString().Length / 2;
                    var parentX = dic[node.p].x - node.p.value.ToString().Length/2;
                    for (int i = parentX; i < printX; i++)
                    {
                        Console.SetCursorPosition(i, dic[node.p].y + 1);
                        Console.Write("-");
                    }
                    Console.SetCursorPosition(printX, dic[node.p].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(parentX, dic[node.p].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(printX, dic[node].y);
                }
            }

            
            Console.Write(node.value.ToString());
            if (node.color == RED)
            {
                Console.Write("红");
            }
            else
            {
                Console.Write("黑");
            }

            if(node.left!=Nil)
                queue.Enqueue(node.left);
            if (node.right != Nil)
                queue.Enqueue(node.right);
        }
        Console.WriteLine();
    }

    class NodePos
    {
        public int  x;
        public int y;
    }
    //获取根节点的开始的x坐标
    private int GetPosY(int nodeDistance,int nodeLength,int h)
    {
       return (int)(Math.Pow(2, h - 1) * nodeLength + (Math.Pow(2, h - 1) - 1) * nodeDistance);//2^(h-1) *nodeLength + (2^(h-1)-1)*nodeDistance
    }

    private int GetNodeH(Node<T> x)
    {
        return _maxHeight - nodeH[x];
    }

    //获取树高 例：
    //      3            -----h=2 
    //    /   \
    //   1     4         -----h=1
    //    \
    //     2             -----h=0
    private int GetHeight()
    {
        _height = -1;
        _maxHeight = -1;
        INORDER_TREE_WALKGetHeight(head);
        return _maxHeight;
    }

    private int _height = -1;
    private int _maxHeight = -1;
    //中序遍历 获取高度
    private void INORDER_TREE_WALKGetHeight(Node<T> x)
    {
        if (x != Nil)
        {
            _height++;
            nodeH[x] = _height;
            if (_height > _maxHeight)
                _maxHeight = _height;
            INORDER_TREE_WALKGetHeight(x.left);

            INORDER_TREE_WALKGetHeight(x.right);
            _height--;
        }
    }
    #endregion
}
public class Node<T>
{
    public T value;
    public Node<T> left;
    public Node<T> right;
    public Node<T> p;
    public int color;
}
