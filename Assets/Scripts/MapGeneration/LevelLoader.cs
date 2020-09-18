using System;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    public Animator crossfadeAnimator;

    private void Start()
    {
        if (!crossfadeAnimator)
            crossfadeAnimator = GetComponentInChildren<Animator>();
    }
    
}