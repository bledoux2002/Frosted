using UnityEngine;

public class HealthManager : MonoBehaviour
{

    private GameManager GameManager;
    public AudioSource DamageAudio;
    public AudioSource HealAudio;
    public AudioSource DieAudio;


    public int maxHealth = 100;
    public int HP { get; private set; }

    void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HP = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;

            if (Random.Range(0f, 1f) <= 0.1f)
                DamageAudio.Play();

            if (gameObject.GetComponent<PlayerController>())
                GameManager.UpdateUI();
            else
            {
                gameObject.GetComponent<EnemyController>().Engaged = true;
            }
            if (HP <= 0)
                Die();
        }
    }

    public bool Heal(int amount)
    {
        if (HP == maxHealth)
            return false;
        HP = Mathf.Min(maxHealth, HP + amount);
        HealAudio.Play();
        GameManager.UpdateUI();
        return true;
    }

    void Die()
    {
        if (GetComponent<EnemyController>())
        {
            DieAudio.transform.parent = null;
            DieAudio.Play();
            Destroy(DieAudio.gameObject, DieAudio.clip.length);
            Destroy(gameObject, DieAudio.clip.length);
        }
        else
        {
            GameManager.GameOver();
        }
    }
}
