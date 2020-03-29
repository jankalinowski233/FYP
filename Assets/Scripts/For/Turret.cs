using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TAim : MonoBehaviour
{
    public List<GameObject> drones = new List<GameObject>();
    [SerializeField]
    protected float oldDistance;

    protected void Start()
    {
        oldDistance = 25.0f;
        GameObject[] ds = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject d in ds)
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
    public float rotationSpeed;

    public Drone target;
    public Transform turret;
    public TAim aim;

    LineRenderer line;
    public ParticleSystem laserBeamStart;
    public Transform laserStart;

    public Text lookingText;
    public Text foundText;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(aim != null)
        {
            target = aim.FindTarget();
        }

        if (target != null)
        {
            // Calculate rotation
            Vector3 targetRotation = target.transform.position - turret.position;
            targetRotation.y = 0f;
            turret.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), 1f);

            target.TakeDamage(dmg);

            // Show line from a turret to the enemy
            line.enabled = true;
            laserBeamStart.Play();

            line.SetPosition(0, laserStart.position);
            line.SetPosition(1, target.transform.position);

            lookingText.color = new Color(lookingText.color.r, lookingText.color.g, lookingText.color.b, 0.2f);
            foundText.color = new Color(foundText.color.r, foundText.color.g, foundText.color.b, 1.0f);
        }
        else
        {
            line.enabled = false;
            laserBeamStart.Stop();

            turret.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, Vector3.up);

            lookingText.color = new Color(lookingText.color.r, lookingText.color.g, lookingText.color.b, 1.0f);
            foundText.color = new Color(foundText.color.r, foundText.color.g, foundText.color.b, 0.2f);
        }
    }
}
