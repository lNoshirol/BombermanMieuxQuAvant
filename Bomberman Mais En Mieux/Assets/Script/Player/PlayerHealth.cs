using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int pv;
    [SerializeField] TextMeshProUGUI healthUI;
    public int damageMultiplier;
    Collider playerCollider;
    [SerializeField] GameObject playerEnergy;
    Color baseColor;
    Renderer energyRenderer;
    bool invincible = false;

    public GameObject playerBase;
    public GameObject playerHealthShow;
    public GameObject playerBombeShow;

    [SerializeField] Renderer playerHealthHeart;



    public event Action<int> OnDamageTakePv;
    public event Action<GameObject, int> OnDamageTakeGameObject;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DamageRadius" && !invincible && pv!=0)
        {
            takeDamage();
        }
    }

    private void Start()
    {
        if(gameObject.tag == "Player")
        {
            gameObject.tag = $"Player{MuultiplayerManager.instance.playerThatJoined}" ;
        }
        if(gameObject.tag == "Bot")
        {
            gameObject.tag = "Player2";
        }
        playerCollider = gameObject.GetComponent<Collider>();
        energyRenderer = playerEnergy.GetComponent<Renderer>();
        baseColor = energyRenderer.material.color;
        playerBase = GameObject.FindGameObjectWithTag($"{gameObject.tag}Base");
        playerHealthShow = playerBase.transform.GetChild(0).gameObject;
        playerBombeShow = playerBase.transform.GetChild(1).gameObject;

        foreach (Transform child in playerBombeShow.transform)
        {
            child.GetComponent<Renderer>().material.color = Color.gray;
            child.GetChild(0).gameObject.SetActive(false);
        }

        searchNewHealthBar();
    }
    public void takeDamage()
    {
        pv=pv-damageMultiplier;

        OnDamageTakePv?.Invoke(pv);
        OnDamageTakeGameObject?.Invoke(gameObject, pv);

        StartCoroutine(Invicibility());
        StartCoroutine(PlayerBaseLostHp());

        if (pv == 2)
        {
            StartCoroutine(PlayerHasLostHP(0.5f));
        }

        else if (pv == 1)
        {
            StopCoroutine(PlayerHasLostHP(0.5f));
            StartCoroutine(PlayerHasLostHP(0.25f));
        }

        else if (pv == 0) {
            StopCoroutine(PlayerHasLostHP(0.25f));
            energyRenderer.material.color = Color.gray;
            if (gameObject.GetComponent<PlayerMove>())
            {
                gameObject.GetComponent<PlayerMove>().enabled = false;
            }
        }
    }

    private void updateHealthUI()
    {
        healthUI.text = ($"{pv}");
    }

    private IEnumerator Invicibility()
    {
        invincible = true;
        energyRenderer.material.color = Color.gray;
        yield return new WaitForSeconds(0.25f);
        energyRenderer.material.color = baseColor;
        yield return new WaitForSeconds(0.25f);
        energyRenderer.material.color = Color.gray;
        yield return new WaitForSeconds(0.25f);
        energyRenderer.material.color = baseColor;
        energyRenderer.material.color = Color.gray;
        yield return new WaitForSeconds(0.25f);
        energyRenderer.material.color = baseColor;
        invincible = false;
    }

    private IEnumerator PlayerHasLostHP(float time)
    {
        while (true) {
            yield return new WaitForSeconds(time);
            energyRenderer.material.color = Color.gray;
            yield return new WaitForSeconds(time);
            energyRenderer.material.color = baseColor;
        }
    }

    private void searchNewHealthBar()
    {
        foreach(Transform child in playerHealthShow.transform)
        {
            if(child.GetComponent<Renderer>().material.color != Color.gray)
            {
                playerHealthHeart = child.GetComponent<Renderer>();
                return;
            }
        }
    }

    private IEnumerator PlayerBaseLostHp()
    {
        playerHealthHeart.material.color = Color.gray;
        yield return new WaitForSeconds(0.25f);
        playerHealthHeart.material.color = baseColor;
        yield return new WaitForSeconds(0.25f);
        playerHealthHeart.material.color = Color.gray;
        yield return new WaitForSeconds(0.25f);
        playerHealthHeart.material.color = baseColor;
        yield return new WaitForSeconds(0.25f);
        playerHealthHeart.material.color = Color.gray;
        searchNewHealthBar();
    }
}
