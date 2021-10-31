using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : Citizen
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private bool _isChilling = false;

    protected override void Start()
    {
        base.Start();
        if (_isChilling)
            animator.SetTrigger("Chill");
    }
}
