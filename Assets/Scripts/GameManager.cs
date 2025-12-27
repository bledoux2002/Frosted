using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance; //Singleton

    private InputAction pauseAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        pauseAction = InputSystem.actions.FindAction("Pause");
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
