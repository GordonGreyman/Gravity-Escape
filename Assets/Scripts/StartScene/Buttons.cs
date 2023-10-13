using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Buttons : MonoBehaviour
{

    public Button setBttn;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    private void Update()
    {
        if(!mainMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)){
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OpenSettingsMenu()
    {

        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

    }



    public void BackToMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }



}
