namespace CodeBase.Logic.Items
{
    public struct Carrot : IWeighty
    {
        public int Weight { get; }

        public Carrot(int weight) =>
            Weight = weight;
    }
}