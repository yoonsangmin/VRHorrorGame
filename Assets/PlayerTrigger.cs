using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField]

    private UnityEvent onTrigger;

    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("VRRig") && isTriggered == false)
        {
            onTrigger?.Invoke();
            isTriggered = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
