using System;

namespace PackageWallet.WalletStorage
{
    public interface IWalletStorage
    {
       
        void Get(Action<WalletItems> onGet);
        void Save(WalletItems walletItems, Action onSave);
    }
}