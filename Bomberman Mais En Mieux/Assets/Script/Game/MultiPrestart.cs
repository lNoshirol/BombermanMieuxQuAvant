using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiPrestart : MonoBehaviour
{
    private MuultiplayerManager joinManager;

    public List<GameObject> gameObjectsToUnactiveAtStart;

    public GameObject panelCanStart;

    private void Start()
    {
        Time.timeScale = 0;
        joinManager = GetComponent<MuultiplayerManager>();
    }

    public void OnStartGame()
    {
        if (joinManager.playerThatJoined >= 2 )
        {
            joinManager.GetComponent<PlayerInputManager>().enabled = false;
            Time.timeScale = 1f;
            foreach (GameObject go in gameObjectsToUnactiveAtStart) { go.SetActive(false); }
        }
        else
        {
            panelCanStart.SetActive(true);
            Unactive(panelCanStart);
        }
        
    }

    public async void Unactive(GameObject gameObject)
    {
        Debug.Log("Debut");
        await Task.Delay(3000);
        Debug.Log("SETACTIVEFAUX");
        gameObject.SetActive(false);
    }
}
