using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject openPosObj;

    //Vector3 closedPos;

    bool isDoorOpened = false;
    bool activate = false;

    bool isSoundPlayed = false;

    public AudioClip audioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 1.0f;

        //closedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate && !isDoorOpened)
            Open();
    }

    void Open()
    {
        if(!isSoundPlayed)
        {
            audioSource.Play();
            isSoundPlayed = true;
        }
            
        transform.position = Vector3.Lerp(transform.position, openPosObj.transform.position, 0.01f);
        
        if(transform.position == openPosObj.transform.position)
        {
            audioSource.Stop();
            isDoorOpened = true;
        }
    }

    public void ButtonClicked()
    {
        activate = true;
    }
}
