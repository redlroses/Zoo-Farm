using CodeBase.Logic.Inventory;
using CodeBase.Logic.Observer;

namespace CodeBase.Logic.Сollectible
{
    public class TestObserverExited : ObserverTargetExit<HeroInventory, TriggerObserverExit>
    {
        protected override void OnTargetEntered(HeroInventory inventory) =>
            print("inventory entered");

        protected override void OnTargetExited(HeroInventory target) =>
            print("inventory exited");
    }
}