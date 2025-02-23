using TMPro;
using UnityEngine;

public class ReceivedProgressView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _receivedProgressValue;

    private void Awake()
    {
        Disable();
    }

    public void UpdateReceivedBillsValue(int value)
    {
        _receivedProgressValue.text = value.ToString();
        Enable();
    }

    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}
