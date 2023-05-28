using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public interface ITriggerObserver
    {
        event Action<Collider> Entered;
    }
}