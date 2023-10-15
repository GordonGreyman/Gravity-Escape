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

    private void FixedUpdate()
    {
        if (currentTextIndex < texts.Count)
        {
            if (currentText.text != texts[currentTextIndex])
            {
                currentText.text = texts[currentTextIndex];
            }
        }
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

        if (currentTextIndex < texts.Count)
            currentTextIndex++;

        if (currentTextIndex == stopIndex)
        {
            Shoot.OnCall -= DisplayText;
            currentText.gameObject.SetActive(false);
        }
    }
}