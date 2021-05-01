using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject flashLight;

    public void TurnOnFlash()
    {
        if(flashLight.activeSelf == false)
        {
            flashLight.SetActive(true);
        }
        else
        {
            flashLight.SetActive(false);
        }    
    }
}
