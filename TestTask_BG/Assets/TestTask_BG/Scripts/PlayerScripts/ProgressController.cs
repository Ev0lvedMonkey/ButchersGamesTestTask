using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation.Examples;

[RequireComponent(typeof(PathFollower))]
public class ProgressController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<GameObject> _skinsList;
    [SerializeField] private List<string> _skinsNames; 

    private PathFollower _pathFollower;
    private float _localProgress = 1f; 
    private bool _isWin;
    private bool _isLose;
    private int _currentSkinIndex = -1;

    private const string Progress = "Progress";
    private const string Win = "Win";
    private const string Lose = "Lose";

    private const float MinProgressValue = 0;
    private const float MaxProgressValue = 1f;
    private const float ProgressStepValue = 0.01f;

    private void Awake()
    {
        if (_skinsList.Count < 5)
        {
            Debug.LogError("_skinsList < 5!");
            return;
        }

        if (_skinsNames.Count != _skinsList.Count)
        {
            Debug.LogError("Количество имен (_skinsNames) не совпадает с количеством скинов (_skinsList)!");
            return;
        }

        _pathFollower = GetComponent<PathFollower>();
        SetAnimState();
        UpdateSkin();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            ChangeProgress(true);
        if (Input.GetKey(KeyCode.S))
            ChangeProgress(false);
        if (Input.GetKeyDown(KeyCode.R))
            WinProgress();
        if (Input.GetKeyDown(KeyCode.T))
            LoseProgress();
    }

    public void WinProgress()
    {
        _isWin = true;
        _pathFollower.DisableWalk();
        SetAnimState();
    }

    public void LoseProgress()
    {
        _isLose = true;
        _pathFollower.DisableWalk();
        SetAnimState();
    }

    public void ChangeProgress(bool isUp)
    {
        _localProgress += isUp ? ProgressStepValue : -ProgressStepValue;
        _localProgress = Mathf.Clamp(_localProgress, MinProgressValue, MaxProgressValue);

        if (_localProgress <= MinProgressValue)
        {
            LoseProgress();
            return;
        }

        SetAnimState();
        UpdateSkin();
    }

    private void SetAnimState()
    {
        _animator.SetFloat(Progress, _localProgress);
        _animator.SetBool(Win, _isWin);
        _animator.SetBool(Lose, _isLose);
    }

    private void UpdateSkin()
    {
        int newIndex = GetSkinIndex();

        if (newIndex == _currentSkinIndex)
            return;

        _currentSkinIndex = newIndex;

        for (int i = 0; i < _skinsList.Count; i++)
        {
            _skinsList[i].SetActive(i == newIndex);
        }

        Debug.Log($"Текущий уровень: {_skinsNames[newIndex]}"); 

        RotateSkin();
    }

    private int GetSkinIndex()
    {
        if (_localProgress < 0.2f) return 0;
        if (_localProgress < 0.4f) return 1;
        if (_localProgress < 0.6f) return 2;
        if (_localProgress < 0.8f) return 3;
        return 4;
    }

    private void RotateSkin()
    {
        Transform obj = transform.GetChild(0);

        Vector3 startRotation = obj.rotation.eulerAngles;

        obj.DORotate(startRotation + new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => obj.rotation = Quaternion.Euler(startRotation));
    }
}
