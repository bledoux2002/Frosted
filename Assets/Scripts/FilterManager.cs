using UnityEngine;

public class FilterManager : MonoBehaviour
{
    private GameManager GameManager;
    private HealthManager HealthManager;
    public AudioSource FilterAudio;
    public int Filter { get; private set; }
    private int maxFilter = 100;
    public float tickRate;
    private float nextTick;

    void Awake()
    {
        GameManager = FindFirstObjectByType<GameManager>();
        HealthManager = GetComponent<HealthManager>();
        Filter = maxFilter;
        nextTick = Time.time + tickRate;
    }

    void Update()
    {
        if (Time.time >= nextTick)
        {
            if (Filter == 0)
            {
                HealthManager.TakeDamage(1);
            }
            nextTick = Time.time + tickRate;
            Filter = Mathf.Max(0, Filter - 1);
            GameManager.UpdateUI();
        }
    }

    public bool AddFilter(int amount)
    {
        if (Filter == maxFilter)
            return false;
        Filter = Mathf.Min(maxFilter, Filter + amount);
        FilterAudio.Play();
        GameManager.UpdateUI();
        return true;
    }
}
