using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateEnableObject : MonoBehaviour
{
    public GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        go.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        go.SetActive(true);

        Destroy(this.gameObject);
    }
}
