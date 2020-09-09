using System;
using System.Collections.Generic;
namespace ConsoleApplication1
{
    public static class GameResources
    {
        /// 资源引用计数
        static readonly Dictionary<string, int> assetRefCount = new Dictionary<string, int>();

        /// 读取资源
        public static T Load<T>(string assetName)
        {
            int refCount;
            if (assetRefCount.TryGetValue(assetName, out refCount))
            { assetRefCount[assetName] = refCount + 1; }
            else
            { assetRefCount.Add(assetName, 1); }
            Console.WriteLine("----GameResources----Load----{0}----{1}----count:={2}----", typeof(T), assetName, refCount + 1);

            //        return Resources.Load<T>(assetName);
            return default(T);
        }

        /// 卸载资源，只有资源计数为0了才会真正卸载
        public static void UnloadAsset(string assetName, int count)
        {
            int refCount;
            if (assetRefCount.TryGetValue(assetName, out refCount))
            {
                refCount -= count;
                Console.WriteLine("----GameResources----UnloadAsset----by ref----{0}----count:={1}----", assetName, refCount);
                if (refCount <= 0)
                {
                    Console.WriteLine("----GameResources----UnloadAsset===={0}----", assetName);
                    assetRefCount.Remove(assetName);
                    //                Resources.UnloadAsset(assetName, false);
                }
                else
                {
                    assetRefCount[assetName] = refCount;
                }
            }
            else
            {
                Console.WriteLine("----GameResources----UnloadAsset:[{0}] is not in assetRefCount----", assetName);
            }
        }
        public static void Destroy()
        {
            assetRefCount.Clear();
        }
    }
    public class AssetCounter
    {
        //key：资源名
        //value：引用次数
        private readonly DictionaryList<string, int> _assetCount = new DictionaryList<string, int>();

        //添加引用时次数+1
        public void AddCount(string assetName)
        {
            int curt = 0;
            _assetCount.TryGetValue(assetName, out curt);
            _assetCount[assetName] = curt + 1;
        }
        //移除引用时次数-1
        //并且在小于0(正常情况不会小于0)时视为无效数据清除
        public void RemoveCount(string assetName)
        {
            int curt = 0;
            if (_assetCount.TryGetValue(assetName, out curt))
            {
                curt--;
                if (curt < 0)
                {
                    _assetCount.Remove(assetName);
                }
                else
                {
                    _assetCount[assetName] = curt;
                }
            }
        }
        //清除所有引用计数
        public void Clear()
        {
            _assetCount.Clear();
        }

        //移除引用时调用
        public void DoUnload()
        {
            if (_assetCount.Count > 0)
            {
                var list = _assetCount.List;
                int count = list.Count;
                for (int i = 0; i < count; i++)
                {
                    var item = list[i];
                    //这里可以一次卸载多次
                    GameResources.UnloadAsset(item.Key, item.Value);
                }
                _assetCount.Clear();
            }
        }
    }
}
