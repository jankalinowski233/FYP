using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject explosionPrefab;

    public virtual void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
