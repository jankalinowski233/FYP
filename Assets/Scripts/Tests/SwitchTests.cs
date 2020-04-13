using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SwitchTests
{
    [Test]
    public void GoalReachedTest()
    {
        GameObject go = new GameObject("Temp");
        SwitchPlayerMove spm = go.AddComponent<SwitchPlayerMove>();

        GameObject tempTarget = new GameObject("Temp target");
        spm.goalReachedTarget = tempTarget;

        spm.GoalReached();

        Assert.AreSame(tempTarget, spm.GetTarget());
        Assert.AreEqual(true, spm.GetGoalReached());
    }
}
