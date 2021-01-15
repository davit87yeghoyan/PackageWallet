using System;
using System.IO;
using UnityEngine;

namespace PackageWallet.Runtime
{
    class WalletStorageFile:WalletStorageJson,IWalletStorage
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
                Debug.LogError("file not found in " + _path);
                return;
            }
            string json = File.ReadAllText(_path);
            WalletItems walletItems = Deserialize(json);
            onGet?.Invoke(walletItems);
        }

    }
    
    
    
}