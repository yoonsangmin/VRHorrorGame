using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    public AudioClip[] walkSound;
    

    AudioSource walkSource;

    // Start is called before the first frame update
    void Start()
    {
        walkSource = gameObject.AddComponent<AudioSource>();

        walkSource.spatialBlend = 1.0f;
        walkSource.minDistance = 0.1f;
        walkSource.maxDistance = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walksound()
    {
        int idx = Random.Range(0, walkSound.Length);

        walkSource.clip = walkSound[idx];
        walkSource.Play();
    }

    
}
