using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PathCreation.Examples;
using System.Collections;

public class ProgressController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PathFollower _pathFollower;
    [SerializeField] private PlayerMover _playerMover;

    [Header("UI Components")]
    [SerializeField] private ProgressView _progressView;
    [SerializeField] private ReceivedBillsView _receivedBillsView;
    [SerializeField] private ReceivedBottleView _receivedBottleView;

    [Header("Properties")]
    [SerializeField] private List<GameObject> _skinsList;
    [SerializeField] private List<string> _skinsNames;

    private float _localProgress = 0.2f;
    private bool _isWin;
    private bool _isLose;
    private int _currentSkinIndex = -1;
    private float _progressStepValue = 0.05f;

    private int _tempReceivedBills = 0;
    private int _tempReceivedBottles = 0;
    private int _totalReceivedBills = 0;

    private Coroutine _resetBillsCoroutine;
    private Coroutine _resetBottlesCoroutine;

    private const string Progress = "Progress";
    private const string Win = "Win";
    private const string Lose = "Lose";

    private const float RegressStepValue = 0.2f;
    private const float MinProgressValue = 0;
    private const float MaxProgressValue = 1f;

    private void Awake()
    {
        if (_skinsList.Count < 5 || _skinsNames.Count < 5)
        {
            Debug.LogError("Недостаточно скинов или имен в списке!");
            return;
        }

        SetAnimState();
        UpdateProgressBar();
        UpdateSkin();
        _receivedBillsView.Disable();
        _receivedBottleView.Disable();
    }

    public void WinProgress()
    {
        _isWin = true;
        _pathFollower.DisableWalk();
        _playerMover.DisableInput();
        SetAnimState();
    }

    public void LoseProgress()
    {
        _isLose = true;
        _pathFollower.DisableWalk();
        _playerMover.DisableInput();
        SetAnimState();
    }

    public void ChangeProgress(bool isUp)
    {
        if (isUp)
        {
            _localProgress += _progressStepValue;
            _tempReceivedBills++;
            _totalReceivedBills++;

            Debug.Log($"Общее количество бумажных денег: {_totalReceivedBills}");
            _receivedBillsView.UpdateReceivedBillsValue(_tempReceivedBills);

            if (_resetBillsCoroutine != null)
                StopCoroutine(_resetBillsCoroutine);
            _resetBillsCoroutine = StartCoroutine(ResetReceivedBills());
        }
        else
        {
            _localProgress -= RegressStepValue;
            _tempReceivedBottles++;

            Debug.Log("Подобрана бутылка!");
            _receivedBottleView.UpdateReceivedBillsValue(_tempReceivedBottles);

            if (_resetBottlesCoroutine != null)
                StopCoroutine(_resetBottlesCoroutine);
            _resetBottlesCoroutine = StartCoroutine(ResetReceivedBottles());
        }

        _localProgress = Mathf.Clamp(_localProgress, MinProgressValue, MaxProgressValue);

        if (_localProgress <= MinProgressValue)
            LoseProgress();

        SetAnimState();
        UpdateProgressBar();
        UpdateSkin();
    }

    private IEnumerator ResetReceivedBills()
    {
        yield return new WaitForSeconds(1f);
        _tempReceivedBills = 0;
        _receivedBillsView.UpdateReceivedBillsValue(_tempReceivedBills);
        _receivedBillsView.Disable();
    }

    private IEnumerator ResetReceivedBottles()
    {
        yield return new WaitForSeconds(1f);
        _tempReceivedBottles = 0;
        _receivedBottleView.UpdateReceivedBillsValue(_tempReceivedBottles);
        _receivedBottleView.Disable();
    }

    public void DoubleProgressStep() =>
        _progressStepValue *= 2;

    private void SetAnimState()
    {
        _animator.SetFloat(Progress, _localProgress);
        _animator.SetBool(Win, _isWin);
        _animator.SetBool(Lose, _isLose);
    }

    private void UpdateProgressBar()
    {
        _progressView.UpdateProgressBar(_localProgress);
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

        string skinName = _skinsNames[newIndex];
        Debug.Log($"Текущий уровень: {skinName}");

        _progressView.UpdateSkinInfo(skinName, newIndex);
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
