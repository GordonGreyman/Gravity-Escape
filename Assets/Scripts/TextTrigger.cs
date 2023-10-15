using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public TextHandler[] textHandlers;
    public int textHandlerIndex;
    public int checkpointNumber;
    private bool triggeredOnce = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController") && !triggeredOnce)
        {
            triggeredOnce = true;
            textHandlers[textHandlerIndex].currentIndex = checkpointNumber;
            textHandlers[textHandlerIndex].textTriggered = true;
        }
    }

}
