using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Base class for an in-game code
public class ShipAttack : MonoBehaviour
{
    public GameObject target; // Target of a player

    // Sets target
    public void SetTarget(GameObject t)
    {
        target = t; 
    }

    // Starts coroutine to shoot
    public void Shoot()
    {
        StartCoroutine(KillEnemy());
    }

    IEnumerator KillEnemy()
    {   
        // If there is a target, kill it almost immediately
        if(target != null)
        {
            EnemyShip sh = target.GetComponent<EnemyShip>();
            yield return new WaitForSeconds(0.1f);
            sh.Die();
            target = null;
        }
    }

    // A virtual function to be written by users (polymorphism)
    public virtual void Aim(bool target)
    {

    }
}

// Player ship class - a premade script to control flow of the game
public class PlayerShip : Ship
{
    // Properties
    public bool shouldShoot;
    public ShipAttack att; // Polymorphic reference to the script created by users

    // Event texts
    public Text shootText;
    public Text aimText;

    private void Start()
    {
        shouldShoot = false;
    }

    private void Update()
    {
        // If users written their code
        if(att != null)
            att.Aim(shouldShoot);

        // Change text based on in-game events
        if (shouldShoot == true)
        {
            shootText.color = new Color(shootText.color.r, shootText.color.g, shootText.color.b, 1.0f);
            aimText.color = new Color(aimText.color.r, aimText.color.g, aimText.color.b, 0.2f);
        }
        else
        {
            shootText.color = new Color(shootText.color.r, shootText.color.g, shootText.color.b, 0.2f);
            aimText.color = new Color(aimText.color.r, aimText.color.g, aimText.color.b, 1.0f);
        }
    }

    public override void Die() // Triggered if players did not complete the code correctly and died
    {
        base.Die();
    }

    // Sets shoot boolean
    public void SetShoot(bool shoot)
    {
        shouldShoot = shoot;
    }
}
