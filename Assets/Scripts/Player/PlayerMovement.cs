using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    public float maxWalkIncline;

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
    private Vector3 groundNormal = Vector3.up;
    private float drag;
    private float sensitivityScale = 1f;

    public bool IsGrounded {  get { return grounded; } }
    public Vector3 MoveDirection {  get { return moveDirection; } }

    private void Start()
    {
        sensitivityScale = PlayerPrefs.GetFloat(PrefDefines.SensitivityKey, 1f);
        PlayerPrefs.SetFloat(PrefDefines.SensitivityKey, sensitivityScale);
    }

    private void Update()
    {
        HandleAiming();
        GetMoveInput();
    }

    private void FixedUpdate()
    {
        HandleGroundCheck();

        HandleMovement();
        SpeedControl();

        ApplyGravity();
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
            rb.AddForce(moveDirection.normalized * walkAcceleration * 10f, ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * walkAcceleration * 10f * airAccelerationMultiplier, ForceMode.Acceleration);
    }

    private void SpeedControl()
    {
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // apply drag on horizontal only
        float drag = grounded ? groundDrag : groundDrag * airDragMultiplier;
        Vector3 dragForce = drag * -horizontalVelocity;
        rb.AddForce(dragForce, ForceMode.Acceleration);

        // clamp velocity
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

    private void ApplyGravity()
    {
        Vector3 gravity = -groundNormal * Physics.gravity.magnitude * rb.mass;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void HandleGroundCheck()
    {
        grounded = Physics.CheckSphere(groundPoint.position, groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);        

        if (grounded)
        {
            RaycastHit hit;
            if (Physics.Raycast(groundPoint.position, Vector3.down, out hit, groundCheckDistance + 0.1f, groundMask))
            {
                groundNormal = hit.normal;

                // if incline is too steep don't count as grounded
                if (Vector3.Angle(groundNormal, Vector3.up) > maxWalkIncline)
                {
                    groundNormal = Vector3.up;
                    grounded = false;
                }
            }
        }
        else
        {
            groundNormal = Vector3.up;
        }
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

    public void SetSensitivity(float sensitivityScale)
    {
        this.sensitivityScale = sensitivityScale;
    }
}