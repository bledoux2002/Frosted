using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    //public PlayerController playerController;
    //public Weapon weapon; // prefab for instantiation when picked up
    //public Vector3 positionOffset = new Vector3(0, 1.5f, 0.5f);
    private Vector3 rotation = new Vector3(0, 90, 0);

    // private Dictionary<string, int> ammo;

    protected float startHeight;

    void Start()
    {
        startHeight = transform.position.y;
        OnStart();
    }

    protected virtual void OnStart() {}
    
    // Update is called once per frame
    void Update()
    {
        Hover();
        OnUpdate();
    }

    protected virtual void OnUpdate() {}

    protected void Hover()
    {
        // Continuously rotate object
        transform.Rotate(rotation * Time.deltaTime);
        
        // Object floats up and down
        transform.position = new Vector3(transform.position.x, startHeight + (Mathf.Sin(Time.time) * 0.1f), transform.position.z);
    }

}
