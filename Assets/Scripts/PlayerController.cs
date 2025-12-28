using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float sprintSpeed = 10f;
    public float gravity = -20f;

    [Header("Look")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraPivot;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    private CharacterController controller;
    private HealthManager HealthManager;
    [HideInInspector] public bool Paused;
    private Vector3 velocity;

    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction lookAction;

    private float pitch;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        HealthManager = GetComponent<HealthManager>();

        moveAction = InputSystem.actions.FindAction("Move");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!Paused)
        {
            HandleLook();
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 move =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        move = move.normalized * (sprintAction.ReadValue<float>() > 0f ? sprintSpeed : moveSpeed);

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

    void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.gameObject.GetComponent<Pickup>();
        bool del = false;

        switch (pickup)
        {
            case Health healthPack:
                del = HealthManager.Heal(healthPack.Amount);
                if (del)
                    Destroy(healthPack.gameObject);
                break;

            case Armor armorPack:
                del = HealthManager.AddArmor(armorPack.Amount);
                if (del)
                    Destroy(armorPack.gameObject);
                break;

            case Mask mask:
                Debug.Log($"Mask acquired: {mask.type}");
                Destroy(mask.gameObject);
                break;
        }
    }
}
