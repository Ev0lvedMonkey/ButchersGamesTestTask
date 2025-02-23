using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _limitValue;

    private bool _canInput;

    private void Awake()
    {
        EnableInput();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Move();
    }

    private void Move()
    {
        if (!_canInput) return;
        float halfScreen = Screen.width / 2;
        float xPosition = (Input.mousePosition.x - halfScreen) / halfScreen;
        float finalPosition = Mathf.Clamp(xPosition * _limitValue, -_limitValue, _limitValue);
        _playerTransform.localPosition = new(finalPosition, 0, 0);
    }

    public void DisableInput()
    {
        _canInput = false;
    } 

    public void EnableInput()
    {
        _canInput = true;
    }
}
