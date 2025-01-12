using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEvent : MonoBehaviour
{
    public KeyCode keybind;
    public UnityEvent testThing;
    public bool canActivate = false;

    private void Update()
    {
        if (Input.GetKeyDown(keybind) && canActivate)
        {
            testThing.Invoke();
        }
    }

    public void AllowActivate()
    {
        canActivate = true;
    }
    
    public void DisableActivate()
    {
        canActivate = false;
    }
}
