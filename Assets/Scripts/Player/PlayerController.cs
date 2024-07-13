using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Transform cameraPivot;

    [Header("Walking")]
    public float walkAcceleration;
    public float maxWalkSpeed;
    public float groundDrag;

    [Header("Aiming")]
    public float xSensitivity;
    public float ySensitivity;

    [Header("Jump")]
    public float playerHeight;
    public LayerMask groundMask;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    Vector3 moveDirection;
    float verticalInput, horizontalInput;
    float xRotation, yRotation;
    bool grounded;
    bool canJump = true;

    private bool locked = false;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!locked)
        {
            HandleAiming();
            GetMoveInput();
            HandleGroundCheck();
            SpeedControl();            
        }

        HandlePauseMenu();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleAiming()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * xSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        cameraPivot.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    private void GetMoveInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && grounded && canJump)
            Jump();
    }

    private void HandleMovement()
    {
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (grounded)
            _rigidbody.AddForce(moveDirection.normalized * walkAcceleration * 10f, ForceMode.Force);
        else
            _rigidbody.AddForce(moveDirection.normalized * walkAcceleration * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 horizontalVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        if (horizontalVelocity.magnitude >  maxWalkSpeed)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * maxWalkSpeed;
            _rigidbody.velocity = new Vector3(clampedVelocity.x, _rigidbody.velocity.y, clampedVelocity.z);
        }
    }

    private void HandleGroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        if (grounded)
            _rigidbody.drag = groundDrag;
        else
            _rigidbody.drag = 0;
    }

    private void Jump()
    {
        canJump = false;
        Invoke(nameof(ResetJump), jumpCooldown);

        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void HandlePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu.Toggle();
    }

    public static void Lock(bool locked)
    {
        Instance.locked = locked;
    }
}