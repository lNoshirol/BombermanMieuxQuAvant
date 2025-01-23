using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestEmrysPlayMode
{
    [Test]
    public void Multiplication_ShouldWork()
    {
        // -- Arrange --
        ScriptTestEmrys emrysTest = new ScriptTestEmrys();
        emrysTest.number = 2;

        int oldValue = emrysTest.number;
        int multiplyNumber = 3;
        int numberCaughtInEvent = 0;

        emrysTest.OnValueChange += (result) =>
        {
            numberCaughtInEvent = result;
            Debug.Log($"Event fired with result: {result}");
        };

        // -- Act -- 
        int returnedNumber = emrysTest.MultiplyNumber(multiplyNumber);
        int newNumber = emrysTest.number;

        // -- Assert -- 
        Assert.That(returnedNumber, Is.EqualTo(oldValue * multiplyNumber), "Returned number is incorrect.");
        Assert.That(newNumber, Is.EqualTo(returnedNumber), "New number is incorrect.");
        Assert.That(numberCaughtInEvent, Is.EqualTo(returnedNumber), "Event result is incorrect.");
    }
}
