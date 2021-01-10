using UnityEngine;

public class AttackAnimationHandler : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<PlayerAttack>().onAttacked += AttackAnimation;
        GetComponent<PlayerAttack>().EndAttack += EndPlayerAttackAnim;
    }

    private void AttackAnimation(AttackType type)
    {
        switch (type)
        {
            case AttackType.None:  { animator.SetBool("Attack", true); break; }
            case AttackType.Melee: { animator.SetBool("WeaponAttack", true); break; }
            case AttackType.Range: { animator.SetBool("WeaponAttack", true); break; }
            case AttackType.Magic: { animator.SetBool("WeaponAttack", true); break; }

            /*case AttackType.Windblow:
            {
                // Animation for Windblow attack
                break;
            }*/
            
        }
        
    }

    private void EndPlayerAttackAnim(AttackType type)
    {
        switch (type)
        {
            case AttackType.None:  { animator.SetBool("Attack", false); break; }
            case AttackType.Melee: { animator.SetBool("WeaponAttack", false); break; }
            case AttackType.Range: { animator.SetBool("WeaponAttack",false); break; }
            case AttackType.Magic: { animator.SetBool("WeaponAttack", false); break; }
        }
    }
}
