using System;
using System.Collections.Generic;

namespace PackageWallet
{
    [Serializable]
    public class WalletItems
    {
        public Dictionary<string, float> Value = new Dictionary<string, float>();

        public WalletItems Copy()
        {
            return new WalletItems() {Value = new Dictionary<string, float>(Value)};
        }
    }
}