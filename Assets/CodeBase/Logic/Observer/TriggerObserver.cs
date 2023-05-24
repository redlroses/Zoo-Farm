using System;
using UnityEngine;

namespace CodeBase.Logic.Observer
{
    public class TriggerObserver : MonoBehaviour, ITriggerObserverCommon
    {
        public event Action<Collider> Entered = c => { };

        private void OnTriggerEnter(Collider other) =>
            Entered?.Invoke(other);
    }
}