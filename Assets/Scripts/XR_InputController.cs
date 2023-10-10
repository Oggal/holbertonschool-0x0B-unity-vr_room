using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class XR_InputController : MonoBehaviour
{
    [SerializeField] InputAction teleport;
    [SerializeField] InputAction grab;
    InputAction use;

    [SerializeField] InputActionReference rightControllerPosition,
        leftControllerPosition,
        rightControllerRotation,
        leftControllerRotation;
    [SerializeField] LocomotionController locomotionController;
    Vector3? targetPos = null;
    // Start is called before the first frame update
    void Start()
    {
        teleport.performed += Teleport;
        teleport.canceled += DoTeleport;
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        teleport.Enable();
        grab.Enable();
        
    }

    // OnDisable is called when the object becomes disabled
    void OnDisable()
    {
        teleport.Disable();
        grab.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(string.Format("Grip {0}",grab.ReadValue<float>().ToString()));
        if(targetPos != null)
        {
            Debug.Log(targetPos);
        }
        
    }

    void Teleport(InputAction.CallbackContext context)
    {
        // Debug.Log(context);
        // Debug.Log(context.action);
        InputDevice device = context.control.device;
        if (device as XRController != null)
        {
            XRController controller = (XRController)device;
            if (controller == XRController.leftHand)
            {
                Vector3 start = transform.position + leftControllerPosition.action.ReadValue<Vector3>();
                targetPos = locomotionController.GetNewPos(
                    start,
                    leftControllerRotation.action.ReadValue<Quaternion>() * Vector3.forward);
            }
            else if (controller == XRController.rightHand)
            {
                Debug.Log("Teleport triggered by right XR controller");
            }
        }
    }

    void DoTeleport(InputAction.CallbackContext context)
    {
        if (targetPos != null)
        {
            transform.position = (Vector3)targetPos;
            targetPos = null;
        }
    }

}
