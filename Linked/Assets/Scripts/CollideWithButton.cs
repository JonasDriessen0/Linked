using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideWithButton : MonoBehaviour
{
    public UnityEvent onButtonPress;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onButtonPress?.Invoke();
        }
    }
}
