using System;

namespace PackageWallet
{
    public interface IWalletStorage
    {
       
        void Get(Action<WalletItems> onGet);
        void Save(WalletItems walletItems, Action onSave);
    }
}