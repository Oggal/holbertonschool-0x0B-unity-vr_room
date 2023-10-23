using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    enum Hand { Left, Right };
    [SerializeField] Hand hand = Hand.Right;
    [SerializeField] Color color;
    XR_InputController parent;
    InteractionParticleController visual;    
    List<Usable> target;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponentInParent<XR_InputController>();
        if(parent != null)
        {
            if(hand == Hand.Left)
            {
                parent.leftInteraction = this;
            } else {
                parent.rightInteraction = this;
            }
        } else {
            Debug.LogError("InteractionController needs to be a child of an XR_InputController");
        }
        target = new List<Usable>();
        visual = GetComponent<InteractionParticleController>();
    }

    void OnDisable()
    {
        Debug.Log(string.Format("InteractionController Disabled: {0}", hand.ToString()));
    }

    void FixedUpdate()
    {
        
    }


    void OnTriggerEnter(Collider other)
    {
        Usable item = other.gameObject.GetComponentInChildren<Usable>();
        Debug.Log(string.Format("Trigger Enter... {0}", other.name));
        if(item != null && !target.Contains(item))
        {
            target.Insert(0, item);
            visual.AddTarget(other.gameObject,color);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Usable item = other.gameObject.GetComponentInChildren<Usable>();
        Debug.Log(string.Format("Trigger Exit... {0}",other.name));
        if(item != null && target.Contains(item))
        {
            target.Remove(item);
            visual.RemoveTarget(other.gameObject);
        }
    }

    public void OnUse()
    {
        Debug.Log(string.Format("Use: {0}", target.Count.ToString()));
        Usable item = target.Count > 0 ? target[0] : null;
        if(item != null)
        {
            item.OnUse();
        }
    }

}
