using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        EnemyShip sh = target.GetComponent<EnemyShip>();
        yield return new WaitForSeconds(0.1f);
        sh.Die();
        target = null;
    }

    public virtual void Aim(bool target)
    {

    }
}

public class PlayerShip : Ship
{
    public bool shouldShoot;
    public ShipAttack att;

    private void Start()
    {
        shouldShoot = false;
    }

    private void Update()
    {
        att.Aim(shouldShoot);
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
