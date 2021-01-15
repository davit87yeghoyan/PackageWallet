using UnityEngine;

namespace PackageWallet.WalletStorage
{
    public abstract class WalletStorageJson
    {
        protected string Serialization(WalletItems walletItems)
        {
            return JsonUtility.ToJson(walletItems);
        }
        
        protected WalletItems Deserialize (string json)
        {
            return JsonUtility.FromJson<WalletItems>(json);
        }
    }
}