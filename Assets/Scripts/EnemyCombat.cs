using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public AudioSource FireAudio;

    public int damage = 10;
    public float fireRate = 2f;
    private float nextAttackTime = 0f;

    public void Attack()
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + fireRate;

        FireAudio.Play();

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
