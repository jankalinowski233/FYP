using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for an in-game turret code
public abstract class TAim : MonoBehaviour
{
    public List<GameObject> drones = new List<GameObject>(); // list of active drones in the scene
    [SerializeField]
    protected float oldDistance; // Distance to a drone

    protected void Start()
    {
        oldDistance = 25.0f; // Init old distance
        GameObject[] ds = GameObject.FindGameObjectsWithTag("Target"); // Find all GameObjects with tag

        foreach (GameObject d in ds) // Add them to the list
        {
            drones.Add(d);
            d.GetComponent<Drone>().deathEvent += RemoveDrone; // Delegate subscribes to a method
        }
    }

    public void RemoveDrone(GameObject drone) // Removes drone from a list
    {
        drones.Remove(drone);
        oldDistance = 25.0f; // Reset old distance
    }

    public abstract Drone FindTarget(); // A function to be written in game
}

public class Turret : MonoBehaviour
{
    // Turret properties
    public float dmg;
    public float rotationSpeed;

    public Drone target;
    public Transform turret;
    public TAim aim;

    LineRenderer line; // Line renderer
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
        if(aim != null) // Triggered if users completed their code
        {
            target = aim.FindTarget();
        }

        if (target != null) // If there is a target
        {
            // Calculate rotation
            Vector3 targetRotation = target.transform.position - turret.position;
            targetRotation.y = 0f;
            turret.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), 1f); // Rotate towards a target

            target.TakeDamage(dmg); // Deal damage

            // Show line from a turret to the enemy
            line.enabled = true;
            laserBeamStart.Play(); // Play particle system
            line.SetPosition(0, laserStart.position);
            line.SetPosition(1, target.transform.position);

            // Set texts color based on in-game events
            lookingText.color = new Color(lookingText.color.r, lookingText.color.g, lookingText.color.b, 0.2f);
            foundText.color = new Color(foundText.color.r, foundText.color.g, foundText.color.b, 1.0f);
        }
        else
        {
            // Disable a line
            line.enabled = false;
            laserBeamStart.Stop(); // Stop particle system

            // Rotate around y axis
            turret.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, Vector3.up);

            // Set texts color based on in-game events
            lookingText.color = new Color(lookingText.color.r, lookingText.color.g, lookingText.color.b, 1.0f);
            foundText.color = new Color(foundText.color.r, foundText.color.g, foundText.color.b, 0.2f);
        }
    }
}
