using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public Settings settings;
    public Image fadeColor;
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Dropdown resDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullScreenToggle;
    public AudioSource[] audioSources;
    public GameObject[] textObjs;
    private List<GameObject> openTextObjs = new List<GameObject>();

    private enum FadeState { Inactive, FadingIn, FadingOut }
    private FadeState fadeState = FadeState.Inactive;
    private bool inSettingsMenu;

    public GameObject escapeMenu;
    public GameObject settingsMenu;

    



    private void Start()
    {
        if (Settings.currentVolume != 0 && Settings.currentVolumePercentage != 0)
        {
            volumeSlider.value = Settings.currentVolumePercentage;
            audioMixer.SetFloat("volume", Settings.currentVolume);
        }


    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (fadeState == FadeState.Inactive)
            {
               foreach(AudioSource audio in audioSources)
               {
                    audio.Pause();
               }

               foreach(GameObject textObj in textObjs)
               {
                    if (textObj.activeInHierarchy)
                    {
                        textObj.SetActive(false);
                        openTextObjs.Add(textObj);
                    }
               }
                StartCoroutine(FadeIn());
            }

            else if (inSettingsMenu)
            {
                inSettingsMenu = false;
                ReturnToEscapePanel();

            }

            else if (fadeState == FadeState.FadingOut && !inSettingsMenu)
            {
                audioMixer.SetFloat("volume", Settings.currentVolume);

                foreach (AudioSource audio in audioSources)
                {
                    if(audio.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                        audio.Play();
                }

                foreach (GameObject textObj in openTextObjs)
                {
                    textObj.SetActive(true);
                    
                }
                openTextObjs.Clear();

                StartCoroutine(FadeOut());
            }
        }

    }

    private IEnumerator FadeIn()
    {
        fadeColor.gameObject.SetActive(true);
        fadeState = FadeState.FadingIn;
        float elapsedTime = 0;
        Color startColor = fadeColor.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        while (elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / 0.2f;
            fadeColor.color = Color.Lerp(startColor, targetColor, percentage);
            yield return null;
        }

        Time.timeScale = 0;
        escapeMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fadeState = FadeState.FadingOut;
    }

    public void CallFadeOut()
    {

        audioMixer.SetFloat("volume", Settings.currentVolume);

        foreach (AudioSource audio in audioSources)
        {
            if (audio.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                audio.Play();
        }

        foreach (GameObject textObj in openTextObjs)
        {
            textObj.SetActive(true);
        }
        openTextObjs.Clear();

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        fadeState = FadeState.FadingOut;
        Time.timeScale = 1;
        escapeMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        float elapsedTime = 0;
        Color startColor = fadeColor.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (elapsedTime < 0.2f)
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / 0.2f;
            fadeColor.color = Color.Lerp(startColor, targetColor, percentage);
            yield return null;
        }

        fadeState = FadeState.Inactive;
        fadeColor.gameObject.SetActive(false);

    }


    public void OpenSettingsMenu()
    {
        inSettingsMenu = true;
        escapeMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ReturnToEscapePanel()
    {
        inSettingsMenu = false;
        settingsMenu.SetActive(false);
        escapeMenu.SetActive(true);

    }

    public void ExitToMainMenu()
    {
        Settings.isFullScreenBool = fullScreenToggle.isOn;
        Settings.currentQuality = qualityDropdown.value;
        Settings.currentRes = resDropdown.value;
        Settings.currentVolumePercentage = volumeSlider.value;
        audioMixer.SetFloat("volume", Settings.currentVolume);
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene");
    }
}