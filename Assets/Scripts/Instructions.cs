using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public List<string> texts = new List<string>();
    public Text currentText;
    public int currentTextIndex = 0;

    private const float initialDelay = 0.5f;
    private const float reenableDelay = 1.5f;
    private int stopIndex;

    private void Start()
    {
        stopIndex = texts.Count;
        currentText = GetComponent<Text>();
        Shoot.OnCall += DisplayText;
    }

    public void DisplayText()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(initialDelay);

        currentText.enabled = false;



        yield return new WaitForSeconds(reenableDelay);
        currentText.enabled = true;

        if (currentTextIndex <= stopIndex) {
            currentTextIndex++;
            if (currentTextIndex != stopIndex)
            {
                currentText.text = texts[currentTextIndex];
            }
            else
            {
                Shoot.OnCall -= DisplayText;
                currentText.gameObject.SetActive(false);
            }
        }
    }
}