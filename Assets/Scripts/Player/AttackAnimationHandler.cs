using UnityEngine;

public class AttackAnimationHandler : MonoBehaviour
{

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<PlayerAttack>().onAttacked += AttackAnimation;
    }

    private void AttackAnimation(AttackType type, int set)
    {
        switch (type)
        {
            case AttackType.None: { animator.SetTrigger("Attack"); break; }
            case AttackType.Melee: { animator.SetTrigger("SwordAttack"); animator.SetInteger("Type", set); break; }
            case AttackType.Range: { /*Animation for Range attack*/ break; }
            case AttackType.Magic: { /*Animation for Magic attack*/ break; }

            case AttackType.Windblow:
            {
                // Animation for Windblow attack
                break;
            }
            
        }
        
    }
    
}
