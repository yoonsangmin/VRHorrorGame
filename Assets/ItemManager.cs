using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onFlashGet;

    bool isFlashPlayed = false;

    [SerializeField]
    private UnityEvent onYellowCardGet;

    bool isYellowCardPlayed = false;

    [SerializeField]
    private UnityEvent onRedCardGet;

    bool isRedCardPlayed = false;

    public bool[] hasItem = new bool[5];

    public int leftItemIdx;
    public int rightItemIdx;

    // Start is called before the first frame update
    void Start()
    {
        hasItem[0] = true; // 손
        for (int i = 1; i < hasItem.Length; i++)
        {
            hasItem[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasItem[1] && !isFlashPlayed)
        {
            onFlashGet?.Invoke();
            isFlashPlayed = true;
        }
        if (hasItem[2] && !isYellowCardPlayed)
        {
            onYellowCardGet?.Invoke();
            isYellowCardPlayed = true;
        }
        if (hasItem[4] && !isRedCardPlayed)
        {
            onRedCardGet?.Invoke();
            isRedCardPlayed = true;
        }
    }
}
