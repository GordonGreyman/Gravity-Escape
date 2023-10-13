using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{



    public Slider volSlider;
    public AudioMixer audioMixer;
    private AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip hoverSound;


    public static float currentVolume;
    public static float currentVolumePercentage;


    public static int currentRes;

    public static int currentQuality = 5;

    public static bool isFullScreenBool = true;

    Resolution[] resolutions;
    public Dropdown resoulutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        

        if (currentVolumePercentage != 0)
        {
            volSlider.value = currentVolumePercentage;
        }

        resolutions = Screen.resolutions;

        resoulutionDropdown.ClearOptions();

        List<string> resList = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resList.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResIndex = i;
            }
        }
        resoulutionDropdown.AddOptions(resList);
        resoulutionDropdown.value = currentResIndex;
        resoulutionDropdown.RefreshShownValue();

        resoulutionDropdown.value = currentRes;

        qualityDropdown.value = currentQuality;

        fullscreenToggle.isOn = isFullScreenBool;

    }

    public void SetSound(float vol)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(vol) * 20);
        currentVolume = Mathf.Log10(vol) * 20;
        currentVolumePercentage = volSlider.value;

    }

    public void SetQuality(int quality)
    {
        currentQuality = quality;
        QualitySettings.SetQualityLevel(currentQuality);

    }

    public void SetResolution(int res)
    {
        Resolution resolution = resolutions[res];
        currentRes = res;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        isFullScreenBool = isFullscreen;
        Screen.fullScreen = isFullScreenBool;
    }


    public void PlayHoverSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClickSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clickSound);
    }

}
