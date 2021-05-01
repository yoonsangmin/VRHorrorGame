using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDetecter : MonoBehaviour
{
    public bool isActivate;

    public string cardName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == cardName)
        {
            isActivate = true;
        }
    }
}
