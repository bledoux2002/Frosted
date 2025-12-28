using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager GameManager;
    private GameObject player;
    private EnemyCombat Combat;

    public float viewDistance = 10f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 10f;
    private bool engaged;


    void Start()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<PlayerController>().gameObject;
        Combat = GetComponent<EnemyCombat>();
        engaged = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= viewDistance || engaged)
        {
            //engaged = true;
            HandleLook();
            HandleMovement();
            Combat.Attack();
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

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
