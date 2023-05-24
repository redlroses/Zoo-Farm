using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public class TriggerObserverExit : TriggerObserver, ITriggerObserverExitCommon
    {
        public event Action<Collider> Exited = c => { };

        private void OnTriggerExit(Collider other) =>
            Exited.Invoke(other);
    }
}