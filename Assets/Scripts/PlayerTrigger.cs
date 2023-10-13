using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject targetDoor;
    public bool isUse = false;
    public bool isCutScene = false;
    public bool isDoorOpen = true;
    private bool canInteract;
    private Doors doorScript;
    private bool interactKeyPressed = false;
    public AudioSource targetAudioSource;


    void Start()
    {
        if(targetDoor != null)
            doorScript = targetDoor.GetComponent<Doors>();
    }


    void Update()
    {
        StartCoroutine(Interact());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("GameController") && isDoorOpen)
        {
            doorScript.playCloseAnim = false;
            doorScript.playOpenAnim = true;
        }

        if(collision.gameObject.CompareTag("GameController") && isCutScene)
        {
            CutsceneEnter.cutsceneStarted = true;
        }
        canInteract = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("GameController") && isDoorOpen)
        {
            doorScript.playCloseAnim = true;
            doorScript.playOpenAnim = false;
        }
        canInteract = false;
    }



    private IEnumerator Interact()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E) && !interactKeyPressed)
        {
            interactKeyPressed = true;
            doorScript.playCloseAnim = !doorScript.playCloseAnim;
            doorScript.playOpenAnim = !doorScript.playOpenAnim;
            if (targetAudioSource != null && !targetAudioSource.isPlaying)
            {
                targetAudioSource.gameObject.layer = LayerMask.NameToLayer("Default");
                targetAudioSource.Play();

                Debug.Log("?");
            }


            yield return new WaitForSeconds(1);
            interactKeyPressed = false;
        }
    }
}
