using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public int maxArmor = 0;
    public int armor;

    void Awake()
    {
        health = maxHealth;
        armor = 0;
    }

    public void TakeDamage(int damage)
    {
        int armorAbsorb = Mathf.Min(armor, damage);
        armor -= armorAbsorb;
        damage -= armorAbsorb;

        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
