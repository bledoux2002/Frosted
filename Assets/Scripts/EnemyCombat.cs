using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public int damage = 10;
    public float fireRate = 2f;
    private float nextAttackTime = 0f;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + fireRate;

        // hitscan example
        if (Physics.Raycast(transform.position,
                             transform.forward,
                             out RaycastHit hit,
                             100f))
        {
            hit.collider.GetComponent<HealthManager>()?.TakeDamage(damage);
        }
    }
}
