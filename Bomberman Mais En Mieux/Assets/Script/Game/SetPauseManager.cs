using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetPauseManager : MonoBehaviour
{
    public static SetPauseManager instance;

    public bool isPause = false;
    [SerializeField] private GameObject pausePanel;

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

    public void SetPauseTrigger()
    {
        if (!isPause)
        {
            Time.timeScale = 0f;
            PauseAllAudio();
            pausePanel.SetActive(true);
            isPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            UnPauseAudio();
            pausePanel.SetActive(false);
            isPause = false;
        }
    }
    public void PauseAllAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    public void UnPauseAudio()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource != null && audioSource.time > 0)
            {
                audioSource.UnPause();
            }
        }
    }
}