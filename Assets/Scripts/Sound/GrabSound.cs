using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSound : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 1.0f;
        audioSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grab()
    {
        audioSource.Play();
    }
}
