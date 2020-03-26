using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aim : MonoBehaviour
{
    public List<GameObject> drones;
    [SerializeField]
    protected float oldDistance;

    protected void Start()
    {
        GameObject[] ds = GameObject.FindGameObjectsWithTag("Target");
        
        foreach(GameObject d in ds)
        {
            drones.Add(d);
            d.GetComponent<Drone>().deathEvent += RemoveDrone;
        }
    }

    public void RemoveDrone(GameObject drone)
    {
        drones.Remove(drone);
        oldDistance = 25.0f;
    }

    public abstract Drone FindTarget();
}

public class Turret : MonoBehaviour
{
    public float dmg;
    public Drone target;
    public Transform turret;
    public Aim aim;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponent<TurretAim>();
    }

    // Update is called once per frame
    protected void Update()
    {
        target = aim.FindTarget();

        if(target != null)
        {
            Vector3 targetRotation = target.transform.position - turret.position;
            targetRotation.y = 0f;
            turret.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), 1f);

            target.TakeDamage(dmg);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target"))
        {
            // Game over
        }
    }
}
