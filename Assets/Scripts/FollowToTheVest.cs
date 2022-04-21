using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowToTheVest : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        Vector3 rot3 = Vector3.ProjectOnPlane(target.right, Vector3.up).normalized;
        transform.eulerAngles = new Vector3(0, -Mathf.Atan2(rot3.normalized.z, rot3.normalized.x) * Mathf.Rad2Deg, 0);

    }
}
