using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent OnPlayerTrigger;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnPlayerTrigger.Invoke();
    }
}
