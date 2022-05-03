using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorCard : MonoBehaviour
{
    public GameObject cardLocker;
    public GameObject openPosObj;

    Vector3 closedPos;

    bool isDoorOpened = false;
    bool activate = false;

    // Start is called before the first frame update
    void Start()
    {
        closedPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(cardLocker.GetComponent<CardDetecter>().isActivate)
        {
            activate = true;
        }

        if(activate && !isDoorOpened)
            Open();
    }

    void Open()
    {
        transform.position = Vector3.Lerp(transform.position, openPosObj.transform.position, 0.02f);
        
        if(transform.position == openPosObj.transform.position)
        {
            isDoorOpened = true;
        }
    }
}
