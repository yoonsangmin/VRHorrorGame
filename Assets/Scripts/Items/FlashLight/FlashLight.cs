using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight : MonoBehaviour
{
    public XRNode inputSource;

    private float triggerAxis;
    private bool keyDown;
    private bool prevKeyDown;



    public GameObject flashLight;

    public Material[] offMat;
    public Material[] onMat;

    private Material[] offMats;
    private Material[] onMats;

    public MeshRenderer meshRenderer = null;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private ItemManager itemManager;

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

        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
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


    bool onClick()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.trigger, out triggerAxis);

        if (triggerAxis >= 0.5f)
            keyDown = true;
        else
            keyDown = false;

        if (keyDown && !prevKeyDown)
        {
            prevKeyDown = keyDown;
            return true;
        }

        prevKeyDown = keyDown;
        return false;
    }

    private void Update()
    {
        if (itemManager.leftItemIdx == 1)
        {
            inputSource = XRNode.LeftHand;
            if (onClick())
            {
                TurnOnFlash();
            }
        }
        else if (itemManager.rightItemIdx == 1)
        {
            inputSource = XRNode.RightHand;
            if (onClick())
            {
                TurnOnFlash();
            }
        }
    }
}