using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.VFX;
using UnityEditor.VFX;

public class CountDown : MonoBehaviour
{
    [SerializeField] float countdownTime = 15f;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] float shakeIntensity = 2f;
    [SerializeField] GameObject softMusic;
    [SerializeField] GameObject hardMusic;
    [SerializeField] Material elec;

    [SerializeField] GameObject softVolume;
    [SerializeField] GameObject hardVolume;
    [SerializeField] VisualEffect eletricity;


    public float currentTime;
    private Vector3 originalPosition;

    void Start()
    {
        currentTime = countdownTime;
        hardMusic.SetActive(false);
        countdownText.gameObject.SetActive(false);
        elec.color = Color.cyan;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (countdownText != null)
            {
                countdownText.text = Mathf.Ceil(currentTime).ToString();
            }
        }
        if (currentTime <= 10)
        {
            countdownText.color = Color.red;
            ShakeText();
        }
        if (currentTime <= 0)
        {
            {
                CountdownEnded();
            }
        }

        void CountdownEnded()
        {
            elec.color = Color.red;
            softMusic.SetActive(false);
            hardMusic.SetActive(true);
            hardVolume.SetActive(true);
            PlayerHealthTo1();
        }

        void ShakeText()
        {
            Vector3 shakeOffset = new Vector3(
            Random.Range(-shakeIntensity, shakeIntensity),
            Random.Range(-shakeIntensity, shakeIntensity),
            0
        );
            countdownText.rectTransform.localPosition = originalPosition + shakeOffset;
        }

        void PlayerHealthTo1()
        {
            foreach(GameObject player in WinManager.instance.playerAliveList)
            {
                while(player.GetComponent<PlayerHealth>().pv != 1)
                {
                    player.GetComponent<PlayerHealth>().takeDamage();
                }
                
            }
            
        }
    }
}
