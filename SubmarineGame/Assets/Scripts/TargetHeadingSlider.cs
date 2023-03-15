using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TargetHeadingSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    private static Slider staticSlider;
    void Start()
    {
        staticSlider = slider;
        sliderText.text = staticSlider.value.ToString("0");

        slider.onValueChanged.AddListener((v) => {
            sliderText.text = v.ToString("0");
        });
    }

    public static void onRelease() {
        SubmarineMovements.headingChangeRequest(staticSlider.value);
    }
}
