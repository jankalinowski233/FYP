using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{
    public delegate void Death(GameObject d); // Death event delegate
    public Death deathEvent;

    // Drone properties
    public Transform t;
    public float speed;

    public float health;
    float remainingHealth;
    public Image healthBar;

    public ParticleSystem hitEffect;
    public UnityEvent OnDeath; // Unity Event to be triggered when a drone dies

    public GameObject deathParticles;

    private void Start()
    {
        SetRemainingHealth(health); // Init health
    }

    public void TakeDamage(float dmg) // Deals damage to a drone
    {
        remainingHealth -= dmg;

        if(healthBar != null)
            healthBar.fillAmount = remainingHealth / health;

        if(hitEffect != null)
            hitEffect.Play();

        if(remainingHealth <= 0)
        {
            Die(); // Die if health is less than 0
        }
    }

    public void Die()
    {
        deathEvent?.Invoke(gameObject); // invoke delegate
        OnDeath.Invoke(); // Invoke UnityEvent
        GameObject x = Instantiate(deathParticles, transform.position, Quaternion.identity); // Instantiate particles
        gameObject.SetActive(false);
    }

    public void SetRemainingHealth(float h)
    {
        remainingHealth = h;
    }

    public float GetRemainingHealth()
    {
        return remainingHealth;
    }
}
