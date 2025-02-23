using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class PickUpObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ProgressController controller))
            UpdateProgress(controller);
    }

    protected virtual void UpdateProgress(ProgressController controller)
    {
        Destroy(transform.parent.gameObject);
    }
}
