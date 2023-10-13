using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetLerp : MonoBehaviour
{
    private Transform jet;
    private Vector3 initialPos;

    void Start()
    {
        jet = transform;
        initialPos = jet.position;
        StartCoroutine(MoveJet());
    }

    private IEnumerator MoveJet()
    {
        yield return new WaitForSeconds(1);
        float startTime = Time.time;
        float duration = 5f;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            jet.position = Vector3.Lerp(initialPos, new Vector3(initialPos.x, initialPos.y, 150), t);
            yield return null;
        }


        yield return new WaitForSeconds(2f);


        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            jet.position = Vector3.Lerp(new Vector3(initialPos.x, initialPos.y, 150), new Vector3(initialPos.x, initialPos.y, 450), t);
            yield return null;
        }
    }
}