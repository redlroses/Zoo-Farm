using UnityEngine;

namespace CodeBase.Logic
{
    public class WorldCanvasRotator : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
            
        }
    }
}