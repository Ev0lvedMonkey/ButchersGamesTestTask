using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ProgressView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private Image _progressFill;
    [SerializeField] private List<Color> _progressColors; 

    public void UpdateProgressBar(float progress)
    {
        _progressFill.fillAmount = progress;

        int colorIndex = Mathf.Clamp(Mathf.FloorToInt(progress * _progressColors.Count), 0, _progressColors.Count - 1);
        _progressFill.color = _progressColors[colorIndex];
    }

    public void UpdateSkinInfo(string skinName, int skinIndex)
    {
        _statusText.text = $"{skinName}";
        _statusText.color = _progressColors[skinIndex];
    }
}
