using CodeBase.Logic.Wallet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Logic.Player
{
    public class HeroWallet : MonoBehaviour
    {
        public IWallet Wallet { get; private set; }

        private void Awake()
        {
            Wallet = new Wallet.Wallet();

            Debug.Log("Press \"X\" for spend 10 coins");

            InputAction input = new InputAction("Press X", InputActionType.Button, "<Keyboard>/x");
            input.started += context => Wallet.TrySpend(10);

            input.Enable();
        }
    }
}