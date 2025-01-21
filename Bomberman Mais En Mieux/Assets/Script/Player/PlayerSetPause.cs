using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetPause : MonoBehaviour
{
    public event Action Pause;

    private void Awake()
    {
        Pause += SetPauseManager.instance.SetPauseTrigger;
    }

    public void OnSetPauseTrigger(InputAction.CallbackContext callbackContext)
    {
        Pause?.Invoke();
    }
}
