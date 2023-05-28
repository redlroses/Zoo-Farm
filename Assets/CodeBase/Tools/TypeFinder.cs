using System;
using System.Collections.Generic;

namespace CodeBase.Tools
{
    public struct TypeFinder
    {
        private readonly Type _type;
        private readonly List<Type> _types;

        public TypeFinder(Type type)
        {
            _type = type;
            _types = new List<Type>();
        }

        public List<Type> GetTypes() =>
            FindTypes();

        public readonly Type GetTypeByName(string currencyName) =>
            _types.Find(type => string.Equals(type.Name, currencyName));

        private List<Type> FindTypes()
        {
            foreach (Type type in _type.Assembly.ExportedTypes)
            {
                if (_type.IsAssignableFrom(type) && type.IsInterface == false)
                    _types.Add(type);
            }

            return _types;
        }
    }
}