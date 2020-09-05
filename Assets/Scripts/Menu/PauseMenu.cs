﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Update()
    {
        
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        animator.SetFloat("Kind of animation", 0f);

    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        animator.SetFloat("Kind of animation", 1f);

    }

    public void SettingsButton()
    {
        Debug.Log("There will be settings");

    }

    public void ExitButton()
    {
        SaveManager.SavePlayer();
        Time.timeScale = 1f;
        Debug.Log("Quitting game...");

        Destroy(PlayerStat.Instance.gameObject);
        Destroy(InterfaceOnScene.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);

        SceneManager.LoadScene("Menu");
    }

}

