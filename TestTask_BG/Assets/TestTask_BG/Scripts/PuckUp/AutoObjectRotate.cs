using UnityEngine;

public class AutoObjectRotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;
    [SerializeField] private float _rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(_rotationAxis * _rotationSpeed * Time.deltaTime);
    }
}
