using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    public AudioClip audioClip;
    public float audioVolume;
    public bool isLoop;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = gameObject.AddComponent<AudioSource>();
        this.audioSource.clip = audioClip;
        this.audioSource.volume = audioVolume;
        this.audioSource.loop = isLoop;
        this.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
