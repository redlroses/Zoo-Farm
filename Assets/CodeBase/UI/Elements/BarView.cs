﻿﻿using System;
using CodeBase.Logic;
using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class BarView : MonoCache
    {
        [SerializeField] protected SliderSetter _sliderSetter;

        protected IParameter Parameter;

        private void OnDestroy()
        {
            if (Parameter is null)
                return;

            Parameter.Changed -= OnChanged;
        }

        public void Construct(IParameter parameter)
        {
            Parameter = parameter;
            Parameter.Changed += OnChanged;
            OnChanged();
        }

        protected virtual void OnChanged() =>
            _sliderSetter.SetNormalizedValue(GetNormalizedBarValue());

        private float GetNormalizedBarValue() =>
            Parameter.CurrentPoints / (float) Parameter.MaxPoints;
    }
}