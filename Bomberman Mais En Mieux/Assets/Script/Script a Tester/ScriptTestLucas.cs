using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTestLucas
{
    public int value;

    public event Action<int> OnValueChange;

    public int AddValue(int a)
    {
        value = Mathf.Clamp(value + a, -50, 50);
        OnValueChange?.Invoke(value);
        return value;
    }

    public int SubstractValue(int a) 
    {
        value = Mathf.Clamp(value - a, -50, 50);
        OnValueChange?.Invoke(value);
        return value;
    }
}
