using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionVsBot : MonoBehaviour
{
    public static WinConditionVsBot instance;

    [SerializeField] private GameObject _winPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void CheckWinCondition(GameObject go, int pv)
    {

    }
}
