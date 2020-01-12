using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectController : MonoBehaviour
{
    public UnityEvent OnCollect;

    protected void InvokeEvents()
    {
        OnCollect.Invoke();
    }
}
