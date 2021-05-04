using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardDetecter : MonoBehaviour
{
    [SerializeField]

    private UnityEvent onCardHover;

    public bool isActivate = false;

    public string cardName;

    public AudioClip detectSound;
    public AudioClip wrongSound;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Card" && !isActivate)
        {
            if (other.gameObject.name == cardName)
            {
                audio.clip = detectSound;
                audio.Play();
                onCardHover?.Invoke();
                isActivate = true;
            }
            else
            {
                audio.clip = wrongSound;
                audio.Play();
            }
        }
    }
}
