using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTestEmrys
{
    public int number = 2; 

    public event Action<int> OnValueChange;

    public int MultiplyNumber(int a)
    {
        number = number * a;
        OnValueChange?.Invoke(number);
        return number;
    }

    public int DividNumber(int a)
    {
        number = number / a;
        OnValueChange?.Invoke(number);
        return number;
    }
}
