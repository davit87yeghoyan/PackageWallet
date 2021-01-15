using System;
using System.Collections.Generic;
using System.Linq;
using PackageWallet.Runtime.WalletStorage;

namespace PackageWallet.Runtime
{
    public static class Wallet
    {
        public static event Action<string,float> SetItem;
        public static event Action<string[]> RemoveItem;
        public static event Action GetFromStorage;
        public static event Action SaveToStorage;

        private static WalletItems _walletItems;

        public static List<WalletItem> GetItems => new List<WalletItem>(_walletItems.items);

        static Wallet()
        {
            _walletItems = new WalletItems();
        }
        
        public static void SetValue(string key, float value)
        {
            _walletItems.Set(key, value);
            OnSetItem(key, value);
        }
        
        public static float GetValue(string key)
        {
            return _walletItems.Get(key);
        }
        
       

        public static bool ContainsKey(string key)
        {
            return _walletItems.DictionaryValue.ContainsKey(key);
        }
        

        public static void DeleteKey(string key)
        {
            string[] strings = {key};
            _walletItems.DeleteKeys(strings);
            OnRemoveItem( strings);
        }

        public static void DeleteAllKey()
        {
            string[] keys = _walletItems.DictionaryValue.Keys.ToArray();
            _walletItems.DeleteKeys(keys);
            OnRemoveItem(keys);    
        }
        
        public static void SaveStorage(IWalletStorage walletStorage)
        {
            walletStorage.Save(_walletItems, OnSaveToStorage);
        }
        
        public static void GetStorage(IWalletStorage walletStorage)
        {
            walletStorage.Get(OnGetFromStorage);
        }
        
        
        
        private static void OnSetItem(string key, float value)
        {
            SetItem?.Invoke(key, value);
        }        
        private static void OnRemoveItem(string[] keys)
        {
            RemoveItem?.Invoke(keys);
        }
        

        private static void OnGetFromStorage(WalletItems walletItems)
        {
            _walletItems = new WalletItems(walletItems);
            GetFromStorage?.Invoke();
        }

        private static void OnSaveToStorage()
        {
            SaveToStorage?.Invoke();
        }
    }

   
}
