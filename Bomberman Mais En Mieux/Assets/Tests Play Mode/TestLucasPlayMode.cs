using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestLucasPlayMode
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestLucasPlayModeSimplePasses()
    {
        // -- Arrange --
        ScriptTestLucas simplePass = new ScriptTestLucas();

        int oldValue = simplePass.value;
        int valueToSubstract = 56;

        int valueCaughtInEvent = 0;
        simplePass.OnValueChange += (score) => valueCaughtInEvent = score;

        // -- Act --

        int returnedValue = simplePass.SubstractValue(valueToSubstract);
        int newValue = simplePass.value;

        // -- Assert --

        Assert.That(returnedValue, Is.EqualTo(Mathf.Clamp(oldValue - valueToSubstract, -50, 50)));
        Assert.That(newValue, Is.EqualTo(returnedValue));
        Assert.That(valueCaughtInEvent, Is.EqualTo(returnedValue));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestLucasPlayModeWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
