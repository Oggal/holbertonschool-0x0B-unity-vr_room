using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleToggle : MonoBehaviour
{
    public GameObject target;

    public void Toggle()
    {
        if (target != null)
            target.SetActive(!target.activeSelf);
    }
}
