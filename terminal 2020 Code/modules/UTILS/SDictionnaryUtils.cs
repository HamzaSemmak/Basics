using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sorec_gamma.modules.UTILS
{
    public class SDictionnaryUtils <TKey, TValue>
    {
        public static TValue getValueByIndex(SortedDictionary<TKey, TValue> sDictionnary, int index)
        {
            int i = -1;
            foreach (TKey k in sDictionnary.Keys)
            {
                i++;
                if (i == index)
                    return sDictionnary[k];
            }
            return (TValue) new object();
        }

        public static int getIndexItem(SortedDictionary<TKey, TValue> dictionnary, TKey key)
        {
            int index = -1;
            foreach (TKey k in dictionnary.Keys)
            {
                index++;
                if (k.Equals(key))
                    return index;
            }
            return -1;
        }

        public static TKey getKeyByIndex(SortedDictionary<TKey, TValue> dict, int i)
        {
            TKey[] keys = new TKey[dict.Keys.Count];
            dict.Keys.CopyTo(keys, 0);
            return keys[i];
        }
    }
}
