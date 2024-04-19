using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TensionSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField] Image fillImage;
    [SerializeField] Gradient colorGradient;

    [SerializeField] Bow bow;

    void Start() {
        slider = GetComponent<Slider>();
        slider.maxValue = bow.maxTension;

        bow.OnTensionChanged += UpdateSliderValue;
    }

    void UpdateSliderValue(float newTension) {
        slider.value = newTension;
        fillImage.color = colorGradient.Evaluate(slider.normalizedValue);
    }
}
