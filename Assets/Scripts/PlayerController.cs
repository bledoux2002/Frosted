using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float gravity = -20f;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraPivot;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private CharacterController controller;
    private Vector3 velocity;

    private InputAction moveAction;
    private InputAction lookAction;

    private float pitch;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        move = move.normalized * moveSpeed;

        if (controller.isGrounded)
            velocity.y = 0f;

        velocity.y += gravity * Time.deltaTime;

        controller.Move((move + velocity) * Time.deltaTime);
    }

    private void HandleLook()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Yaw (player)
        transform.Rotate(Vector3.up * mouseX);

        // Pitch (camera)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
