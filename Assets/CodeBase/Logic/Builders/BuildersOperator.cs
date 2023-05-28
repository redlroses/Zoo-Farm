using System.Collections.Generic;
using CodeBase.Logic.Interactions;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Builders
{
    public class BuildersOperator : MonoBehaviour
    {
        private readonly Queue<Builder> _builders = new Queue<Builder>();

        private InteractionInventoryPaidZone _interactionPaidZone;

        private void Start()
        {
            CollectAllBuilders();
            ActivateNextBuilder();
        }

        private void OnFullPaid()
        {
            _interactionPaidZone.FullPaid -= OnFullPaid;
            ActivateNextBuilder();
        }

        private void ActivateNextBuilder()
        {
            if (_builders.TryDequeue(out Builder builder) == false)
                return;

            builder.gameObject.Enable();
            _interactionPaidZone = builder.GetComponentInChildren<InteractionInventoryPaidZone>(true);
            _interactionPaidZone.FullPaid += OnFullPaid;
        }

        private void CollectAllBuilders()
        {
            foreach (CarrotBuilder builder in GetComponentsInChildren<CarrotBuilder>(true))
            {
                builder.gameObject.Disable();
                _builders.Enqueue(builder);
            }
        }
    }
}