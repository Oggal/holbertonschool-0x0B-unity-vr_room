using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Usable : MonoBehaviour
{

    public UnityEvent onUse, onUseRelease, onUseHold;
    public void OnUse()
    {
        onUse.Invoke();
    }

    public void OnUseRelease()
    {
        onUseRelease.Invoke();
    }

    public void OnUseHold()
    {
        onUseHold.Invoke();
    }

}
