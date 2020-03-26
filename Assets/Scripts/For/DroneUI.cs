using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not used, but left as the evidence of what I have tried while developing

public class DroneUI : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation;
        Vector3 objToCameraVector = mainCamera.transform.position - transform.position;

        rotation = Quaternion.LookRotation(-objToCameraVector);
        transform.rotation = rotation;
    }
}
