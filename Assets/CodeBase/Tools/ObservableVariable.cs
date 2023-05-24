using System;

namespace CodeBase.Tools
{
    public class ObservableVariable<T>
    {
        private T _variable;

        private event Action<T> Changed;

        public T Variable
        {
            get => _variable;
            set
            {
                _variable = value;
                Changed?.Invoke(value);
            }
        }
    }
}