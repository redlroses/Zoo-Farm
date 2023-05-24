using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public interface ITriggerObserverExitCommon : ITriggerObserverCommon
    {
        event Action<Collider> Exited;
    }
}