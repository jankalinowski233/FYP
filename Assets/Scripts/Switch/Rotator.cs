using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : ObjectController
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 20f * Time.deltaTime, 0);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InvokeEvents();

            switch (transform.position.x)
            {
                case 5:
                    gameObject.transform.position = new Vector3(-5, transform.position.y, transform.position.z);
                    break;
                case -5:
                    gameObject.transform.position = new Vector3(5, transform.position.y, transform.position.z);
                    break;
            }
        }
    }
}
