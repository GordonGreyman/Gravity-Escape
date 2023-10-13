using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animation anim;
    public bool isOpen = true;
    public bool openPlayed = false;
    public bool closePlayed = true;
    public bool playOpenAnim = false;
    public bool playCloseAnim = false;
    private OcclusionPortal occlusionPortal;


    public AudioSource audioSource;
    public AudioClip[] audioClips = new AudioClip[2];


    private void Start()
    {
        anim = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();
        if(transform.GetComponent<OcclusionPortal>() != null)
            occlusionPortal = GetComponent<OcclusionPortal>();
    }

    private void Update()
    {

        if (!isOpen && !openPlayed && playOpenAnim)
        {
            if(occlusionPortal != null) occlusionPortal.open = true;
            anim.Play("Open " + transform.name);
            openPlayed = true;
            closePlayed = false;
            isOpen = true;
            audioSource.clip = audioClips[0];
            audioSource.Play();

        }
        else if (isOpen && !closePlayed && playCloseAnim)
        {
            if (occlusionPortal != null) occlusionPortal.open = false;
            anim.Play("Close " + transform.name);
            openPlayed = false;
            closePlayed = true;
            isOpen = false;
            audioSource.clip = audioClips[1];
            audioSource.Play();

        }
    }
}
