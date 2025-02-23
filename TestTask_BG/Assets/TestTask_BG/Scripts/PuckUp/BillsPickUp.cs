using System.Collections;
using UnityEngine;

public class BillsPickUp : TriggersHandler
{
    [SerializeField] private AudioSource _source;

    protected override void DoAction(ProgressController controller)
    {
        controller.ChangeProgress(true);
        _source.Play();
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); 
        Destroy(transform.parent.gameObject);
    }
}
