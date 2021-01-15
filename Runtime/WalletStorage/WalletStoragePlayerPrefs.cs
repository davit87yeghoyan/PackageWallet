using System;
using UnityEngine;

namespace PackageWallet.Runtime.WalletStorage
{
    public class WalletStoragePlayerPrefs:WalletStorageJson,IWalletStorage
    {

        private readonly string _playerPrefsKey;
        
        public WalletStoragePlayerPrefs(string playerPrefsKey)
        {
            _playerPrefsKey = playerPrefsKey;
        }

        public void Save(WalletItems walletItems, Action onSave)
        {
            PlayerPrefs.SetString(_playerPrefsKey, Serialization(walletItems));
            PlayerPrefs.Save();
            onSave?.Invoke();
        }
        
        public void Get(Action<WalletItems> onGet)
        {
            string json = PlayerPrefs.GetString(_playerPrefsKey);
            if(string.IsNullOrEmpty(json)) return;
            WalletItems walletItems = Deserialize(json);
            onGet?.Invoke(walletItems);
        }

    }
}