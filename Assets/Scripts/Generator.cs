using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : MonoBehaviour
{
    public GameObject generatorLight;
    public GameObject targetDoor;
    public TextMeshProUGUI screenText;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            light.transform.GetComponent<Light>().color = Color.green;

            var lamp = generatorLight.transform.GetComponent<Renderer>().material;
            lamp.color = Color.green;
            lamp.SetColor("_EmissionColor", Color.green);

            targetDoor.GetComponent<Doors>().playCloseAnim = false; 
            targetDoor.GetComponent<Doors>().playOpenAnim = true;

            screenText.text = "Door Opened";
        }
    }
}
