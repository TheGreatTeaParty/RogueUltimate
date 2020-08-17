using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationHandler : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<PlayerAttack>().onAttacked += AttackAnimation;
    }

    private void AttackAnimation(WeaponType type,int set)
    {
        switch (type)
        {
            case WeaponType.None: { animator.SetTrigger("Attack"); break; }
            case WeaponType.Melee: { animator.SetTrigger("SwordAttack"); animator.SetInteger("Type", set); break; }
            case WeaponType.Range: { /*Animation for Range attack*/ break; }
            case WeaponType.Magic: { /*Animation for Magic attack*/ break; }

        }
    }
}
