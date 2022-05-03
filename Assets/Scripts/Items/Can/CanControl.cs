using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanControl : MonoBehaviour
{
    CanPocket canPocket;

    // Start is called before the first frame update
    void Start()
    {
        canPocket = GameObject.FindGameObjectWithTag("CanPocket").GetComponent<CanPocket>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pop()
    {
        canPocket.Pop(this.gameObject);
    }
}
