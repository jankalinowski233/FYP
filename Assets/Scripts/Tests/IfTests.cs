using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class IfTests
{
   [Test]
   public void DestroyShipTest()
   {
        GameObject go = new GameObject("Ship");
        Ship sh = go.AddComponent<Ship>();

        GameObject testGo = new GameObject("Temp");
        sh.explosionPrefab = testGo;

        sh.Die();

        Assert.AreNotEqual(go.activeSelf, true);
   }

    [Test]
    public void SetShipTargetTest()
    {
        GameObject go = new GameObject("Ship");
        ShipAttack attack = go.AddComponent<ShipAttack>();

        GameObject target = new GameObject("Target");
        attack.SetTarget(target);

        Assert.AreNotEqual(null, attack.target);
    }

    [Test]
    public void SetBoolTest()
    {
        GameObject go = new GameObject("Ship");
        PlayerShip ship = go.AddComponent<PlayerShip>();

        ship.SetShoot(true);

        Assert.AreEqual(true, ship.shouldShoot);
    }
}
