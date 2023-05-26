using CodeBase.Logic.Items;

namespace CodeBase.Logic.Сollectible
{
    public interface ICollectible
    {
        public IItem Item { get; }
        public int Count { get; }
        public void Collect();
    }
}