using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestLucas
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestLucasSimplePasses()
    {
        // -- Arrange --
        GameObject go = new();

        PlayerHealth playerHealth = go.AddComponent<PlayerHealth>();

        int oldHealth = playerHealth.pv;
        int damageYouWillTake = 1;

        playerHealth.damageMultiplier = damageYouWillTake;

        int healthCaughtInEvent = 0;
        playerHealth.OnDamageTake += (score) => healthCaughtInEvent = score;

        // -- Act --

        playerHealth.takeDamage();
        int newHealth = playerHealth.pv;

        // -- Assert --

        Assert.That(newHealth, Is.EqualTo(oldHealth - damageYouWillTake));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestLucasWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
