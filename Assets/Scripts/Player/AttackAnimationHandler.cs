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
            case AttackType.None: { animator.SetTrigger("Attack"); break; }
            case AttackType.Melee: { animator.SetTrigger("WeaponAttack"); break; }
            case AttackType.Range: { animator.SetTrigger("WeaponAttack"); break; }
            case AttackType.Magic: { animator.SetTrigger("WeaponAttack"); break; }

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
            case AttackType.None: { animator.SetTrigger("EndAttack"); break; }
            case AttackType.Melee: { animator.SetTrigger("EndWeaponAttack"); break; }
            case AttackType.Range: { animator.SetTrigger("EndWeaponAttack"); break; }
            case AttackType.Magic: { animator.SetTrigger("EndWeaponAttack"); break; }
        }
    }
}
