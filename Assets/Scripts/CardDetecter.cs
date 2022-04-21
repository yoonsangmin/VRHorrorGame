using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardDetecter : MonoBehaviour
{
    private CanSoundManager canSoundManager;

    public GameObject particle;

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
        canSoundManager = GameObject.FindWithTag("CanSoundManager").GetComponent<CanSoundManager>();
        audio = gameObject.AddComponent<AudioSource>();
        audio.volume = 0.5f;
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

                GameObject go = Instantiate(particle, this.transform);
                Destroy(go, 1f);
                canSoundManager.DropCan(this.transform);

                onCardHover?.Invoke();
                isActivate = true;

                CaptionManager.Instance.updateCaption("문이 열렸어!", 3, false);
            }
            else
            {
                audio.clip = wrongSound;
                audio.Play();

                GameObject go = Instantiate(particle, this.transform);
                Destroy(go, 1f);
                canSoundManager.DropCan(this.transform);

                CaptionManager.Instance.updateCaption("이 카드가 아니야", 3, false);
            }
        }
    }
}
