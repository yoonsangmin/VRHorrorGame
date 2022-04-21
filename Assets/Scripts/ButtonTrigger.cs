using Project.Core.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField]

    private UnityEvent onButtonPressed;

    private bool pressedInProgress = false;

    public float pressCoolTime;
    public float timer;

    public AudioClip buttonSound;
    AudioSource audio;

    private void Start()
    {
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
