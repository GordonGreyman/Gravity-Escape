using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextUnfold : MonoBehaviour
{
    public List<string> textList;

    private float letterDelay = 0.15f; 
    private Text textComponent;
    private int currentIndex = 0;
    private Color textColor;
    private bool lastTextDisplayed = false;

    public Image fadeOut;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        textComponent = GetComponent<Text>();
        textColor = textComponent.color;
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        if (lastTextDisplayed)
        {
            float alphaValue = Mathf.PingPong(Time.time, 1f);
            textColor.a = alphaValue;
            textComponent.color = textColor;
        }

        if (lastTextDisplayed && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("StartScene");
        }
    }

    private IEnumerator ShowText(string currentText)
    {
        for (int i = 0; i <= currentText.Length; i++)
        {
            string displayedText = currentText.Substring(0, i);
            textComponent.text = displayedText;
            yield return new WaitForSeconds(letterDelay);
        }

        if (currentIndex == textList.Count - 1)
        {
            lastTextDisplayed = true;
        }

        yield return new WaitForSeconds(2);

        currentIndex++;

        if (currentIndex < textList.Count)
        {
            textComponent.text = ""; 
            StartCoroutine(ShowText(textList[currentIndex])); 
        }
    }


    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        Color targetColor = new Color(0, 0, 0, 0);

        while (elapsedTime < 2.0f) 
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / 2.0f; 
            fadeOut.color = Color.Lerp(new Color(0,0,0,1), targetColor, percentage);
            yield return null;
        }

        fadeOut.gameObject.SetActive(false);

        if (textList.Count > 0)
        {
            StartCoroutine(ShowText(textList[currentIndex]));
        }

    }
}
