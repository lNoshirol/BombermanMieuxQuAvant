using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestEmrys
{

    [UnityTest]
    public void TakeDamage_ShouldWork()
    {
        // -- Arrange --
        PlayerHealth playerhealth = new PlayerHealth();
        int oldHealth = 3;
        int damage = 1;

        int healthCaughtInEvent = 0;

        playerhealth.OnDamageTake += (health) => healthCaughtInEvent = health;
        // -- Act -- 

        playerhealth.takeDamage();
        int newHealth = playerhealth.pv;
        // -- Assert -- 

        Assert.That(newHealth, Is.EqualTo(oldHealth - damage));
        Assert.That(healthCaughtInEvent, Is.EqualTo(newHealth));
    }

}
