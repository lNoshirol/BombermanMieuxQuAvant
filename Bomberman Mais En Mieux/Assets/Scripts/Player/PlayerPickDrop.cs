using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickDrop : MonoBehaviour
{
    [SerializeField] Transform bombeTransform;
    private PlayerActions _playerInput;
    private GameObject bombe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bombe")
        {
            other.transform.position = bombeTransform.position;
            other.gameObject.transform.SetParent(bombeTransform);
            bombe = other.gameObject;

            Debug.Log("Attraper");
        }
    }

    private void Awake()
    {
        _playerInput = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void OnDrop(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Je drop");

        bombeTransform.DetachChildren();
        bombe.GetComponent<SphereCollider>().enabled = false;
        bombe.GetComponent<Bombe>().StartExplosion();


    }

}
