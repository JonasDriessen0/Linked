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

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            if (animator.GetBool("IsJumping"))
            {
                animator.SetBool("IsJumping", false);
            }
            
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");
            
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            
            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();
            
            Vector3 desiredMoveDirection = (forward * moveZ + right * moveX).normalized;
            
            if (moveX != 0 || moveZ != 0)
            {
                currentVelocity = Vector3.Lerp(currentVelocity, desiredMoveDirection * moveSpeed, accelerationTime * Time.deltaTime);
                
                Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
                targetRotation = Quaternion.Euler(-90f, targetRotation.eulerAngles.y + 90, 0f);
                playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * 10f);
                
                float velocityMagnitude = currentVelocity.magnitude;
                float normalizedSpeed = Mathf.Clamp01(velocityMagnitude / moveSpeed);
                animator.SetFloat("RunSpeed", normalizedSpeed);
            }
            else
            {
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, accelerationTime * Time.deltaTime);
                animator.SetFloat("RunSpeed", 0);
            }
            
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetBool("IsJumping", true);
            }
        }
        
        moveDirection.x = currentVelocity.x;
        moveDirection.z = currentVelocity.z;
        
        moveDirection.y += gravity * Time.deltaTime;
        
        controller.Move(moveDirection * Time.deltaTime);
    }
}
