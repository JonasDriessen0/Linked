using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnGameStart : MonoBehaviour
{
    public UnityEvent OnStart;
        
    void Start()
    {
        OnStart.Invoke();
    }
}
