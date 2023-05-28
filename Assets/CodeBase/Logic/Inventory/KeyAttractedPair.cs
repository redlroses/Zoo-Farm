using System;
using UnityEngine;

namespace CodeBase.Logic.Inventory
{
    public readonly struct KeyAttractedPair
    {
        public readonly Type type;
        public readonly GameObject attracted;

        public KeyAttractedPair(Type type, GameObject attracted)
        {
            this.type = type;
            this.attracted = attracted;
        }
    }
}