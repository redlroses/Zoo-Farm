namespace CodeBase.Tools
{
    internal class ObservableInt : ObservableVariable<int>
    {
        public static ObservableInt operator +(ObservableInt observableInt1, ObservableInt observableIn2) =>
            new ObservableInt {Variable = observableInt1.Variable + observableIn2.Variable};
    }
}