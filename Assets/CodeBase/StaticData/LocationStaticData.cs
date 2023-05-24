using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "DefaultPositionsStaticData", menuName = "Static Data/DefaultPositionsStaticData")]
    public class LocationStaticData : ScriptableObject
    {
        public LocationKey Key;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}