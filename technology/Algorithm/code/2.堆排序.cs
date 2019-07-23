using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class Program
    {
        class MergeItem
        {
            public int value;
            public List<int> address;
        }
       
        static void Main(string[] args)
        {
            List<int> A =new List<int> {0,3,6,9,12,14,16,18,21};
            List<int> B =new List<int> { 1, 4, 7, 10, 13, 16, 19 };
            List<int> C =new List<int> { 2, 5, 8, 23, 25, 67, 78 };
            List<int> D =new List<int> { 54, 55, 65, 234, 1234 };

//            int[] E = new int[] { 4, 3, 8, 45, 23, 3, 57, 1 };
//            Heap<int> test = new Heap<int>(E, (a, b) => a - b);
//            test.HEAPSPORT();
//            test.PRINT(a => Console.Write(a + "  "));

            var merge =MergeOrderList(A, B, C,D);
            for (int i = 0; i < merge.Count; i++)
            {
                Console.Write("  "+merge[i]);
            }
        }

        static List<int> MergeOrderList(params List<int>[] m)
        {
            Heap<MergeItem> heap = new Heap<MergeItem>((a, b) => b.value - a.value);

            List<int> sortOrder = new List<int>();

            for (int i = 0; i < m.Length; i++)
            {
                heap.MAX_HEAP_INSERT(new MergeItem{value = m[i][0],address =m[i]});
                m[i].RemoveAt(0);
            }
            while (heap.Count > 0)
            {

                MergeItem item = heap.MAXIMUM();
                sortOrder.Add(item.value);

                if (item.address.Count > 0)
                {
                    heap.INCREASE_KEY(0,new MergeItem{value = item.address[0],address =item.address});
                    item.address.RemoveAt(0);
                }
                else
                {
                    heap.EXTRACT_MAX();
                }
            }
            return sortOrder;
        }


    }

    public class Heap<T>
    {
        private int HeapSize;

        private Comparison<T> compare;
        private T[] A;

        public Heap(T[] A,Comparison<T> compare)
        {
            if(compare == null)
                throw new Exception("compare should not null");

            this.A = A;
            this.compare = compare;
            HeapSize = A.Length;
        }

        public Heap(Comparison<T> compare):this(16,compare){ }

        public Heap(int capacity, Comparison<T> compare)
        {
            if (compare == null)
                throw new Exception("compare should not null");

            A = new T[capacity];
            this.compare = compare;
        }
        public int LEFT(int i)
        {
            return (i << 1) + 1;
        }

        public int RIGHT(int i)
        {
            return (i << 1) + 2;
        }

        public int PARENT(int i)
        {
            return (i - 1) >> 1;
        }

        public void HEAPSPORT()
        {
            BUILD_MAX_HEAP();
            int heapSize = HeapSize;
            for (int i = HeapSize-1; i >=1; i--)
            {
                Exchange(0,i);//最大的换到最后
                HeapSize = HeapSize - 1;
                MAX_HEAPIFY(0);//维护剩余的值最大堆性质
            }
            HeapSize = heapSize;
        }
        public void MAX_HEAPIFY(int i)
        {
            int largest;
            var l = LEFT(i);
            var r = RIGHT(i);
            if (l < HeapSize && compare(A[l], A[i]) > 0)
                largest = l;
            else
                largest = i;

            if (r < HeapSize && compare(A[r], A[largest]) > 0)
                largest = r;

            if (largest != i)
            {
                Exchange(i, largest);
                MAX_HEAPIFY(largest);
            }
        }

        public void Exchange(int i, int j)
        {
            T temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }
        public void BUILD_MAX_HEAP()
        {
            for (int i = PARENT(HeapSize - 1); i >= 0; i--)
            {
                MAX_HEAPIFY(i);
            }
        }

        public int Count
        {
            get { return HeapSize; }
        } 
        public void PRINT(Action<T> ac)
        {
            Console.WriteLine();
            Queue<int> queue = new Queue<int>(10);
            int i = 0;
            queue.Enqueue(i);
            while (i < HeapSize && queue.Count > 0)
            {
                i = queue.Dequeue();
                if (i == 1
                    || i == 3
                    || i == 7
                    || i == 15)
                    Console.WriteLine();

                ac(A[i]);


                if (LEFT(i) < HeapSize)
                    queue.Enqueue(LEFT(i));
                if (RIGHT(i) < HeapSize)
                    queue.Enqueue(RIGHT(i));
            }
        }

        //优先队列
        public T MAXIMUM()
        {
            return A[0];
        }
        public T EXTRACT_MAX()
        {
            if (HeapSize < 1)
                throw new Exception("heap underflow");
            T max = A[0];
            A[0] = A[HeapSize - 1];
            HeapSize--;
            MAX_HEAPIFY(0);
            return max;
        }

        public void INCREASE_KEY(int i, T key)
        {
            if (compare(A[i], key) > 0)
            {
                A[i] = key;
                MAX_HEAPIFY(i);
            }
            else
            {
                while (i > 0 && compare(key, A[PARENT(i)]) > 0)
                {
                    A[i] = A[PARENT(i)];
                    i = PARENT(i);
                }
                A[i] = key;
            }
        }

        public void MAX_HEAP_INSERT(T key)
        {
            if(HeapSize >= A.Length)
                Array.Resize(ref A, HeapSize * 2);

            HeapSize++;
            A[HeapSize - 1] = key;
            INCREASE_KEY(HeapSize - 1,key);
        }
    }
}