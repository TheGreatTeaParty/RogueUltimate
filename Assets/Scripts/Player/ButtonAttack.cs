﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void attackPlayer()
    {
        animator.SetTrigger("Attack");

    }

}