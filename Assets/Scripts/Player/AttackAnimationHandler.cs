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

    private void AttackAnimation(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.nothing: { animator.SetTrigger("Attack"); break; }
            case WeaponType.mele: { /*Animation for mele attack*/ break; }
            case WeaponType.range: { /*Animation for range attack*/ break; }
            case WeaponType.magic: { /*Animation for magic attack*/ break; }

        }
    }
}
