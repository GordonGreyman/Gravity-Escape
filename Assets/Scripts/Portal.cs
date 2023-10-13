using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EvolveGames;

public class Portal : MonoBehaviour
{
    public GameObject pairTeleportPad;
    public GameObject targetTeleportPos;
    public bool recentlyTeleported;
    public EvolveGames.PlayerController controller;

    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 1 && recentlyTeleported == false)
        {

            other.gameObject.transform.position = targetTeleportPos.transform.position;

            Vector3 throwDirection = pairTeleportPad.transform.forward;
            
            if (other.gameObject.GetComponent<Rigidbody>().velocity.magnitude == 0)
            {
                int baseThrowSpeed = 8;
                other.gameObject.GetComponent<Rigidbody>().velocity = throwDirection * baseThrowSpeed;

                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
        
                float objectSpeed = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
                int baseThrowSpeed = 8;
                float speedToApply = Mathf.Max(objectSpeed, baseThrowSpeed);

                other.gameObject.GetComponent<Rigidbody>().velocity = throwDirection * speedToApply;

                audioSource.clip = audioClip;
                audioSource.Play();

            }
            pairTeleportPad.GetComponent<Portal>().recentlyTeleported = true;
        }

        if (other.gameObject.CompareTag("GameController") && recentlyTeleported == false)
        {
            controller.characterController.enabled = false;

            Vector3 newEulerAngles = new Vector3(
            other.gameObject.transform.rotation.eulerAngles.x,
            pairTeleportPad.transform.rotation.eulerAngles.y,
            other.gameObject.transform.rotation.eulerAngles.z
        );


            other.gameObject.transform.rotation = Quaternion.Euler(newEulerAngles);
            other.gameObject.transform.position = targetTeleportPos.transform.position;

            controller.characterController.enabled = true;
            pairTeleportPad.GetComponent<Portal>().recentlyTeleported = true;

            audioSource.clip = audioClip;
            audioSource.Play();

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") || other.gameObject.layer == 1)
        {
            recentlyTeleported = false;

        }
    }


}
