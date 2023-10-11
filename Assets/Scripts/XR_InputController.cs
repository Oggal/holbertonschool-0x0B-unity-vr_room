using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class XR_InputController : MonoBehaviour
{
    [SerializeField] InputAction teleport;
    [SerializeField] public InputAction grab;
    InputAction use;

    [SerializeField] InputActionReference rightControllerPosition,
        leftControllerPosition,
        rightControllerRotation,
        leftControllerRotation,
        HMDPosition;
    [SerializeField] LocomotionController locomotionController;
    Vector3? targetPos = null;
    // Start is called before the first frame update
    void Start()
    {
        teleport.performed += Teleport;
        teleport.canceled += c => locomotionController.Teleport(
            Vector3.Scale(
                HMDPosition.action.ReadValue<Vector3>(),
                new Vector3(-1,0,-1))
            );
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


    void Teleport(InputAction.CallbackContext context)
    {
        // Debug.Log(context);
        // Debug.Log(context.action);
        InputDevice device = context.control.device;
        if (device as XRController == null)
            return;
        XRController controller = (XRController)device;
        if (controller == XRController.leftHand)
        {
            targetPos = locomotionController.GetNewPos(
                transform.position + leftControllerPosition.action.ReadValue<Vector3>(),
                leftControllerRotation.action.ReadValue<Quaternion>() * Vector3.forward);
        }
        else if (controller == XRController.rightHand)
        {
            targetPos = locomotionController.GetNewPos(
                transform.position + rightControllerPosition.action.ReadValue<Vector3>(),
                rightControllerRotation.action.ReadValue<Quaternion>() * Vector3.forward);
        }
        
    }

}
