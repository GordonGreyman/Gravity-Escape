using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLights : MonoBehaviour
{
    private GameObject lamp;
    private Light _light;
    private float initialIntensity;
    private float minIntensity = 0.1f;
    private float maxIntensity = 1.0f;
    private float flickerDuration = 0.1f;
    private float minFlickerInterval = 0.1f;
    private float maxFlickerInterval = 1.0f; 

    void Start()
    {
        lamp = transform.gameObject;
        _light = lamp.transform.GetChild(0).GetComponent<Light>();
        initialIntensity = _light.intensity;
        StartCoroutine(FlickeryLights());
    }

    private IEnumerator FlickeryLights()
    {
        var localLamp = lamp.transform.GetComponent<Renderer>().material;

        var localColor = localLamp.GetColor("_EmissionColor");
        localLamp.color = localColor;
        localLamp.SetColor("_EmissionColor", localColor);
             

        while (true)
        {
            _light.intensity = Random.Range(minIntensity, maxIntensity);

            Color emissionColor = Color.Lerp(Color.black, localColor, _light.intensity / maxIntensity);
            localLamp.SetColor("_EmissionColor", emissionColor);

            yield return new WaitForSeconds(flickerDuration);

            _light.intensity = initialIntensity;
            localLamp.SetColor("_EmissionColor", localColor);


            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval));
        }
    }
}