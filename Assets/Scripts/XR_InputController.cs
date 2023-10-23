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
    [SerializeField ]InputAction use;

    [SerializeField] InputActionReference rightControllerPosition,
        leftControllerPosition,
        rightControllerRotation,
        leftControllerRotation,
        HMDPosition;
    [SerializeField] LocomotionController locomotionController;
    Vector3? targetPos = null;
    Vector3? debugPos = null;

    public GrabController leftGrab, rightGrab;
    public InteractionController leftInteraction, rightInteraction;
    // Start is called before the first frame update
    void Start()
    {
        teleport.performed += Teleport;
        teleport.canceled += DoTeleport;
        grab.started += DoGrab;
        grab.canceled += DoDrop;
        use.started += DoInteract;

    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        teleport.Enable();
        grab.Enable();
        use.Enable();
        
    }

    // OnDisable is called when the object becomes disabled
    void OnDisable()
    {
        teleport.Disable();
        grab.Disable();
        use.Disable();
    }
    void OnDrawGizmos()
    {
        if(debugPos.HasValue)
            Gizmos.DrawSphere(debugPos.Value,0.5f);
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

    void DoTeleport(InputAction.CallbackContext context)
    {
        locomotionController.Teleport(
            Vector3.Scale(
                HMDPosition.action.ReadValue<Vector3>(),
                new Vector3(-1,0,-1))
            );
    }

    void DoGrab(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        if (device as XRController == null)
            return;
        XRController controller = (XRController)device;
        if (controller == XRController.leftHand)
        {
            leftGrab.OnGrab();
        }
        else if (controller == XRController.rightHand)
        {
            rightGrab.OnGrab();
        }
    }

    void DoDrop(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        if (device as XRController == null)
            return;
        XRController controller = (XRController)device;
        if (controller == XRController.leftHand)
        {
            leftGrab.OnGrabRelease();
        }
        else if (controller == XRController.rightHand)
        {
            rightGrab.OnGrabRelease();
        }
    }

    void DoInteract(InputAction.CallbackContext context)
    {
        InputDevice device = context.control.device;
        if (device as XRController == null)
            return;
        XRController controller = (XRController)device;
        if (controller == XRController.leftHand)
        {
            leftInteraction.OnUse();
        }
        else if (controller == XRController.rightHand)
        {
            rightInteraction.OnUse();
        }
    }
}
