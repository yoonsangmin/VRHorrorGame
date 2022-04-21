using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseAndCallMenu : MonoBehaviour
{
    public XRNode inputSource;

    public GameObject menuPanel;

    bool isPressed = false;
    bool isPressing = false;
    bool isPause;

    public Transform target;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressing);

        if (isPressing && !isPressed)
        {
            if (isPause == false)
            {
                Time.timeScale = 0;
                isPause = true;

                menuPanel.SetActive(true);


                transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

                menuPanel.transform.position = target.position + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;
            }

            else if (isPause == true)
            {
                Time.timeScale = 1;
                isPause = false;

                menuPanel.SetActive(false);
            }
        }

        isPressed = isPressing;
    }
}
