using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onTrigger;
    public bool disableWhenFinish;
    public Collider trigger;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();
        if (disableWhenFinish)
        {
            trigger.enabled = false;
        }
    }
}
