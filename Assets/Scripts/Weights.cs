
using UnityEngine;

public class Weights : MonoBehaviour
{

    private Vector3 initialPos;
    private Rigidbody rb;


    void Start()
    {
        initialPos = transform.position;
        rb = transform.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        CheckForFall();
    }

    private void CheckForFall()
    {
        if (transform.position.y < -15)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = initialPos;

        }
    }

}
