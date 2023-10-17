using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GrabController : MonoBehaviour
{
    // Start is called before the first frame update
    enum Hand { Left, Right };

    [SerializeField] Hand hand = Hand.Right;
    GameObject target;
    GameObject grabbedObject;

    Vector3 lastPos, lastRot;
    Vector3 velocity, angularVelocity;

    void Start()
    {
        XR_InputController parent = GetComponentInParent<XR_InputController>();
        if(parent != null)
        {
            // this needs refactored
            if(hand == Hand.Left)
            {
                parent.leftGrab = this;
            } else {
                parent.rightGrab = this;
            }
        }else
        {
            Debug.LogError("GrabController needs to be a child of an XR_InputController");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = (transform.position - lastPos) / Time.deltaTime;
        angularVelocity = (transform.eulerAngles - lastRot) / Time.deltaTime;
        lastRot = transform.eulerAngles;
        lastPos = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Rigidbody>() != null && !other.gameObject.GetComponent<Rigidbody>().isKinematic && other.gameObject.GetComponent<GrabController>() == null)
        {
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            target = null;
        }
    }


    public void OnGrab()
    {
        Grab();
    }

    public void OnGrabRelease()
    {
        Drop();
    }

    void Drop()
    {
        if (grabbedObject == null)
            return;

        grabbedObject.transform.parent = null;
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
        grabbedObject = null;
    }

    void Grab()
    {
        if (target == null)
            return;

        grabbedObject = target;
        grabbedObject.transform.parent = transform;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
