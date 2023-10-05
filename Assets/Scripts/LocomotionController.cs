using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LocomotionController : MonoBehaviour
{

    public LayerMask layerMask;

    public GameObject targetRetical;
    // Start is called before the first frame update
    void Start()
    {
        if (targetRetical == null)
        {
            targetRetical = BuildDebugReticule();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3? targetPos = GetNewPos();
        if (targetPos != null)
        {
            targetRetical.transform.position = (Vector3)targetPos;
        }
        targetRetical.SetActive(targetPos != null);
    }

    Vector3? GetNewPos()
    {    
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.forward, out hit, 10f, layerMask))
            return null;
        if (Vector3.Distance(Vector3.up, hit.normal) > 0.2f)
            return null;
        return hit.point;
    }

    private GameObject BuildDebugReticule()
    {
        GameObject retical = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        retical.SetLayerRecursively(LayerMask.NameToLayer("Ignore Raycast"));
        retical.GetComponent<MeshRenderer>().material.color = Color.red;
        return retical;
    }
}
