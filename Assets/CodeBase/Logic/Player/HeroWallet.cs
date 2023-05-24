using CodeBase.Logic.Wallet;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class HeroWallet : MonoBehaviour
    {
        public IWallet Wallet { get; private set; }

        private void Awake()
        {
            Wallet = new Wallet.Wallet();
            Wallet.Changed += i => print($"Wallet account is {i} money");
        }
    }
}