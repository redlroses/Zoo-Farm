using UnityEngine;

public class InfiniteRotator : MonoBehaviour
{
    public Transform transformToRotate;
    [Range(1f, 100f)] public float RotationSpeed = 0.1f;
    public bool RotateOnStart = true;
    private bool isRotating;

    private void Start()
    {
        if (RotateOnStart)
            StartRotating();
    }

    private void Update()
    {
        if (isRotating)
        {
            float angle = RotationSpeed * Time.deltaTime;
            transformToRotate.Rotate(0f, 0f, angle);
        }
    }

    public void StartRotating() =>
        isRotating = true;

    public void StopRotating() =>
        isRotating = false;
}