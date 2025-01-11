using UnityEngine;
using UnityEngine.Events;

public class DelayedEvent : MonoBehaviour
{
    public float delayInSeconds = 6f;
    public UnityEvent onDelayComplete;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= delayInSeconds)
        {
            onDelayComplete.Invoke();
            
            enabled = false;
        }
    }
}