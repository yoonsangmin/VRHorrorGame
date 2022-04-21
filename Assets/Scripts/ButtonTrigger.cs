using Project.Core.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class ButtonTrigger : MonoBehaviour
{
    private CanSoundManager canSoundManager;

    public GameObject particle;

    [SerializeField]

    private UnityEvent onButtonPressed;

    private bool pressedInProgress = false;

    public float pressCoolTime;
    public float timer;

    public AudioClip buttonSound;
    AudioSource audio;

    private void Start()
    {
        canSoundManager = GameObject.FindWithTag("CanSoundManager").GetComponent<CanSoundManager>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = buttonSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("들어왔음");
        if (other.IsTriggerButton() && !pressedInProgress && timer <= 0)
        {
            pressedInProgress = true;
            onButtonPressed?.Invoke();
            timer = pressCoolTime;

            audio.Play();
            GameObject go = Instantiate(particle, this.transform);
            Destroy(go, 1f);
            canSoundManager.DropCan(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.IsTriggerButton())
        {
            pressedInProgress = false;
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
