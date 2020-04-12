using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Not used
// Simple pan script
public class SimplePan : MonoBehaviour
{
    public Vector3 panAxis;
    public float maxTime;
    float timeLeft;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        timeLeft += Time.deltaTime;
        if(timeLeft > maxTime)
        {
            panAxis = -panAxis;
            timeLeft = 0.0f;
        }

        transform.position += transform.TransformDirection(panAxis * speed * Time.deltaTime); // Pan from point A to point B
                                                                                            // On a given axis with a given speed
    }
}
