using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight1 : MonoBehaviour
{
    public GameObject flashLight;

    public Material[] offMat;
    public Material[] onMat;

    private Material[] offMats;
    private Material[] onMats;

    public MeshRenderer meshRenderer = null;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.spatialBlend = 0.7f;
        audioSource.playOnAwake = false;

        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        offMats = meshRenderer.materials;
        onMats = meshRenderer.materials;
        offMats[1] = offMat[0];
        offMats[2] = offMat[1];
        offMats[5] = offMat[2];

        onMats[1] = onMat[0];
        onMats[2] = onMat[1];
        onMats[5] = onMat[2];

        meshRenderer.materials = offMats;
        flashLight.SetActive(false);
    }

    public void TurnOnFlash()
    {
        if(flashLight.activeSelf == false)
        {
            meshRenderer.materials = onMats;
            flashLight.SetActive(true);
            audioSource.Play();
        }
        else
        {
            meshRenderer.materials = offMats;
            flashLight.SetActive(false);
            audioSource.Play();
        }    
    }
}
