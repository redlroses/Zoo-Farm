using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public interface ITriggerObserverExit : ITriggerObserver
    {
        event Action<Collider> Exited;
    }
}