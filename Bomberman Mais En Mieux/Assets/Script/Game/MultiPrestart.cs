using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPrestart : MonoBehaviour
{
    private MuultiplayerManager joinManager;

    private void Start()
    {
        Time.timeScale = 0;
        joinManager = GetComponent<MuultiplayerManager>();
    }

    public void OnStartGame()
    {
        joinManager.GetComponent<PlayerInputManager>().enabled = false;
        Time.timeScale = 1f;
    }
}
