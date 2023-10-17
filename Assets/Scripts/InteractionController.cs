using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    enum Hand { Left, Right };
    [SerializeField] Hand hand = Hand.Right;
    XR_InputController parent;
    ParticleSystem particles;
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
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void OnDisable()
    {
        Debug.Log(string.Format("InteractionController Disabled: {0}", hand.ToString()));
    }

    void FixedUpdate()
    {
        if(particles != null )
        {  
            var main  = particles.main;
            if(target.Count > 0 )
            {
                Usable item = target[0];
                var shape = particles.shape;
                main.customSimulationSpace = item.transform;
                shape.meshRenderer = item.GetComponent<MeshRenderer>();
                if (!particles.isPlaying)
                    particles.Play();
            
            } else {
                particles.Stop();
                particles.Clear();
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Usable item = other.gameObject.GetComponentInChildren<Usable>();
        Debug.Log(string.Format("Trigger Enter... {0}", other.name));
        if(item != null && !target.Contains(item))
        {
            target.Insert(0, item);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Usable item = other.gameObject.GetComponentInChildren<Usable>();
        Debug.Log(string.Format("Trigger Exit... {0}",other.name));
        if(item != null && target.Contains(item))
        {
            target.Remove(item);
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
