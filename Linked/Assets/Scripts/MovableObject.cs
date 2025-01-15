using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float gridSize = 1f;
    public float holdTime = 0.5f;
    public float moveDelay = 0.2f;

    private bool isMoving;
    private float[] keyHoldTimers = new float[4];
    private float[] nextMoveTimes = new float[4];

    void Update()
    {
        HandleKeyTimers();

        if (isMoving)
        {
            HandleMovement();
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        ResetKeyTimers();
    }

    void HandleKeyTimers()
    {
        // Update timers based on key presses
        if (Input.GetKey(KeyCode.W)) keyHoldTimers[0] += Time.deltaTime;
        else keyHoldTimers[0] = 0;

        if (Input.GetKey(KeyCode.S)) keyHoldTimers[1] += Time.deltaTime;
        else keyHoldTimers[1] = 0;

        if (Input.GetKey(KeyCode.A)) keyHoldTimers[2] += Time.deltaTime;
        else keyHoldTimers[2] = 0;

        if (Input.GetKey(KeyCode.D)) keyHoldTimers[3] += Time.deltaTime;
        else keyHoldTimers[3] = 0;
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        // Initial movement on key press
        if (Input.GetKeyDown(KeyCode.W)) moveDirection += Vector3.right;
        if (Input.GetKeyDown(KeyCode.S)) moveDirection += Vector3.left;
        if (Input.GetKeyDown(KeyCode.A)) moveDirection += Vector3.forward;
        if (Input.GetKeyDown(KeyCode.D)) moveDirection += Vector3.back;

        // Continuous movement after holding the key for holdTime
        if (keyHoldTimers[0] >= holdTime && Time.time >= nextMoveTimes[0])
        {
            moveDirection += Vector3.right;
            nextMoveTimes[0] = Time.time + moveDelay;
        }
        if (keyHoldTimers[1] >= holdTime && Time.time >= nextMoveTimes[1])
        {
            moveDirection += Vector3.left;
            nextMoveTimes[1] = Time.time + moveDelay;
        }
        if (keyHoldTimers[2] >= holdTime && Time.time >= nextMoveTimes[2])
        {
            moveDirection += Vector3.forward;
            nextMoveTimes[2] = Time.time + moveDelay;
        }
        if (keyHoldTimers[3] >= holdTime && Time.time >= nextMoveTimes[3])
        {
            moveDirection += Vector3.back;
            nextMoveTimes[3] = Time.time + moveDelay;
        }

        if (moveDirection != Vector3.zero)
        {
            Vector3 newPosition = transform.position + moveDirection * gridSize;
            transform.position = newPosition;
        }
    }

    void ResetKeyTimers()
    {
        for (int i = 0; i < keyHoldTimers.Length; i++)
        {
            keyHoldTimers[i] = 0;
            nextMoveTimes[i] = 0;
        }
    }
}
