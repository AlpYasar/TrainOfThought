using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine.UI;
using NaughtyAttributes;
public class OptionsPanelController : MonoBehaviour
{
    [SerializeField, BoxGroup("Texts")] private TextMeshProUGUI trainCountText;
    [SerializeField, BoxGroup("Texts")] private TextMeshProUGUI trainSpeedText;
    [SerializeField, BoxGroup("Global Atom Params")] private IntVariable trainCount;
    [SerializeField, BoxGroup("Global Atom Params")] private FloatVariable trainSpeed;
    [SerializeField, BoxGroup("Sliders")] private Slider trainCountSlider;
    [SerializeField, BoxGroup("Sliders")] private Slider trainSpeedSlider;
    [SerializeField, BoxGroup("Panel")] private GameObject coverPanel;
    
    
    
    public void SetTrainCount(float count)
    {
        trainCount.Value = (int) count;
        trainCountText.text = count.ToString();
    }
    
    public void SetTrainSpeed(float speed)
    {
        trainSpeed.Value = speed;
        trainSpeedText.text = speed.ToString("F3", CultureInfo.InvariantCulture);
    }
    
    public void SetDefault(bool isDefault)
    {
        if (isDefault)
        {
            SetSliderValues();
            coverPanel.SetActive(true);
        }
        else
        {
            coverPanel.SetActive(false);
        }
    }

    
    private void SetSliderValues()
    {
        trainCountSlider.value = 30;
        trainSpeedSlider.value = 2.5f;
    }
    
}
