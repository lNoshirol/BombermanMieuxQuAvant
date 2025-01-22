using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] float countdownTime = 15f;
    [SerializeField] TextMeshProUGUI countdownText;

    [SerializeField] float shakeIntensity = 2f;


    private float currentTime;
    private Vector3 originalPosition;

    void Start()
    {
        currentTime = countdownTime;
        countdownText.gameObject.SetActive(false);
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
        else if (currentTime == 0)
        {
            {
                CountdownEnded();
            }
        }

        void CountdownEnded()
        {
            Debug.Log("Countdown Finished!");

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
    }
}
