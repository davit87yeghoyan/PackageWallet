using UnityEngine;

namespace PackageWallet.Runtime.WalletStorage
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