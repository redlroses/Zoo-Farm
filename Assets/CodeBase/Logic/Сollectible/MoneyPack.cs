using CodeBase.Logic.Items;

namespace CodeBase.Logic.Сollectible
{
    public class MoneyPack : Collectible
    {
        public int AmountMoney => ((Money) Item).Amount;
    }
}