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
        ScriptTestLucas simplePass = new ScriptTestLucas();

        int oldValue = simplePass.value;
        int valueToAdd = 56;

        int valueCaughtInEvent = 0;
        simplePass.OnValueChange += (score) => valueCaughtInEvent = score;

        // -- Act --

        int returnedValue = simplePass.AddValue(valueToAdd);
        int newValue = simplePass.value;

        // -- Assert --

        Assert.That(returnedValue, Is.EqualTo(Mathf.Clamp(oldValue + valueToAdd, -50, 50)));
        Assert.That(newValue, Is.EqualTo(returnedValue));
        Assert.That(valueCaughtInEvent, Is.EqualTo(returnedValue));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestLucasWithEnumeratorPasses()
    {
        

        yield return null;
    }
}
