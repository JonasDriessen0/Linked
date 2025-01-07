using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float accelerationTime = 0.2f;
    private CharacterController controller;
    private Vector3 moveDirection;
    private Vector3 currentVelocity;
    private bool isGrounded;
    [SerializeField] private float gravity = -9.81f;

    public Transform cameraTransform;
    public Transform playerModel;
    public Animator animator;

    private bool isBraking = false; // Track if braking should be happening

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            // Reset jump animation state if on the ground
            if (animator.GetBool("IsJumping"))
            {
                animator.SetBool("IsJumping", false);
            }

            // Capture input for movement
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            // Get the camera's forward and right directions
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            // Calculate movement direction based on camera orientation
            Vector3 desiredMoveDirection = (forward * moveZ + right * moveX).normalized;

            if (moveX != 0 || moveZ != 0)
            {
                // Player is moving, update velocity
                currentVelocity = Vector3.Lerp(currentVelocity, desiredMoveDirection * moveSpeed, accelerationTime * Time.deltaTime);

                // Stop braking animation when movement resumes
                if (isBraking)
                {
                    animator.SetBool("Braking", false);
                    isBraking = false;
                }

                // Rotate the player model based on movement direction
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 90, 0f);
                playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * 10f);

                // Update the running speed based on movement magnitude
                float velocityMagnitude = currentVelocity.magnitude;
                float normalizedSpeed = Mathf.Clamp01(velocityMagnitude / moveSpeed);
                animator.SetFloat("RunSpeed", normalizedSpeed);
            }
            else
            {
                // Player stopped giving input, check for momentum
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, accelerationTime * Time.deltaTime);
                animator.SetFloat("RunSpeed", 0);

                // Start braking animation if the player was moving but has stopped input
                if (currentVelocity.magnitude > 8f && !isBraking)
                {
                    animator.SetBool("Braking", true);
                    isBraking = true; // Set braking flag to true
                }
                else if (currentVelocity.magnitude <= 2f && isBraking)
                {
                    // Stop braking once the player fully stops
                    animator.SetBool("Braking", false);
                    isBraking = false;
                }
            }

            // Jumping logic
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetBool("IsJumping", true);
            }
        }

        // Apply gravity and movement
        moveDirection.x = currentVelocity.x;
        moveDirection.z = currentVelocity.z;
        moveDirection.y += gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
