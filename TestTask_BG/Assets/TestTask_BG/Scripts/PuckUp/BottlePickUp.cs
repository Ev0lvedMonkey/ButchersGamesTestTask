using System.Collections;
using UnityEngine;

public class BottlePickUp : TriggersHandler
{
    [SerializeField] private AudioSource _source;

    protected override void DoAction(ProgressController controller)
    {
        _source.Play();
        controller.ChangeProgress(false);
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(transform.parent.gameObject);
    }
}

