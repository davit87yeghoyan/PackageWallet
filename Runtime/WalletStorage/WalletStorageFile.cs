using System;
using System.IO;
using UnityEngine;

namespace PackageWallet.Runtime.WalletStorage
{
    public class WalletStorageFile:WalletStorageJson,IWalletStorage
    {

        private readonly string _path;
        
        public WalletStorageFile(string path)
        {
            _path = path;
        }

        public void Save(WalletItems walletItems, Action onSave)
        {
            File.WriteAllText(_path,Serialization(walletItems));
            onSave?.Invoke();
        }
        
        public void Get(Action<WalletItems> onGet)
        {
            if (!File.Exists(_path))
            {
                return;
            }
            string json = File.ReadAllText(_path);
            if(string.IsNullOrEmpty(json)) return;
            WalletItems walletItems = Deserialize(json);
            onGet?.Invoke(walletItems);
        }

    }
    
    
    
}