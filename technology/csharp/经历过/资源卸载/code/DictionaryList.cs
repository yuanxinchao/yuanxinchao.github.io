using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DictionaryList<TKey, TValue>
    {
        readonly Dictionary<TKey, ListPair<TKey, TValue>> _dict;
        readonly List<ListPair<TKey, TValue>> _list;

        public Dictionary<TKey, ListPair<TKey, TValue>> Dict { get { return _dict; } }

        public List<ListPair<TKey, TValue>> List { get { return _list; } }

        public int Count { get { return _dict.Count; } }

        public DictionaryList() : this(0)
        {

        }

        public DictionaryList(int size)
        {
            _dict = new Dictionary<TKey, ListPair<TKey, TValue>>(size);
            _list = new List<ListPair<TKey, TValue>>(size);
        }

        public void Add(TKey key, TValue value)
        {
            var pair = new ListPair<TKey, TValue>(key, value);
            _dict.Add(key, pair);
            _list.Add(pair);
        }

        public void Remove(TKey key)
        {
            ListPair<TKey, TValue> pair;
            if (_dict.TryGetValue(key, out pair))
            {
                _list.Remove(pair);
                _dict.Remove(key);
            }
        }

        public void Clear()
        {
            _dict.Clear();
            _list.Clear();
        }

        public TValue this[TKey key]
        {
            get
            {
                return _dict[key].Value;
            }
            set
            {
                Set(key, value);
            }
        }

        public bool ContainsKey(TKey item)
        {
            return _dict.ContainsKey(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            ListPair<TKey, TValue> pair;
            if (_dict.TryGetValue(key, out pair))
            {
                value = pair.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public void Set(TKey key, TValue value)
        {
            ListPair<TKey, TValue> pair;
            if (_dict.TryGetValue(key, out pair))
            {
                pair.Value = value;
            }
            else
            {
                Add(key, value);
            }
        }

        public IEnumerator<ListPair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<ListPair<TKey, TValue>>)_list).GetEnumerator();
        }
    }
    public class ListPair<TKey, TValue>
    {
        public TKey Key;
        public TValue Value;

        public ListPair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public ListPair()
        {

        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[');
            if (Key != null)
            {
                stringBuilder.Append(Key.ToString());
            }
            stringBuilder.Append(", ");
            if (Value != null)
            {
                stringBuilder.Append(Value.ToString());
            }
            stringBuilder.Append(']');
            return stringBuilder.ToString();
        }
    }
}
