using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class TriggersHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ProgressController controller))
            DoAction(controller);
    }

    protected virtual void DoAction(ProgressController controller)
    {
    }
}
