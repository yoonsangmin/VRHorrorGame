using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLittedState : MonoBehaviour
{
    public bool isPlayerLitted;
    public FlashLight flashLight;
    public float lightThresholdDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerLitted = false;
        flashLight = GameObject.FindGameObjectWithTag("FlashLight")?.GetComponent<FlashLight>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashLight != null)
        {
            if (Vector3.Distance(transform.position, flashLight.transform.position) < lightThresholdDistance && flashLight.lightObject.activeSelf == true)
            {
                isPlayerLitted = true;
                Debug.Log("light is near");
            }
        }

        //Debug.Log(isPlayerLitted);
    }

    private void LateUpdate()
    {
        isPlayerLitted = false;
    }
}
