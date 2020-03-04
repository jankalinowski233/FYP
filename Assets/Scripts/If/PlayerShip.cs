using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    public void Shoot()
    {
        // play particle system etc
            // kill an enemy
    }
}

//public class Attack : ShipAttack
//{
//    public void Aim(bool target)
//    {
//        if(target)
//        {
//            Shoot();
//        }
//    }
//}

    // TODO think how to set it all up

public class PlayerShip : Ship
{
    public bool shouldShoot;

    private void Start()
    {
        shouldShoot = false;
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
