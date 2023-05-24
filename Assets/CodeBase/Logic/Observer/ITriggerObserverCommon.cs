using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public interface ITriggerObserverCommon
    {
        event Action<Collider> Entered;
    }
}