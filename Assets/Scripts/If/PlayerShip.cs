using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipAttack : MonoBehaviour
{
    public GameObject target;

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public void Shoot()
    {
        StartCoroutine(KillEnemy());
    }

    IEnumerator KillEnemy()
    {       
        if(target != null)
        {
            EnemyShip sh = target.GetComponent<EnemyShip>();
            yield return new WaitForSeconds(0.1f);
            sh.Die();
            target = null;
        }
    }

    public virtual void Aim(bool target)
    {

    }
}

public class PlayerShip : Ship
{
    public bool shouldShoot;
    public ShipAttack att;

    public Text shootText;
    public Text aimText;

    private void Start()
    {
        shouldShoot = false;
    }

    private void Update()
    {
        if(att != null)
            att.Aim(shouldShoot);

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

    public override void Die()
    {
        base.Die();
    }

    public void SetShoot(bool shoot)
    {
        shouldShoot = shoot;
    }
}
