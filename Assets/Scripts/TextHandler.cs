using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public List<string> textList;

    private float letterDelay = 0.08f;
    public Text textComponent;
    public int currentIndex = 0;
    public enum TextType { Unfold, Full }
    public TextType textType;
    private Color textColor;
    public bool textTriggered;
    public GameObject instructions;

    private void Start()
    {
        if(instructions!= null)
            StartCoroutine(StartInstructions());
    }
    void Update()
    {
        if (textTriggered)
        {

            if (textType == TextType.Unfold)
            {
                textTriggered = false;
                StartCoroutine(UnfoldText(textList[currentIndex]));
            }
            else
            {
                textTriggered = false;
                StartCoroutine(FullText(textList[currentIndex]));

            }

        }
    }


    public IEnumerator UnfoldText(string currentText)
    {
        textComponent.gameObject.SetActive(true);

        for (int i = 0; i <= currentText.Length; i++)
        {
            string displayedText = currentText.Substring(0, i);
            textComponent.text = displayedText;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(3);

        textComponent.gameObject.SetActive(false);
    }


    public IEnumerator FullText(string currentText)
    {

        textComponent.gameObject.SetActive(true);

        textComponent.text = currentText;
        textColor = textComponent.color;

        float elapsedTime = 0.0f;
        float startAlpha = 0.0f;
        float endAlpha = 1.0f;


        while (elapsedTime < 2.0f)
        {
            float alphaValue = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 2.0f);
            textColor.a = alphaValue;
            textComponent.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        textColor.a = endAlpha;
        textComponent.color = textColor;

        yield return new WaitForSeconds(10); 



        elapsedTime = 0.0f;
        startAlpha = 1.0f;
        endAlpha = 0.0f;

        while (elapsedTime < 2.0f)
        {
            float alphaValue = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 2.0f);
            textColor.a = alphaValue;
            textComponent.color = textColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        textColor.a = endAlpha;
        textComponent.color = textColor;
        textComponent.gameObject.SetActive(false);
    }

    private IEnumerator StartInstructions()
    {
        yield return new WaitForSeconds(3);
        instructions.SetActive(true);
    }



}
