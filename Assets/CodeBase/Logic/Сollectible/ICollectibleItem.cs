using CodeBase.Logic.Items;

namespace CodeBase.Logic.Сollectible
{
    public interface ICollectibleItem : ICollectible
    {
        public IItem Item { get; }
        public int Count { get; }
    }
}