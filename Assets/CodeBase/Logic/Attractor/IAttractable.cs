using System;
using UnityEngine;

namespace CodeBase.Logic.Attractor
{
    public interface IAttractable
    {
        void Attract(Transform to);
        GameObject GameObject { get; }
        event Action WasAttracted;
    }
}