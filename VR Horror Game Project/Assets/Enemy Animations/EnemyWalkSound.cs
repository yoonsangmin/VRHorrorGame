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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walksound()
    {
        int idx = Random.Range(0, walkSound.Length);

        walkSource.clip = walkSound[idx];
        walkSource.maxDistance = 4;
        walkSource.Play();
    }

    
}
