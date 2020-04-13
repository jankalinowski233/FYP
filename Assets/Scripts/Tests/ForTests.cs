using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ForTests
{
    [Test]
    public void GetDamageTest()
    {
        GameObject go = new GameObject("Drone");
        Drone d = go.AddComponent<Drone>();

        d.health = 100;
        d.SetRemainingHealth(d.health);

        d.TakeDamage(10);

        Assert.AreNotEqual(d.health, d.GetRemainingHealth());
    }

    class TempClass : TAim
    {
        public override Drone FindTarget()
        {
            GameObject tempGo = new GameObject("TempDrone");
            Drone d = tempGo.AddComponent<Drone>();
            return d;
        }
    }

    [Test]
    public void RemoveDroneTest()
    {
        GameObject go = new GameObject("Turret");
        Turret t = go.AddComponent<Turret>();
        TAim tAim = go.AddComponent<TempClass>();

        t.aim = tAim;

        GameObject drone1 = new GameObject("Drone1");
        GameObject drone2 = new GameObject("Drone2");

        t.aim.drones.Add(drone1);
        t.aim.drones.Add(drone2);
        int initCount = t.aim.drones.Count;

        t.aim.RemoveDrone(drone1);

        Assert.AreNotEqual(initCount, t.aim.drones.Count);
    }

    [Test]
    public void SetDroneTargetTest()
    {
        GameObject go = new GameObject("Turret");
        Turret t = go.AddComponent<Turret>();
        TAim tAim = go.AddComponent<TempClass>();

        t.aim = tAim;

        t.target = tAim.FindTarget();

        Assert.AreNotEqual(null, t.target);
    }
}
