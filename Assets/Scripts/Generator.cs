using UnityEngine;
using System.Collections;
using TMPro;

public class Generator : MonoBehaviour
{
    public GameObject generatorLight;
    public GameObject roomLight;
    public GameObject targetDoor;
    public TextMeshProUGUI screenText;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Key") {
            other.transform.gameObject.layer = 0;
            other.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.transform.rotation = Quaternion.Euler(90, 90, 0);
            other.GetComponent<Rigidbody>().freezeRotation = true;
            other.GetComponent<Rigidbody>().isKinematic = true;

            other.transform.position = new Vector3(41.638f, 5, -2);

            var light = generatorLight.transform.GetChild(0);
            light.GetComponent<Light>().color = Color.green;

            var lamp = generatorLight.transform.GetComponent<Renderer>().material;
            lamp.color = Color.green;
            lamp.SetColor("_EmissionColor", Color.green);


            StartCoroutine(LerpRoomLight());




            targetDoor.GetComponent<Doors>().playCloseAnim = false; 
            targetDoor.GetComponent<Doors>().playOpenAnim = true;

            screenText.text = "Door Opened";
        }
    }


    private IEnumerator LerpRoomLight()
    {
        yield return new WaitForSeconds(1);
        var roomLightObj = roomLight.transform.GetChild(0);
        var roomLampMaterial = roomLight.GetComponent<Renderer>().material;
        var _roomLight = roomLightObj.GetComponent<Light>();
        _roomLight.enabled = true;
        float elapsedTime = 0f;
        float duration = 2f; 

        while (elapsedTime < duration)
        {
            _roomLight.range = Mathf.Lerp(0, 30, elapsedTime / duration);
            
            roomLampMaterial.SetColor("_EmissionColor", Color.white * 1.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _roomLight.range = 30;
        roomLampMaterial.SetColor("_EmissionColor", Color.white * 1.5f);
    }
}
