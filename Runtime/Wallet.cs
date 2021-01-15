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
        public static event Action<WalletItems> GetFromStorage;
        public static event Action SaveToStorage;

        private static readonly WalletItems WalletItems;

        public static WalletItems GetWalletItems => WalletItems.Copy();


        static Wallet()
        {
            WalletItems = new WalletItems {Value = new Dictionary<string, float>()};
        }
        
        public static void Set(string key, int value)
        {
            WalletItems.Value[key] = value;
            OnSetItem(key, value);
        }
        
        public static float Get(string key)
        {
            if(!WalletItems.Value.ContainsKey(key)) throw new Exception();
            return WalletItems.Value[key];
        }

        public static void DeleteKey(string key)
        {
            WalletItems.Value.Remove(key);
            OnRemoveItem( new[]{key});
        }

        public static void DeleteAllKey()
        {
            string[] keys = WalletItems.Value.Keys.ToArray();
            WalletItems.Value.Clear();
            OnRemoveItem( keys);    
        }
        
        public static void SaveStorage(IWalletStorage walletStorage)
        {
            walletStorage.Save(WalletItems, OnSaveToStorage);
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
        

        private static void OnGetFromStorage(WalletItems obj)
        {
            GetFromStorage?.Invoke(obj);
        }

        private static void OnSaveToStorage()
        {
            SaveToStorage?.Invoke();
        }
    }

   
}
