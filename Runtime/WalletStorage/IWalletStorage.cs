using System;

namespace PackageWallet.Runtime
{
    public interface IWalletStorage
    {
       
        void Get(Action<WalletItems> onGet);
        void Save(WalletItems walletItems, Action onSave);
    }
}