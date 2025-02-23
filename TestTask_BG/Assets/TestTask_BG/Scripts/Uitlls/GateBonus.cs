using DG.Tweening;
using System;
using TMPro.EditorUtilities;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GateBonus : TriggersHandler
{
    [Header("Comnponents")]
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private GameObject _leftDoor;

    [Header("Properties")]
    [SerializeField, Range(0, 10)] private int _multiBonus;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private bool _isFinal;
    [SerializeField] private SkinType _type;


    protected override void DoAction(ProgressController controller)
    {
        if (_isFinal)
        {
            controller.WinProgress();
            return;
        }

        controller.MultiBonusProgressStep(_multiBonus);
        SkinType tempType = controller.GetSkinType();
        Debug.Log($"{_type} {tempType}");
        if (_type < tempType)
        {
            OpenDoors();
        }
        else
            controller.WinProgress();

    }

    private void OpenDoors()
    {
        if (_leftDoor == null || _rightDoor == null)
            return;
        _leftDoor.transform.DORotate(new Vector3(0, 180, 0), _duration, RotateMode.WorldAxisAdd)
            .SetEase(Ease.OutQuad);

        _rightDoor.transform.DORotate(new Vector3(0, -180, 0), _duration, RotateMode.WorldAxisAdd)
            .SetEase(Ease.OutQuad);
    }
}
