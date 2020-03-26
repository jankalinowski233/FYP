using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    public delegate void Death(GameObject d);
    public Death deathEvent;

    public Transform t;
    public float speed;

    public float health;
    float remainingHealth;
    public Image healthBar;

    private void Start()
    {
        remainingHealth = health;
    }

    public void TakeDamage(float dmg)
    {
        remainingHealth -= dmg;
        healthBar.fillAmount = remainingHealth / health;

        if(remainingHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        deathEvent?.Invoke(gameObject); // invoke delegate
        gameObject.SetActive(false);
    }
}
