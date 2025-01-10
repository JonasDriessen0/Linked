using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public float gridSize = 1f;
    private bool isMoving;

    void Update()
    {
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
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W)) moveDirection += Vector3.right;
        if (Input.GetKeyDown(KeyCode.S)) moveDirection += Vector3.left;
        if (Input.GetKeyDown(KeyCode.A)) moveDirection += Vector3.forward;
        if (Input.GetKeyDown(KeyCode.D)) moveDirection += Vector3.back;

        if (moveDirection != Vector3.zero)
        {
            Vector3 newPosition = transform.position + moveDirection * gridSize;
            transform.position = newPosition;
        }
    }
}