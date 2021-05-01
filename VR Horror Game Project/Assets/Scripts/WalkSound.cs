using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] walkClips;
    public AudioSource walkSound;

    public float walkSoundFreq = 1f;
    public float timer;

    Vector3 currentPosition;
    Vector3 prevPosition;

    Vector3 currentCenter;
    Vector3 prevCenter;

    public float speedTreshold = 0.05f;

    bool isSoundPlayed;

    public float transformUpdateTime = 0.5f;
    public float transformUpdateTimer;

    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        walkSound = gameObject.AddComponent<AudioSource>();

        timer = walkSoundFreq;

        prevPosition = transform.position;
        prevCenter = gameObject.GetComponent<CharacterController>().center;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        currentCenter = gameObject.GetComponent<CharacterController>().center;

        speed = Mathf.Abs(Vector3.Distance(new Vector3(prevPosition.x, 0, prevPosition.z), new Vector3(currentPosition.x, 0, currentPosition.z))
            + Vector3.Distance(new Vector3(prevCenter.x, 0, prevCenter.z), new Vector3(currentCenter.x, 0, currentCenter.z)));
        //float speed = Mathf.Abs(Vector3.Distance(gameObject.GetComponent<CharacterController>().velocity, Vector3.zero));

        transformUpdateTimer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = walkSoundFreq;
            isSoundPlayed = false;
        }

        else if(speed > speedTreshold)
        {
            if(!isSoundPlayed)
            {
                RandomSelectSound();
                walkSound.Play();
                isSoundPlayed = true;
            }

            timer -=  speed * Time.deltaTime;
        }

        if (transformUpdateTimer < 0)
        {
            prevPosition = transform.position;
            prevCenter = currentCenter;
            transformUpdateTimer = transformUpdateTime;
        }

        //prevPosition = currentPosition;
    }

    public void RandomSelectSound()
    {
        walkSound.clip = walkClips[Random.Range(0, walkClips.Length)];
    }
}
