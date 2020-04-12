using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An enemy ship class
// Requires an EventTrigger component
[RequireComponent(typeof(EventTrigger))]
public class EnemyShip : Ship
{
    EventTrigger ev; // Reference to an event trigger script

    private void Start()
    {
        ev = GetComponent<EventTrigger>(); // Init
    }

    public override void Die()
    {
        base.Die(); // Trigger base behaviour
    }

    private void OnTriggerEnter(Collider other)
    {
        // Invoke an event if the object came in contact with a player
        if(other.CompareTag("Player"))
        {
            ev.OnPlayerTrigger.Invoke();
        }
    }
}
