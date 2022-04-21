using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class CanPocket : MonoBehaviour
{
    public Text text;

    public GameObject canAttachPos;
    public GameObject canDamageAttachPos;

    List<GameObject> canList = new List<GameObject>();

    public GameObject hoveredObject;
    public GameObject latelyRemovedObject;

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject latelyRemovedObjectHand;

    bool grabbedAndSelected;

    float refreshtime = 1.0f;
    float timer = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        canAttachPos.GetComponent<MeshRenderer>().enabled = false;
        canDamageAttachPos.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < canList.Count; i++)
        {
            if (canList[i].GetComponent<MeshFilter>().mesh.ToString() == canAttachPos.GetComponent<MeshFilter>().mesh.ToString())
            {
                canList[i].transform.position = canAttachPos.transform.position;
                canList[i].transform.rotation = canAttachPos.transform.rotation;
            }
            else
            {
                canList[i].transform.position = canDamageAttachPos.transform.position;
                canList[i].transform.rotation = canDamageAttachPos.transform.rotation;
            }
            if (i == canList.Count - 1)
            {
                canList[i].GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                canList[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        text.text = "Remain: " + canList.Count.ToString();

       
    }

    private void LateUpdate()
    {
        if (latelyRemovedObject != null)
        {
            //Debug.Log(latelyRemovedObjectHand);
            if (latelyRemovedObjectHand == leftHand.transform.parent.gameObject)
            {
                if (leftHand.activeSelf)
                {
                    if (timer > refreshtime)
                        timer = 0.0f;
                    timer += Time.deltaTime;
                    if (timer > refreshtime)
                    {
                        latelyRemovedObject = null;
                        latelyRemovedObjectHand = null;
                    }
                }
            }
            else if (latelyRemovedObjectHand == rightHand.transform.parent.gameObject)
            {
                if (rightHand.activeSelf)
                {
                    if (timer > refreshtime)
                        timer = 0.0f;
                    timer += Time.deltaTime;
                    if (timer > refreshtime)
                    {
                        latelyRemovedObject = null;
                        latelyRemovedObjectHand = null;
                    }
                }
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Can") && other.GetComponent<XRGrabInteractable>().selectingInteractor != null)
        {

            this.GetComponent<MeshRenderer>().enabled = true;

            //for (int i = 0; i < canList.Count; i++)
            //{
            //    canList[i].GetComponent<MeshCollider>().enabled = false;
            //}

            hoveredObject = other.gameObject;

            grabbedAndSelected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Can"))
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            if (other.gameObject == hoveredObject)
                hoveredObject = null;
        }    
           
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("---");
        //Debug.Log(canList.Count);
        //Debug.Log("can : " + other.CompareTag("Can"));
        //Debug.Log("아더랑 호버가 같은지 " + (other.gameObject == hoveredObject));
        //Debug.Log("엔터 제대로 됐는지 " + grabbedAndSelected);
        //Debug.Log(!other.GetComponent<XRGrabInteractable>().isSelected);
        //Debug.Log(latelyRemovedObject == other.gameObject);

        //if (other.CompareTag("Can") && other.gameObject == hoveredObject && grabbedAndSelected)
        //{
        //    Debug.Log(other.GetComponent<XRGrabInteractable>().selectingInteractor);
        //    if(other.GetComponent<XRGrabInteractable>().selectingInteractor != null)
        //        Debug.Log(other.GetComponent<XRGrabInteractable>().selectingInteractor.gameObject); 
        //}

        //if (other.CompareTag("Can") && grabbedAndSelected && !other.GetComponent<XRGrabInteractable>().isSelected)
        //{
        //if (other.CompareTag("Can") && other.gameObject == hoveredObject && grabbedAndSelected
        //    && (!other.GetComponent<XRGrabInteractable>().isSelected || (latelyRemovedObject == other.gameObject && other.GetComponent<XRGrabInteractable>().selectingInteractor.gameObject.GetComponentInChildren<HandPresence>().gameObject.activeSelf)))
        //{
        if (other.CompareTag("Can") && grabbedAndSelected && other.GetComponent<XRGrabInteractable>().selectingInteractor == null 
            && (other.gameObject == hoveredObject || other.gameObject == latelyRemovedObject))
        {
            //for (int i = 0; i < canList.Count; i++)
            //{
            //    canList[i].GetComponent<MeshCollider>().enabled = true;
            //}
            //Debug.Log("들어옴");



            canList.Add(other.gameObject);
            canList[canList.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
            //if(canList.Count > 1)
            //{
            //    canList[canList.Count - 2].GetComponent<MeshRenderer>().enabled = false;
            //}

            hoveredObject = null;
            latelyRemovedObject = null;
            grabbedAndSelected = false;
        }
    }

    public void Pop(GameObject gameObject)
    {
        if (canList.Count > 0)
        {
            //Debug.Log(canList.Peek());
            //Debug.Log(gameObject);
            if (canList.Contains(gameObject))
            {
                canList[canList.IndexOf(gameObject)].GetComponent<Rigidbody>().isKinematic = false;
                canList[canList.IndexOf(gameObject)].GetComponent<MeshRenderer>().enabled = true;
                latelyRemovedObject = canList[canList.IndexOf(gameObject)];
                latelyRemovedObjectHand = latelyRemovedObject.GetComponent<XRGrabInteractable>().selectingInteractor.gameObject;
                canList.RemoveAt(canList.IndexOf(gameObject));
            }
        }
    }
}