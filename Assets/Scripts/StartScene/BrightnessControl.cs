using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessControl : MonoBehaviour
{
    public Slider ambientSlider; // Reference to the UI slider for controlling ambient intensity

    private const string AmbientIntensityKey = "AmbientIntensity";

    private void Start()
    {
        // Add a listener to the slider to respond to value changes
        ambientSlider.onValueChanged.AddListener(ChangeAmbientIntensity);

        // Load the saved ambient intensity setting
        float savedIntensity = PlayerPrefs.GetFloat(AmbientIntensityKey, 1.0f); // Default to 1.0 if not found
        ambientSlider.value = savedIntensity;
        RenderSettings.ambientIntensity = savedIntensity;
    }

    private void ChangeAmbientIntensity(float value)
    {
        // Set the ambient intensity based on the slider value
        RenderSettings.ambientIntensity = value;

        // Save the ambient intensity setting
        PlayerPrefs.SetFloat(AmbientIntensityKey, value);
        PlayerPrefs.Save();
    }
}