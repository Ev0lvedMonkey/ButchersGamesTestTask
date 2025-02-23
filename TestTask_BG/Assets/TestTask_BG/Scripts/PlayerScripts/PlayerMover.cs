using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _limitValue;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            Move();
    }

    private void Move()
    {
        float halfScreen = Screen.width / 2;
        float xPosition = (Input.mousePosition.x - halfScreen) / halfScreen;
        float finalPosition = Mathf.Clamp(xPosition * _limitValue, -_limitValue, _limitValue);
        _playerTransform.localPosition = new(finalPosition, 0, 0);
    }
}
