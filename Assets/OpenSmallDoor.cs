using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSmallDoor : MonoBehaviour
{
    public GameObject openRotObj;

    //Vector3 closedRotation;

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

        //closedRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate && !isDoorOpened)
            Open();
    }

    void Open()
    {
        if (!isSoundPlayed)
        {
            audioSource.Play();
            isSoundPlayed = true;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, openRotObj.transform.rotation, 0.03f);

        if (transform.rotation == openRotObj.transform.rotation)
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
