using System.Collections.Generic;
using CodeBase.Logic.Attractor;
using CodeBase.Logic.Inventory;
using CodeBase.Logic.Items;
using CodeBase.Logic.Pool;
using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.Logic.Interactions
{
    public class InventoryView : MonoCache
    {
        private const string AttractableLayerName = "Attractable";

        [SerializeField] private HeroPools _heroPools;
        [SerializeField] private Transform _onHandSpawnPlace;
        [SerializeField] private float _onHandOffset;
        [SerializeField] private Vector3 _onHandItemRotation;

        private List<KeyAttractedPair> _onHandItems = new List<KeyAttractedPair>();
        private int _attractedLayer;
        private IInventory _inventory;
        private Transform _selfTransform;

        private void OnDestroy()
        {
            _inventory.Replenished -= OnInventoryReplenished;
            _inventory.Spend -= OnInventorySpend;
        }

        public void Construct(IInventory inventory)
        {
            _selfTransform = transform;
            _inventory = inventory;
            _inventory.Replenished += OnInventoryReplenished;
            _inventory.Spend += OnInventorySpend;
            _attractedLayer = LayerMask.NameToLayer(AttractableLayerName);
        }

        private void OnInventorySpend(IReadOnlyInventoryCell cell)
        {
            if (cell.Item is IWeighty _)
            {
                GetOutOfHand(cell);
            }
            else
            {
                CreatePooled(cell);
            }
        }

        private void OnInventoryReplenished(IReadOnlyInventoryCell cell)
        {
            if (cell.Item is IWeighty)
            {
                GameObject itemObject = CreatePooled(cell);
                PutOnHands(cell, itemObject);
            }
        }

        private GameObject CreatePooled(IReadOnlyInventoryCell cell)
        {
            IAttractable itemObject = _heroPools.Get(cell.Item);
            itemObject.WasAttracted += () => _heroPools.Return(itemObject, cell.Item);
            itemObject.GameObject.transform.position = _selfTransform.position;
            return itemObject.GameObject;
        }

        private void GetOutOfHand(IReadOnlyInventoryCell cell)
        {
            KeyAttractedPair keyAttractedPair = _onHandItems.Find(pair => pair.type == cell.Item.GetType());
            keyAttractedPair.attracted.layer = _attractedLayer;
            keyAttractedPair.attracted.transform.parent = null;
            _onHandItems.Remove(keyAttractedPair);
        }

        private void PutOnHands(IReadOnlyInventoryCell cell, GameObject itemObject)
        {
            itemObject.layer = 0;
            itemObject.transform.SetParent(_onHandSpawnPlace);
            float itemOffset = (_onHandSpawnPlace.childCount - 1) * _onHandOffset;
            Vector3 itemPosition = new Vector3(0, itemOffset, 0);
            itemObject.transform.SetLocalPositionAndRotation(itemPosition, Quaternion.Euler(_onHandItemRotation));
            _onHandItems.Add(new KeyAttractedPair(cell.Item.GetType(), itemObject));
        }
    }
}