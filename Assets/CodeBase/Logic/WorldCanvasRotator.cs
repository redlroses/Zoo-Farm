using NTC.Global.Cache;
using UnityEngine;

namespace CodeBase.Logic
{
    public class WorldCanvasRotator : MonoCache
    {
        [SerializeField] private Canvas _canvas;

        private Transform _canvasTransform;
        private Transform _cameraTransform;

        private void Awake()
        {
            _canvasTransform = _canvas.transform;
            Camera worldCamera = Camera.main;
            _cameraTransform = worldCamera.transform;
            _canvas.worldCamera = worldCamera;
            _canvasTransform.forward = _cameraTransform.forward;
        }
    }
}