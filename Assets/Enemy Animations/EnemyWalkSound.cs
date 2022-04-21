using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    public AudioClip[] walkSound;

    public GameObject walkParticle;

    AudioSource walkSource;

    // Start is called before the first frame update
    void Start()
    {
        walkSource = gameObject.AddComponent<AudioSource>();

        walkSource.spatialBlend = 1.0f;
        walkSource.minDistance = 0.1f;
        walkSource.rolloffMode = AudioRolloffMode.Custom;
        walkSource.maxDistance = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walksound()
    {
        int idx = Random.Range(0, walkSound.Length);
        GameObject instantiatedParticle = Instantiate(walkParticle, this.transform.position, this.transform.rotation) as GameObject;

        walkSource.clip = walkSound[idx];
        walkSource.Play();
        Destroy(instantiatedParticle, 1f);
    }

    
}
