using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all ships
public class Ship : MonoBehaviour
{
    public GameObject explosionPrefab; // Explosion VFX

    public virtual void Die()
    {
        // Base death behaviour
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
