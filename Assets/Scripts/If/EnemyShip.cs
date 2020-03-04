using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventTrigger))]
public class EnemyShip : Ship
{
    EventTrigger ev;

    private void Start()
    {
        ev = GetComponent<EventTrigger>();    
    }

    public override void Die()
    {
        base.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ev.OnPlayerTrigger.Invoke();
        }
    }
}
