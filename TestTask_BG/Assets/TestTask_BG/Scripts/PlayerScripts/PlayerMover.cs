using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _limitValue;
    [SerializeField] private AudioSource _source;

    private bool _canInput;
    private Coroutine _soundCoroutine;

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

        if (_soundCoroutine == null)
            _soundCoroutine = StartCoroutine(PlaySound());
    }

    public void DisableInput()
    {
        _canInput = false;
        if (_soundCoroutine != null)
        {
            StopCoroutine(_soundCoroutine);
            _soundCoroutine = null;
        }
    }

    public void EnableInput()
    {
        _canInput = true;
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
            _source.pitch = Random.Range(0.9f, 1.2f);
            _source.Play();
            yield return new WaitForSeconds(0.7f);
        }
    }
}
