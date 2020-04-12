using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Controls various objects
// Easily reusable
public class ObjectController : MonoBehaviour
{
    public UnityEvent OnCollect;

    protected void InvokeEvents()
    {
        OnCollect.Invoke();
    }
}
