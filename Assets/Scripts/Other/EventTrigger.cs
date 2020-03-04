using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerTrigger;
    public UnityEvent OnDestructableTrigger;

    [Header("Exits")]
    [Space(15f)]
    public UnityEvent OnPlayerExit;
    public UnityEvent OnDestructableExit;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerTrigger.Invoke();

        if (other.CompareTag("Destructable"))
            OnDestructableTrigger.Invoke();          
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerExit.Invoke();

        if (other.CompareTag("Destructable"))
            OnDestructableExit.Invoke();
    }
}
