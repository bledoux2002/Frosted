using UnityEngine;

public class HealthManager : MonoBehaviour
{

    private GameManager GameManager;

    public int maxHealth = 100;
    public int HP { get; private set; }

    public int maxArmor = 0;
    public int Armor { get; private set; }

    void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HP = maxHealth;
        Armor = 0;
    }

    public void TakeDamage(int damage)
    {
        int armorAbsorb = Mathf.Min(Armor, damage);
        Armor -= armorAbsorb;
        damage -= armorAbsorb;

        HP -= damage;

        if (gameObject.GetComponent<PlayerController>())
            GameManager.UpdateUI();

        if (HP <= 0)
            Die();
    }

    public bool Heal(int amount)
    {
        if (HP == maxHealth)
            return false;
        HP = Mathf.Min(maxHealth, HP + amount);
        GameManager.UpdateUI();
        return true;
    }

    public bool AddArmor(int amount)
    {
        if (Armor == maxArmor)
            return false;
        Armor = Mathf.Min(maxArmor, Armor + amount);
        GameManager.UpdateUI();
        return true;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
