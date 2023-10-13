using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutsceneEnter : MonoBehaviour
{
    public Transform destination;
    public Transform rotationTarget;

    public float moveSpeed = 4f;
    public float rotationSpeed = 10f;

    public EvolveGames.PlayerController controller;
    public static bool cutsceneStarted = false;
    public float stopDistance = 1f;

    public GameObject curtains;
    public Animation anim;

    public Image fadeIn;
    void Start()
    {
        controller.characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (cutsceneStarted)
        {

            if (!curtains.activeInHierarchy)
            {
                curtains.SetActive(true);
                anim.Play("Close Curtains");
            }


            float distanceToDestination = Vector3.Distance(transform.position, destination.position);

            if (distanceToDestination > stopDistance)
            {

                Vector3 newPosition = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
                controller.characterController.Move(newPosition - transform.position);

                Vector3 lookDirection = rotationTarget.position - transform.position;

                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                Vector3 newRotation = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
                transform.rotation = Quaternion.Euler(newRotation);
                cutsceneStarted = false;
                StartCoroutine(FadeIn());

            }
        }


    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3);

        fadeIn.gameObject.SetActive(true);
        float elapsedTime = 0;
        Color startColor = fadeIn.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); 

        while (elapsedTime < 1.0f) 
        {
            elapsedTime += Time.deltaTime;
            float percentage = elapsedTime / 1.0f; 
            fadeIn.color = Color.Lerp(startColor, targetColor, percentage);
            yield return null;
        }
        SceneManager.LoadScene("EndScene");
    }
}