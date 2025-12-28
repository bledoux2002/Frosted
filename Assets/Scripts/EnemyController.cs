using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyController : MonoBehaviour
{
    private GameManager GameManager;
    private GameObject player;
    private EnemyCombat Combat;
    private CharacterController controller;
    [HideInInspector] public bool Paused;
    [HideInInspector] public bool Engaged;

    public float viewDistance = 10f;
    public float disengageDistance = 20f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
        Combat = GetComponent<EnemyCombat>();
        Paused = false;
        Engaged = false;
    }

    void Update()
    {
        if (!Paused)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist <= viewDistance || Engaged)
            {
                //Engaged = true;
                HandleLook();
                HandleMovement();
                Combat.Attack();
            }
            //else if (dist >= disengageDistance)
                //Engaged = false;
        }
    }

    private void HandleLook()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void HandleMovement()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        
        direction = direction.normalized;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f))
        {
            // Wall detected -> slide
            Vector3 slide = Vector3.ProjectOnPlane(direction, hit.normal);
            controller.Move(slide * moveSpeed * Time.deltaTime);
        }
        else
            controller.Move(direction * moveSpeed * Time.deltaTime);
    }
}
