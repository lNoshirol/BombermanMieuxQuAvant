using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeLang : MonoBehaviour
{
    public List<TradOption> textToChange;

    public void ChangeAllText(string langId)
    {
        
            foreach (TradOption text in textToChange)
            {
            if (text != null)
            {
                text.Change(langId);
            }
        }

    }
}
