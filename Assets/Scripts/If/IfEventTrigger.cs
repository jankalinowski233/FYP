using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An event trigger specific for 3rd level
// Triggers base functions of the derived class, but adds functionality on top of it
public class IfEventTrigger : EventTrigger
{
    public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // Trigger base function

        // Set target
        ShipAttack att = gameObject.GetComponentInParent<ShipAttack>();
        att.SetTarget(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        // Trigger base function
        base.OnTriggerExit(other);
    }
}
