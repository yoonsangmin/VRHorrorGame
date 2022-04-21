using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class ChangeItem : MonoBehaviour
{
    public UnityEvent onItemSelect;

    public XRNode inputSource;

    private bool keyDown;
    private bool prevKeyDown;

    private bool grabKeyDown;
    private bool grabPrevKeyDown;

    int itemIdx = 0;

    public GameObject[] items;

    private ItemManager itemManager;

    public enum Hand
    {
        NONE = 0,
        LEFT = 1,
        RIGHT,
        SIZE
    }

    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        items[0].SetActive(true);
        for (int i = 1; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.GetComponent<XRDirectInteractor>().selectTarget);
        if (onClick() && this.GetComponent<XRDirectInteractor>().selectTarget == null)
        {
            do
            {
                itemIdx++;
                itemIdx %= items.Length;
            } while (!itemManager.hasItem[itemIdx]);

            switch(hand)
            {
                case Hand.LEFT:
                    if(itemIdx == itemManager.rightItemIdx && itemManager.rightItemIdx != 0)
                    {
                        do
                        {
                            itemIdx++;
                            itemIdx %= items.Length;
                        } while (!itemManager.hasItem[itemIdx]);
                    }
                    itemManager.leftItemIdx = itemIdx;
                    break;
                case Hand.RIGHT:
                    if (itemIdx == itemManager.leftItemIdx && itemManager.leftItemIdx != 0)
                    {
                        do
                        {
                            itemIdx++;
                            itemIdx %= items.Length;
                        } while (!itemManager.hasItem[itemIdx]);
                    }
                    itemManager.rightItemIdx = itemIdx;
                    break;
            }

            onItemSelect?.Invoke();
            SetItem();
        }


        //Debug.Log(itemIdx);
    }

    bool onClick()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out keyDown);

        if(keyDown && !prevKeyDown)
        {
            prevKeyDown = keyDown;
            return true;
        }

        prevKeyDown = keyDown;
        return false;
    }

    bool onGrab()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.gripButton, out grabKeyDown);

        if (grabKeyDown && !grabPrevKeyDown)
        {
            grabPrevKeyDown = grabKeyDown;
            return true;
        }

        grabPrevKeyDown = grabKeyDown;
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Grabbable") && (other.name == "FlashLight" || other.name =="Card1" || other.name == "Card2" || other.name == "Card3"))
        {
            if(this.GetComponent<XRDirectInteractor>().selectTarget == null)
            {
                if (onGrab() && itemIdx == 0)
                {
                    if (other.name == "FlashLight") itemIdx = 1;
                    if (other.gameObject.name == "Card1") itemIdx = 2;
                    if (other.gameObject.name == "Card2") itemIdx = 3;
                    if (other.gameObject.name == "Card3") itemIdx = 4;

                    itemManager.hasItem[itemIdx] = true;

                    switch (hand)
                    {
                        case Hand.LEFT:
                            itemManager.leftItemIdx = itemIdx;
                            break;
                        case Hand.RIGHT:
                            itemManager.rightItemIdx = itemIdx;
                            break;
                    }

                    SetItem();
                    onItemSelect?.Invoke();

                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void SetItem()
    {
        if (itemIdx != 0)
        {
            this.GetComponent<SphereCollider>().enabled = false;
            this.GetComponent<XRDirectInteractor>().enabled = false;
        }
        else
        {
            this.GetComponent<SphereCollider>().enabled = true;
            this.GetComponent<XRDirectInteractor>().enabled = true;
        }


        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }
        items[itemIdx].SetActive(true);
    }
}
