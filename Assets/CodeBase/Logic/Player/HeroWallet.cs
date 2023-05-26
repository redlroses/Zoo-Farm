using System.Linq;
using System.Reflection;
using CodeBase.Logic.Items;
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
            input.started += context => Wallet.TrySpend(typeof(Coin), 10);

            //TODO: убрать количество денег из Money

            input.Enable();

            FindTypes();
        }
        
        private void FindTypes()
        {
            var baseType = typeof(IItem);
            var derivedTypes = baseType.Assembly.ExportedTypes.Where(t => baseType.IsAssignableFrom(t) && t.IsInterface == false);

            Debug.Log("types");
            
            foreach (var info in derivedTypes)
            {
                Debug.Log(info.Name);
            }
        }
    }
}