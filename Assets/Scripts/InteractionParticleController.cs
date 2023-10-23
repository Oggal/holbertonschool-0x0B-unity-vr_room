using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class InteractionParticleController : MonoBehaviour
{

    List<MeshRenderer> targets;

    [SerializeField] ParticleSystem particles;
    Color highlightColor = Color.magenta;

    // Start is called before the first frame update
    void Start()
    {
        if (particles == null)
            particles = GetComponent<ParticleSystem>();
        targets = new List<MeshRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if(particles == null )
        {
            Debug.Log("No Particles");
            return;
        }
        
        if(targets.Count > 0 && targets[0] != null )
        {
            var shape = particles.shape;
            var main  = particles.main;
            main.customSimulationSpace = targets[0].transform;
            //main.startColor = highlightColor;
            shape.meshRenderer = targets[0];
            if (!particles.isPlaying)
                particles.Play();
        
        } else {
            Debug.Log(targets.Count);
            particles.Stop();
            particles.Clear();
        }
    }


    public void AddTarget(GameObject t,Color c)
    {
        MeshRenderer result = t.GetComponentInChildren<MeshRenderer>();
        if( result != null && !targets.Contains(result))
        {
            targets.Insert(0,result);
            highlightColor = c;
        }
        else
        {
            Debug.Log(
                string.Format("Mesh Renderer not found on {0}",t));
        }
    }

    public void RemoveTarget(GameObject t)
    {
        if (t == null)
        {
            return;
        }
        MeshRenderer result = t.GetComponentInChildren<MeshRenderer>();
        if( result != null )
        {
            targets.Remove(result);
        }
    }
}
