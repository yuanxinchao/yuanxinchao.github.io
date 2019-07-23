using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Node<int> n11 = new Node<int>() { value = 9 };
            Node<int> n10 = new Node<int>() { value = 13, left = n11 };
            n11.parent = n10;
            Node<int> n9 = new Node<int>() { value = 4 };
            Node<int> n8 = new Node<int>() { value = 2 };
            Node<int> n7 = new Node<int>() { value = 20};
            Node<int> n6 = new Node<int>() { value = 17 };
            Node<int> n5 = new Node<int>() { value = 7, right = n10 };
            n10.parent = n5;
            Node<int> n4 = new Node<int>() { value = 3,left = n8,right = n9};
            n8.parent = n9.parent = n4;
            Node<int> n3 = new Node<int>() { value = 18,left = n6,right = n7};
            n6.parent = n7.parent = n3;
            Node<int> n2 = new Node<int>() { value = 6 ,left = n4,right = n5};
            n4.parent = n5.parent = n2;
            Node<int> n1 = new Node<int>() {value = 15,left = n2,right = n3};
            n2.parent = n3.parent = n1;

            SearchTree<int> s = new SearchTree<int>(n1,(a,b)=> a-b);
            s.INORDER_TREE_WALK1(n1);

//            Console.WriteLine(s.TREE_PREDECESSOR(n7).value);
//            SearchTree<int> s = new SearchTree<int>(n1, (a, b) => a - b);
//            var test = new Node<int>() {value = 9};
//            s.TREE_INSERT(test);
//            s.TREE_INSERT(new Node<int>() {value = 2});
//            s.TREE_INSERT(new Node<int>() {value = 3});
//            s.TREE_INSERT(new Node<int>() {value = 4});
//            s.TREE_INSERT(new Node<int>() {value = 23});
//            s.TREE_INSERT(new Node<int>() {value = 14});
//            s.INORDER_TREE_WALK();
//            s.PrintHeap();

//            var x = s.TREE_MAXIMUM();
//            while (x != null)
//            {
//                s.TREE_DELETE(x);
//                s.INORDER_TREE_WALK();
//                Console.WriteLine();
//                x = s.TREE_PREDECESSOR(x);
//            }

        }


    }

}

public class SearchTree<T> where T : IComparable
{
    private Node<T> head;

    private Comparison<T> compare; 

    public SearchTree(Node<T> node,Comparison<T> compare)
    {
        head = node;
        this.compare = compare;
    }


    //中序遍历(递归)
    public void INORDER_TREE_WALK1(Node<T> x)
    {
        if (x != null)
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
            if (x != null)
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
        if (x != null)
        {
            Print(x.value);
            PREORDER_TREE_WALK1(x.left);
            PREORDER_TREE_WALK1(x.right);
        }
    }
    //后续遍历
    public void POSTORDER_TREE_WALK1(Node<T> x)
    {
        if (x != null)
        {
            POSTORDER_TREE_WALK1(x.left);
            POSTORDER_TREE_WALK1(x.right);
            Print(x.value);

        }
    }

    public Node<T> TREE_SEARCH(Node<T> node,T k)
    {
        if (node == null || compare(k,node.value) == 0)
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
        while (node !=null && compare(k,node.value)!=0)
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
        while (node.left != null)
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
        while (x.right != null)
        {
            x = x.right;
        }
        return x;
    } 
    //中序遍历 的某结点后继
    public Node<T> TREE_SUCCESSOR(Node<T> x)
    {
        if (x.right != null)
            return TREE_MINIMUM(x.right);

        var y = x.parent;
        while (y !=null &&x == y.right)
        {
            x = y;
            y = y.parent;
        }
        return y;
    } 
    //中序遍历 的某结点前驱
    public Node<T> TREE_PREDECESSOR(Node<T> x)
    {
        if (x.left != null)
            return TREE_MAXIMUM(x.left);

        var y = x.parent;
        while (y !=null && x == y.left)
        {
            x = y;
            y = y.parent;
        }
        return y;
    } 
    //插入
    public void TREE_INSERT(Node<T> x)
    {
        if (head == null)
        {
            head = x;
            return;
        }

        var node = head;
        var y = node;
        while (node != null)
        {
            y = node;
            if (compare(x.value, node.value) < 0)
            {
                node = node.left;
            }
            else
            {
                node = node.right;
            }
        }
        if (compare(x.value, y.value) < 0)
            y.left = x;
        else
            y.right = x;

        x.parent = y;
    }
    // 嫁接(删除节点中需要的一个操作)
    public void TRANSPLANT(Node<T> u,Node<T> v)
    {
        if (u == head)
            head = v;
        else if (u == u.parent.left)
            u.parent.left = v;
        else if (u == u.parent.right)
            u.parent.right = v;
        if (v != null)
            v.parent = u.parent;
    }

    public void TREE_DELETE(Node<T> z)
    {
        
        if (z.left == null)
            TRANSPLANT(z,z.right);
        else if (z.right == null)
            TRANSPLANT(z, z.left);
        //有两孩子
        else
        {
            var y = TREE_MINIMUM(z.right);
            if (y.parent != z)
            {
                TRANSPLANT(y, y.right);

                y.right = z.right;
                y.right.parent = y;

            }
            TRANSPLANT(z, y);
            y.left = z.left;
            y.left.parent = y;
        }
    }

    public void Print(T value)
    {
        Console.Write(value.ToString() + "  ");
    }
    #region Print GraphHeap
    private  Dictionary<Node<T>,int> nodeH = new Dictionary<Node<T>, int>();//记录每个节点的h
    public void PrintHeap()
    {
        if(head == null)
            return;

        Console.SetBufferSize(1080,720);
        int nodeDistance = 2;//兄弟节点距离
        int nodeLength = 3;//节点长度
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
                if (node == node.parent.left)
                {
                    var newX = dic[node.parent].x - (int)(Math.Pow(2, GetNodeH(node) - 1) * (nodeDistance + nodeLength));
                    var newY = dic[node.parent].y + 2;
                    dic.Add(node, new NodePos { x = newX, y = newY });

                    var printX = dic[node].x - node.value.ToString().Length / 2;
                    var parentX = dic[node.parent].x - node.parent.value.ToString().Length / 2;
                    for (int i = parentX; i > printX; i--)
                    {
                        Console.SetCursorPosition(i, dic[node.parent].y + 1);
                        Console.Write("-");
                    }
                    Console.SetCursorPosition(printX, dic[node.parent].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(dic[node.parent].x - node.parent.value.ToString().Length / 2, dic[node.parent].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(printX, dic[node].y);
                }      
                else
                {
                    var newX = dic[node.parent].x + (int)(Math.Pow(2, GetNodeH(node) - 1) * (nodeDistance + nodeLength));
                    var newY = dic[node.parent].y + 2;
                    dic.Add(node, new NodePos { x = newX, y = newY });

                    var printX = dic[node].x - node.value.ToString().Length / 2;
                    var parentX = dic[node.parent].x - node.parent.value.ToString().Length/2;
                    for (int i = parentX; i < printX; i++)
                    {
                        Console.SetCursorPosition(i, dic[node.parent].y + 1);
                        Console.Write("-");
                    }
                    Console.SetCursorPosition(printX, dic[node.parent].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(parentX, dic[node.parent].y + 1);
                    Console.Write("+");
                    Console.SetCursorPosition(printX, dic[node].y);
                }
            }

            
            Console.Write(node.value.ToString());

            if(node.left!=null)
                queue.Enqueue(node.left);
            if(node.right!=null)
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
        if (x != null)
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
    public Node<T> parent;
}