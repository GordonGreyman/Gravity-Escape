using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weights : MonoBehaviour
{

    private Vector3 initialPos;
    public bool onTrigger = false;


    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFall();
    }

    private void CheckForFall()
    {
        if (transform.position.y < -15)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.position = initialPos;

        }
    }

}
