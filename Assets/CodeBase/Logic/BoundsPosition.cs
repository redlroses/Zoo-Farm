using UnityEngine;

namespace CodeBase.Logic
{
    public class BoundsPosition : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boundCollider;

        public Vector3 GetRandomBoundsPosition()
        {
            Bounds randomField = _boundCollider.bounds;
            float randomX = Random.Range(randomField.min.x, randomField.max.x);
            float randomY = Random.Range(randomField.min.y, randomField.max.y);
            float randomZ = Random.Range(randomField.min.z, randomField.max.z);
            return new Vector3(randomX, randomY, randomZ);
        }
    }
}