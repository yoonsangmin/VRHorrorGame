using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSound : MonoBehaviour
{
    private CanSoundManager canSoundManager;

    public GameObject particle;
    public AudioClip cansound;
    AudioSource audiosource = null;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(this.GetComponent<Rigidbody>().velocity.magnitude);

        if (collision.gameObject.CompareTag("Obstacle") && this.GetComponent<Rigidbody>().velocity.magnitude > 0.3f)
        {
            GameObject go = Instantiate(particle, this.transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(go, 1f);

            audiosource.volume = this.GetComponent<Rigidbody>().velocity.magnitude + 0.2f;
            audiosource.Play();

            canSoundManager.DropCan(this.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canSoundManager = GameObject.FindWithTag("CanSoundManager").GetComponent<CanSoundManager>();

        audiosource = this.gameObject.AddComponent<AudioSource>();
        audiosource.clip = cansound;
        audiosource.spatialBlend = 1.0f;
        audiosource.rolloffMode = AudioRolloffMode.Custom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
