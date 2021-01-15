using System;
using UnityEngine;

namespace PackageWallet.Runtime
{
    class WalletStoragePlayerPrefs:WalletStorageJson,IWalletStorage
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
            WalletItems walletItems = Deserialize(json);
            onGet?.Invoke(walletItems);
        }

    }
}