using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject carriedObject;
    public GameObject carryLocationObject;
    private RaycastHit hit;
    private Rigidbody rb;


    public float rayDistance = 15f;
    public LayerMask targetLayer;
    public GameObject player;
    private LineRenderer lineRenderer;


    public AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[3];
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        

            ShootRay();
            CarryTarget();
            if(carriedObject != null && Input.GetMouseButtonDown(0))
            {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                lineRenderer.enabled = false;

                Vector3 throwDirection = player.transform.forward;
                int throwSpeed =20;

                carriedObject.GetComponent<Rigidbody>().velocity = throwDirection * throwSpeed;

                audioSource.clip = audioClips[2];
                audioSource.Play();


                carriedObject = null;

            }

            if (carriedObject != null && Input.GetMouseButtonUp(1))
            {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                lineRenderer.enabled = false;

                audioSource.clip = audioClips[1];
                audioSource.Play();

                carriedObject = null;
            }

            if (carriedObject != null && rayDistance +1f < Vector3.Distance(transform.position, carriedObject.transform.position))
            {

                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                lineRenderer.enabled = false;


                audioSource.clip = audioClips[1];
                audioSource.Play();
                carriedObject = null;
            }
        
    }

    private void ShootRay()
    {
        if (carriedObject != null || !Input.GetMouseButtonDown(1))
        {
            return;
        }

        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, targetLayer))
        {
            rb = hit.transform.GetComponent<Rigidbody>();
            if (rb)
            {
                if (!IsObstructed(rayOrigin, hit.point))
                {
                    rb.useGravity = false;
                    rb.freezeRotation = true;
                    rb.velocity = Vector3.zero;

                    carriedObject = hit.transform.gameObject;
                    audioSource.clip = audioClips.Length > 0 ? audioClips[0] : null;
                    audioSource.Play();
                }
            }
        }
    }

    private bool IsObstructed(Vector3 start, Vector3 end)
    {
        RaycastHit obstructionHit;
        return Physics.Linecast(start, end, out obstructionHit, ~targetLayer);
    }

    private void CarryTarget()
    {
        if (carriedObject != null ) {



            if (carriedObject.transform.gameObject.layer != 1)
            {
                rb.useGravity = true;
                rb.freezeRotation = false;
                lineRenderer.enabled = false;

                carriedObject = null;

            }
            else
            {
                Vector3 direction = (carryLocationObject.transform.position - carriedObject.transform.position);
                float distance = Vector3.Distance(carriedObject.transform.position, carryLocationObject.transform.position);

                if (Physics.Raycast(carriedObject.transform.position, direction, out hit, distance))
                {
                    float elapsedTime = 0;
                    elapsedTime += Time.deltaTime;
                    float percentage = elapsedTime / .1f;

                    rb.MovePosition ( Vector3.Slerp(carriedObject.transform.position, hit.point - direction * 0.1f , percentage));
                    lineRenderer.SetPositions(new Vector3[] { transform.position, carriedObject.transform.position });
                    lineRenderer.enabled = true;
                }
                else
                {
                    float elapsedTime = 0;
                    elapsedTime += Time.deltaTime;
                    float percentage = elapsedTime / .15f;

                    carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, carryLocationObject.transform.position, percentage);
                    lineRenderer.SetPositions(new Vector3[] { transform.position, carriedObject.transform.position });
                    lineRenderer.enabled = true;
                }
            }
        }
    }
}
