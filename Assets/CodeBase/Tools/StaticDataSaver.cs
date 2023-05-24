using System.Diagnostics;
using CodeBase.StaticData;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace CodeBase.Tools
{
    public class StaticDataSaver : MonoBehaviour
    {
        [SerializeField] private LocationStaticData _location;

        [Button("Save")]
        [Conditional("UNITY_EDITOR")]
        private void Save()
        {
            _location.Position = transform.position;
            _location.Rotation = transform.rotation;
            Debug.Log("Position saved");
        }
    }
}