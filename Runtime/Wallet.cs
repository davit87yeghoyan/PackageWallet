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

        public static WalletItems GetWalletItems => _walletItems.Copy();


        static Wallet()
        {
            _walletItems = new WalletItems {Value = new Dictionary<string, float>()};
        }
        
        public static void SetValue(string key, int value)
        {
            _walletItems.Value[key] = value;
            OnSetItem(key, value);
        }
        
        public static float GetValue(string key)
        {
            if(!_walletItems.Value.ContainsKey(key)) throw new Exception();
            return _walletItems.Value[key];
        }

        public static bool ContainsKey(string key)
        {
            return _walletItems.Value.ContainsKey(key);
        }
        

        public static void DeleteKey(string key)
        {
            _walletItems.Value.Remove(key);
            OnRemoveItem( new[]{key});
        }

        public static void DeleteAllKey()
        {
            string[] keys = _walletItems.Value.Keys.ToArray();
            _walletItems.Value.Clear();
            OnRemoveItem( keys);    
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
            _walletItems = walletItems;
            GetFromStorage?.Invoke();
        }

        private static void OnSaveToStorage()
        {
            SaveToStorage?.Invoke();
        }
    }

   
}
