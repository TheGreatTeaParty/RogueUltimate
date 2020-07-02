﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Quitting game...");
        Application.Quit();

    }

}
