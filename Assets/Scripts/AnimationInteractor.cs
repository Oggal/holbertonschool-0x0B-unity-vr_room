using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInteractor : MonoBehaviour
{
    public Animator controller;

    // Start is called before the first frame update
    void Start()
    {
        if(controller == null)
            controller = GetComponent<Animator>();
    }

    public void ToggleBool(string name)
    {
        controller.SetBool(name, !controller.GetBool(name));
    }
}
