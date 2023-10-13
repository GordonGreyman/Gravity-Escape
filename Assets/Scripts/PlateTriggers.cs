using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlateTriggers : MonoBehaviour
{
    public GameObject roomLight;
    float duration = 1.0f;
    private Color red = Color.red;
    private Color green = Color.green;
    private Color black = Color.black;
    private Vector3 initialPos;
    public GameObject targetDoor;
    public int requiredMass;
    private int totalMass = 0;
    private int collisionCount = 0;

    private bool isColliding = false;

    public TextMeshProUGUI screenText;
    private string initialScreenText;

    public List<GameObject> weights = new List<GameObject>();
    public List<GameObject> weightsToAdd = new List<GameObject>();

    void Start()
    {   if(screenText != null && screenText.name.Contains("Door"))
            initialScreenText = screenText.text;
        initialPos = transform.position;
    }

    void Update()
    {
        CheckForFall();

        if (totalMass >= requiredMass)
        {
            targetDoor.GetComponent<Doors>().playCloseAnim = false;
            targetDoor.GetComponent<Doors>().playOpenAnim = true;

            if (screenText != null && screenText.name.Contains("Door"))
                screenText.text = "Door opened";

            if (roomLight != null)
            {
                float t = Mathf.PingPong(Time.time, duration) / duration;

                var light = roomLight.transform.GetChild(0);
                light.GetComponent<Light>().color = Color.Lerp(green, black, t);

                var lamp = roomLight.transform.GetComponent<Renderer>().material;
                Color newEmissionColor = Color.Lerp(green, black, t);
                lamp.SetColor("_EmissionColor", newEmissionColor);
                lamp.color = Color.Lerp(green, black, t);


            }
        }

        if (totalMass < requiredMass)
        {
            targetDoor.GetComponent<Doors>().playCloseAnim = true;
            targetDoor.GetComponent<Doors>().playOpenAnim = false;

            if (screenText != null && screenText.name.Contains("Door"))
                    screenText.text = initialScreenText;

            if (roomLight != null)
            {
                float t = Mathf.PingPong(Time.time, duration) / duration;

                var light = roomLight.transform.GetChild(0);
                light.GetComponent<Light>().color = Color.Lerp(red, black, t);

                var lamp = roomLight.transform.GetComponent<Renderer>().material;
                Color newEmissionColor = Color.Lerp(red, black, t);
                lamp.SetColor("_EmissionColor", newEmissionColor);
                lamp.color = Color.Lerp(green, black, t);


            }
        }

        isColliding = false; 

        foreach (GameObject obj in weights)
        {
            CheckCollision(obj);
            if (CheckCollision(obj))
            {
                isColliding = true;
            }
        }

        if (!isColliding && collisionCount > 0)
        {

            foreach (GameObject a in weightsToAdd)
            {
                totalMass -= (int)a.GetComponent<Rigidbody>().mass;
                weights.Remove(a);

                if (screenText != null && screenText.name.Contains("Trigger"))
                {
                    screenText.text = totalMass.ToString() + " kg";
                }
            }

            collisionCount = 0; 
            weightsToAdd.Clear();
        }

        foreach (GameObject obj in weightsToAdd)
        {
            if (!weights.Contains(obj))
            {
                weights.Add(obj);
                totalMass += (int)obj.GetComponent<Rigidbody>().mass;

                if(screenText != null && screenText.name.Contains("Trigger"))
                {
                    screenText.text = totalMass.ToString() + " kg";
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 1 || other.gameObject.layer == 2)
        {
            weights.Add(other.gameObject);
            totalMass += (int)other.gameObject.GetComponent<Rigidbody>().mass;

            if(screenText != null && screenText.name.Contains("Trigger"))
            {
                screenText.text = totalMass.ToString() + " kg";
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 1 || other.gameObject.layer == 2)
        {
            totalMass -= (int)other.gameObject.GetComponent<Rigidbody>().mass;
            weights.Remove(other.gameObject);

            if (screenText != null && screenText.name.Contains("Trigger"))
            {
                screenText.text = totalMass.ToString() + " kg";
            }
        }
    }

    private void CheckForFall()
    {
        if (transform.position.y < -3)
        {
            transform.position = initialPos;
        }
    }

    private bool CheckCollision(GameObject obj)
    {
        RaycastHit hit;
        float rayLength = 1.0f;

        if (Physics.Raycast(obj.transform.position, Vector3.up, out hit, rayLength, 2))
        {
            if (!weightsToAdd.Contains(hit.transform.gameObject))
            {
                weightsToAdd.Add(hit.transform.gameObject);
                collisionCount += 1;
            }
            return true;
        }

        return false;
    }
}