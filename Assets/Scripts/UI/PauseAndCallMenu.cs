using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseAndCallMenu : MonoBehaviour
{
    public XRNode inputSource;

    public GameObject menuPanel;
    public GameObject gameRolePanel;
    public GameObject toTitlePanel;

    bool isPressed = false;
    bool isPressing = false;
    bool isPause;

    public Transform target;
    public Vector3 offset;

    public bool isPopUpOpened = false;

    public GameObject popUpPanel;
    public Vector3 popUpOffset;

    List<GameObject> popPanels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out isPressing);

        if(isPopUpOpened)
        {
            if (popPanels.Count != 0)
                popPanels[0].SetActive(true);


            popUpPanel.SetActive(true);


            transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            popUpPanel.transform.position = target.position + Vector3.up * popUpOffset.y
        + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * popUpOffset.x
        + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * popUpOffset.z;
        }

        else if (isPressing && !isPressed)
        {
            if (isPause == false)
            {
                isPause = true;

                gameRolePanel.SetActive(false);
                toTitlePanel.SetActive(false);

                menuPanel.SetActive(true);


                transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

                menuPanel.transform.position = target.position + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;
                Time.timeScale = 0;
            }

            else if (isPause == true)
            {
                Time.timeScale = 1;
                isPause = false;

                menuPanel.SetActive(false);
            }
        }

        else
        {
            popUpPanel.SetActive(false);
        }

        isPressed = isPressing;
    }
    
    public void QuitPopUp()
    {
        Destroy(popPanels[0]);
        popPanels.RemoveAt(0);

        if(popPanels.Count == 0)
            isPopUpOpened = false;
    }

    public void PopPanel(GameObject go)
    {
        GameObject temp = Instantiate(go, popUpPanel.transform);
        popPanels.Add(temp);
        popPanels[popPanels.Count - 1].SetActive(false);
        isPopUpOpened = true;
    }
}
