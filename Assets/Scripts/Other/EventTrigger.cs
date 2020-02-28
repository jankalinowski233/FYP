using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerTrigger;
    public UnityEvent OnDestructableTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerTrigger.Invoke();

        if (other.CompareTag("Destructable"))
            OnDestructableTrigger.Invoke();          
    }
}
