using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PackageWallet.Runtime
{
    [Serializable]
    public class WalletItems 
    {
        public Dictionary<string, int> DictionaryValue = new Dictionary<string, int>();

        
        [SerializeField]
        public List<WalletItem> items = new List<WalletItem>();

        public WalletItems()
        {
          
        }
        public WalletItems(WalletItems walletItems)
        {
            foreach (var t in walletItems.items.Where(t => t != null))
            {
                Set(t.key,t.value);
            }
        }
       
        
        public void Set(string key, float value)
        {
            WalletItem walletItem = new WalletItem() {key = key, value = value};

            if (DictionaryValue.ContainsKey(key))
            {
                int index = DictionaryValue[key];
                items[index] = walletItem;
                return;
            }
            
            items.Add(walletItem);
            DictionaryValue[walletItem.key] = items.Count-1;
        }
        
        public float Get(string key)
        {
            if (!DictionaryValue.ContainsKey(key)) return 0;
            int index = DictionaryValue[key];
            return items[index].value;
        }
        
        public void DeleteKeys(string[] strings)
        {
            foreach (var key in strings)
            {
                if (!DictionaryValue.ContainsKey(key))
                {
                    continue;
                }

                int index = DictionaryValue[key];
                DictionaryValue.Remove(key);
                items[index] = null;
            }
        }
    }

    [Serializable]
    public class WalletItem
    {
        public string key;
        public float value;
    }
}