using UnityEngine;

public class OverseerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float zoomSpeed = 2f;
    public float zoomMin = 20f;
    public float zoomMax = 80f;
    public Camera overseerCamera;
    public Transform overseerGameObject;

    void Update()
    {
        HandleMovement();
        HandleZoom();
        SyncOverseerGameObject();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 localMove = new Vector3(moveX, moveY, 0f);
        Vector3 newPosition = transform.position + transform.TransformDirection(localMove);
        transform.position = newPosition;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float newFieldOfView = overseerCamera.fieldOfView - scroll;
        overseerCamera.fieldOfView = Mathf.Clamp(newFieldOfView, zoomMin, zoomMax);
    }

    private void SyncOverseerGameObject()
    {
        if (overseerGameObject != null)
        {
            overseerGameObject.position = new Vector3(transform.position.x, overseerGameObject.position.y, transform.position.z);
        }
    }
}