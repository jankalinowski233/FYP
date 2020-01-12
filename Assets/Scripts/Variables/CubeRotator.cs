using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : ObjectController
{
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InvokeEvents();
            Destroy(gameObject);
        }
    }
}
