
using System;
using System.Collections.Generic;

public class PoolFactory
{
    public static Pool<T> Create<T>(Func<T> creatFunc, Action<T> onGet, Action<T> onPut, int initSize = 4)
    {
        if (creatFunc == null)
            throw new Exception("createFunc must not be null!");

        var pool = new Pool<T>(creatFunc, onGet, onPut, initSize) {ExpandRule = PoolExpandRule.InitSize};
        return pool;
    }

    public enum PoolExpandRule
    {
        InitSize, //每次扩展时都增加初始指定容量
        Double, // 每次扩展时增加当前容量的一倍
        One, //每次扩展时加1
    }
}

public class Pool<T>
{
    private readonly Func<T> _creatFunc;
    private readonly Action<T> _onGet;
    private readonly Action<T> _onPut;
    private Stack<T> _objs;
    private readonly int _initSize;
    private int _maxSize;

    public int FreeCount
    {
        get { return _objs.Count; }
    }

    public int UsingCount
    {
        get { return _maxSize - _objs.Count; }
    }

    public PoolFactory.PoolExpandRule ExpandRule { get; set; }

    internal Pool(Func<T> creatFunc, Action<T> onGet, Action<T> onPut, int initSize = 32)
    {
        _initSize = initSize;
        _creatFunc = creatFunc;
        _onGet = onGet;
        _onPut = onPut;
        _objs = new Stack<T>(initSize);
        Spawn(_initSize);
    }

    private void Spawn(int size)
    {
        for (int i = 0; i < size; i++)
        {
            Put(_creatFunc());
        }
        _maxSize += size;
    }

    private void Expand()
    {
        switch (ExpandRule)
        {
            case PoolFactory.PoolExpandRule.One:
                Spawn(1);
                break;

            case PoolFactory.PoolExpandRule.Double:
                Spawn(_maxSize);
                break;

            case PoolFactory.PoolExpandRule.InitSize:
                Spawn(_initSize);
                break;

            default:
                Spawn(1);
                break;
        }
    }

    public T Get()
    {
        if (_objs.Count <= 0)
        {
            Expand();
        }
        var obj = _objs.Pop();
        if (_onGet != null)
            _onGet(obj);
        return obj;
    }

    public void Put(T obj)
    {
        if (_onPut != null)
            _onPut(obj);
        _objs.Push(obj);
    }

    public void Clear()
    {
        if(_objs!=null)
            _objs.Clear();
        _objs = null;
    }
}



