using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class LocomotionController : MonoBehaviour
{

    public LayerMask layerMask;

    public GameObject targetRetical;
    Vector3? lastPos = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (lastPos != null && targetRetical != null)
        {
            targetRetical.transform.position = (Vector3)lastPos;
        }

        targetRetical.SetActive(lastPos != null);
    }


    /// <summary>
    /// Returns a new position to move to if valid, otherwise returns null
    /// </summary>
    /// <param name="rayStart"> start of ray</param>
    /// <param name="targetDir"> direction to cast ray</param>
    /// <returns> new pos or null</returns>
    public Vector3? GetNewPos(Vector3 rayStart = default(Vector3), Vector3 targetDir = default(Vector3))
    {    
        if (targetDir.magnitude < 0.1f)
            return targetDir = transform.forward;
        if (rayStart.magnitude < 0.1f)
            rayStart = transform.position;
        RaycastHit hit;
        if (!Physics.Raycast(rayStart, targetDir, out hit, 10f, layerMask))
            return null;
        if (Vector3.Distance(Vector3.up, hit.normal) > 0.2f)
            return null;
        return lastPos = hit.point;
    }

    public void Teleport(Vector3? pos = null)
    {
        if(pos.HasValue)
            lastPos += pos.Value;
        if (lastPos != null)
        {
            transform.position = (Vector3)lastPos;
            lastPos = null;
        }
    }

    private GameObject BuildDebugReticule()
    {
        GameObject retical = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        retical.SetLayerRecursively(LayerMask.NameToLayer("Ignore Raycast"));
        retical.GetComponent<MeshRenderer>().material.color = Color.red;
        return retical;
    }
}
