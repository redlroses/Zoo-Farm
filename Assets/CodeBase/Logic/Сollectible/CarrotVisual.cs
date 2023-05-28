using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Сollectible
{
    public class CarrotVisual : MonoBehaviour, ICollectible
    {
        public void Collect() =>
            gameObject.Disable();
    }
}