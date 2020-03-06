using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfEventTrigger : EventTrigger
{
    public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        ShipAttack att = gameObject.GetComponentInParent<ShipAttack>();
        att.SetTarget(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
