using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMK.Utils.Extensions
{
    public class MultiMap<KeyT, ValueT>
    {
        Dictionary<KeyT, List<ValueT>> _dictionary = new Dictionary<KeyT, List<ValueT>>();

        public void Add(KeyT key, ValueT value, bool unique = false)
        {
            // Add a key.
            List<ValueT> list;
            if (this._dictionary.TryGetValue(key, out list))
            {
                if (unique)
                {
                    if (!list.Contains(value))
                        list.Add(value);
                }
                else
                    list.Add(value);
            }
            else
            {
                list = new List<ValueT>();
                list.Add(value);
                this._dictionary[key] = list;
            }
        }

		public void Clear()
		{
			_dictionary.Clear();
		}

		public IEnumerable<KeyT> Keys
        {
            get
            {
				// Get all keys.
				return this._dictionary.Keys;
            }
        }

		public bool ContainsKey(KeyT key)
		{
			return this._dictionary.ContainsKey(key);
		}

		public List<ValueT> this[KeyT key]
        {
            get
            {
                // Get list at a key.
                List<ValueT> list;
                if (!this._dictionary.TryGetValue(key, out list))
                {
                    list = new List<ValueT>();
                    this._dictionary[key] = list;
                }
                return list;
            }
        }


    }
}
