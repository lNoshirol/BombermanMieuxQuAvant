using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPrestart : MonoBehaviour
{
    private PlayerJoinManager joinManager;

    private void Start()
    {
        Time.timeScale = 0;
        joinManager = GetComponent<PlayerJoinManager>();
    }

    public void OnStartGame()
    {
        joinManager.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
