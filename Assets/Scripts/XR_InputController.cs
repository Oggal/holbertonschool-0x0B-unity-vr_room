using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XR_InputController : MonoBehaviour
{

    [SerializeField] InputAction teleport;
    [SerializeField] InputAction grab;
    InputAction use;
    // Start is called before the first frame update
    void Start()
    {
        teleport.started += context => Debug.Log($"{context.action} started");
    }

    // Update is called once per frame
    void Update()
    {
        if(teleport.triggered)
        {
            Debug.Log("Teleport");
        }
    }
}
