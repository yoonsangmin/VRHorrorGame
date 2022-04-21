using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandButton : MonoBehaviour
{
    [SerializeField]

    private GameObject buttonTrigger;

    private Vector3 originalPosition;

    private float minDistance;

    private float maxDistance;

    void Awake()
    {
        minDistance = Vector3.Distance(buttonTrigger.transform.position, transform.position);

        maxDistance = buttonTrigger.transform.position.y;

        originalPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(buttonTrigger.transform.position, transform.position) >= minDistance)
        {
            transform.position = originalPosition;
        }

        if (transform.position.y <= maxDistance)
        {
            transform.position = new Vector3(transform.position.x, maxDistance, transform.position.z);
        }
    }
}




//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class HandButton : MonoBehaviour
//{
//    [System.Serializable]
//    public class ButtonEvent : UnityEvent { }

//    public float pressLength;
//    public bool pressed;
//    public ButtonEvent downEvent;

//    Vector3 startPos;
//    Rigidbody rb;

//    public float pressCoolTime;
//    float timer;

//    void Start()
//    {
//        startPos = transform.position;
//        rb = GetComponent<Rigidbody>();
//    }

//    void Update()
//    {
//        // If our distance is greater than what we specified as a press
//        // set it to our max distance and register a press if we haven't already
//        float distance = Mathf.Abs(transform.position.y - startPos.y);
//        if (distance >= pressLength)
//        {
//            // Prevent the button from going past the pressLength
//            transform.position = new Vector3(transform.position.x, startPos.y - pressLength, transform.position.z);
//            if (!pressed && timer <= 0)
//            {
//                pressed = true;
//                // If we have an event, invoke it
//                downEvent?.Invoke();
//                timer = pressCoolTime;
//            }
//        }
//        else
//        {
//            // If we aren't all the way down, reset our press
//            pressed = false;
//        }
//        // Prevent button from springing back up past its original position
//        if (transform.position.y > startPos.y)
//        {
//            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
//        }

//        if (timer > 0)
//        {
//            timer -= Time.deltaTime;
//        }
//    }
//}