using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO this is code to be written in game
public class TurretAim : Aim
{  
    public override Drone FindTarget()
    {
        for (int i = drones.Count - 1; i >= 0; i--)
        {
            float distance = Vector3.Distance(transform.position, drones[i].transform.position);

            if (distance < oldDistance)
            {
                oldDistance = distance;

                Drone d = drones[i].GetComponent<Drone>();
                return d;
            }
        }

        return null;
    }
}
