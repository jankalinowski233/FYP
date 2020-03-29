using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Drone : MonoBehaviour
{
    public delegate void Death(GameObject d);
    public Death deathEvent;

    public Transform t;
    public float speed;

    public float health;
    float remainingHealth;
    public Image healthBar;

    public ParticleSystem hitEffect;
    public UnityEvent OnDeath;

    public GameObject deathParticles;

    private void Start()
    {
        remainingHealth = health;
    }

    public void TakeDamage(float dmg)
    {
        remainingHealth -= dmg;
        healthBar.fillAmount = remainingHealth / health;
        hitEffect.Play();

        if(remainingHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        deathEvent?.Invoke(gameObject); // invoke delegate
        OnDeath.Invoke();
        GameObject x = Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
