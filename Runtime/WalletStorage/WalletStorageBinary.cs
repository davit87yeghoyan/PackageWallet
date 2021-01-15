using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PackageWallet
{
    class WalletStorageBinary:IWalletStorage
    {
        private readonly string _path;
        
        public WalletStorageBinary(string path)
        {
            _path = path;
        }

        public void Save(WalletItems walletItems, Action onSave)
        {
            FileStream dataStream = new FileStream(_path, FileMode.Create);
            BinaryFormatter converter = new BinaryFormatter();
            converter.Serialize(dataStream, walletItems);
            dataStream.Close();
            onSave?.Invoke();
        }
        
        public void Get(Action<WalletItems> onGet)
        {
            if (!File.Exists(_path))
            {
                Debug.LogError("file not found in " + _path);
                return;
            }
            // File exists 
            FileStream dataStream = new FileStream(_path, FileMode.Open);
            BinaryFormatter converter = new BinaryFormatter();
            var deserialize = converter.Deserialize(dataStream);
            dataStream.Close();
            WalletItems walletItems = deserialize as WalletItems;
            onGet?.Invoke(walletItems);
        }

    }
    
    
    
}