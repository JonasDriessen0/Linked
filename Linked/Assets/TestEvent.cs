using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEvent : MonoBehaviour
{
    public KeyCode keybind;
    public UnityEvent testThing;

    private void Update()
    {
        if (Input.GetKeyDown(keybind))
        {
            testThing.Invoke();
        }
    }
}
