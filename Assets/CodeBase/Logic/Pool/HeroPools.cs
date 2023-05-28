using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.Attractor;
using CodeBase.Logic.Items;
using NTC.Global.System;
using UnityEngine;

namespace CodeBase.Logic.Pool
{
    public class HeroPools : MonoBehaviour
    {
        private const string PoolContainerName = "Pool Container";

        [SerializeField] private int _preloadCount;
        [SerializeField] private Transform _container;

        private Dictionary<Type, Pool<IAttractable>> _pools = new Dictionary<Type, Pool<IAttractable>>();
        private Transform _selfTransform;

        public void Construct(IGameFactory gameFactory)
        {
            _selfTransform = transform;
            _container = new GameObject(PoolContainerName).transform;

            _pools = new Dictionary<Type, Pool<IAttractable>>
            {
                [typeof(Items.Carrot)] = new Pool<IAttractable>(
                    () =>
                    {
                        GameObject gameobject = gameFactory.CreateCarrotVisual(_selfTransform.position, Quaternion.identity, _container);
                        IAttractable attractable = gameobject.GetComponent<IAttractable>();
                        return attractable;
                    },
                    obj =>
                    {
                        obj.WasAttracted += OnWasCarrotAttracted;
                        obj.GameObject.Enable();
                    },
                    obj =>
                    {
                        obj.GameObject.Disable();
                        obj.WasAttracted -= OnWasCarrotAttracted;
                    },
                    _preloadCount),
                [typeof(Items.Coin)] = new Pool<IAttractable>(
                    () => gameFactory
                        .CreateMoneyVisual(_selfTransform.position, Quaternion.identity, _container)
                        .GetComponent<IAttractable>(),
                    obj =>
                    {
                        obj.WasAttracted += OnWasMoneyAttracted;
                        obj.GameObject.Enable();
                    },
                    obj =>
                    {
                        obj.GameObject.Disable();
                        obj.WasAttracted -= OnWasMoneyAttracted;
                    },
                    _preloadCount),
            };
        }

        private void OnWasCarrotAttracted(IAttractable attractable) =>
            Return(attractable, new Items.Carrot());

        private void OnWasMoneyAttracted(IAttractable attractable) =>
            Return(attractable, new Coin());

        public IAttractable Get<TItem>(TItem item) where TItem : IItem =>
            _pools[item.GetType()].Get();

        public void Return<TItem>(IAttractable obj, TItem item) =>
            _pools[item.GetType()].Return(obj);
    }
}