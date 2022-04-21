using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSoundManager : MonoBehaviour
{
    public bool isCanDropped = false;
    public Transform canDroppedTransform;

    private void LateUpdate()
    {
        isCanDropped = false;
    }

    public void DropCan(Transform transform)
    {
        isCanDropped = true;
        canDroppedTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canDroppedTransform);
    }
}
