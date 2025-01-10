using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("MouseStats")]
    public float mouseSensitivity = 100f;
    public float distanceFromPlayer = 5f;
    public float verticalOffset = 1.5f;
    public float smoothSpeed = 0.125f;
    public float collisionRadius = 0.5f;
    public LayerMask collisionLayers;

    [Header("References")]
    public Transform playerBody;

    private float xRotation = 0f;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.RotateAround(playerBody.position, Vector3.up, mouseX);

        Quaternion verticalRotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0f);

        Vector3 direction = verticalRotation * Vector3.back;
        Vector3 desiredPosition = playerBody.position + Vector3.up * verticalOffset + direction * distanceFromPlayer;
        
        if (Physics.SphereCast(playerBody.position + Vector3.up * verticalOffset, collisionRadius, direction, out RaycastHit hit, distanceFromPlayer, collisionLayers))
        {
            desiredPosition = hit.point + direction * collisionRadius;
        }

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.rotation = verticalRotation;
    }
}