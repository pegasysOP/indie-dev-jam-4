using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Rigidbody rb;
    public Transform camPivot;
    public Transform groundPoint;

    [Header("Speed")]
    public float walkAcceleration;
    public float maxWalkSpeed;
    public float groundDrag;

    public float airAccelerationMultiplier;
    public float maxAirSpeed;
    public float airDragMultiplier;

    [Header("Aiming")]
    public float xSensitivity;
    public float ySensitivity;
    public float yLookMin;
    public float yLookMax;

    [Header("Jump")]
    public LayerMask groundMask;
    public float groundCheckDistance;
    public float jumpForce;
    public float jumpCooldown;

    private Vector3 moveDirection;
    private float verticalInput, horizontalInput;
    private float xRotation, yRotation;
    private bool grounded;
    private bool canJump = true;
    private float sensitivityScale = 1f;

    private void Start()
    {
        sensitivityScale = PlayerPrefs.GetFloat(PrefDefines.SensitivityKey, 1f);
        PlayerPrefs.SetFloat(PrefDefines.SensitivityKey, sensitivityScale);
    }

    private void Update()
    {
        HandleAiming();
        GetMoveInput();
        HandleGroundCheck();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleAiming()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity * sensitivityScale * 0.005f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * ySensitivity * sensitivityScale * 0.005f;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, yLookMin, yLookMax);

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        camPivot.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void GetMoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (Input.GetKey(KeyCode.Space) && grounded && canJump)
            Jump();
    }

    private void HandleMovement()
    {
        if (grounded)
            rb.AddForce(moveDirection.normalized * walkAcceleration * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * walkAcceleration * 10f * airAccelerationMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (grounded && horizontalVelocity.magnitude > maxWalkSpeed)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * maxWalkSpeed;
            rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
        }
        else if (!grounded && horizontalVelocity.magnitude > maxAirSpeed)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * maxAirSpeed;
            rb.velocity = new Vector3(clampedVelocity.x, rb.velocity.y, clampedVelocity.z);
        }
    }

    private void HandleGroundCheck()
    {
        grounded = Physics.OverlapSphere(groundPoint.position, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore).Count() > 0;
        rb.drag = grounded ? groundDrag : groundDrag * airDragMultiplier;
    }

    private void Jump()
    {
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundPoint.position, groundCheckDistance);
    }
}