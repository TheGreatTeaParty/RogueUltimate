using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour
{
    private AI NPC;
    private EnemyStat NPCstat;
    private Animator animator;
    private Vector2 dir;
    private Vector2 velocity;
    private float movementSpeed;
    [SerializeField] private bool isEnemy = true;
    

    // Start is called before the first frame update
    void Start()
    {
        NPC = GetComponent<AI>();

        //Only enemies has attack,die,damage animations, as well as stats
        if (isEnemy)
        {

            NPC.onAttacked += Attack;

            NPCstat = GetComponent<EnemyStat>();
            NPCstat.onDamaged += GetDamage;
            NPCstat.onDie += Die;
        }

        animator = GetComponent<Animator>();
        InvokeRepeating("UpdateDirection", 0f, 0.3f); //Call this method every 0.3 seconds
    }

    // Update is called once per frame
    void UpdateDirection()
    {
        dir = NPC.GetDirection();
        velocity = NPC.GetVelocity();
        ProcessInputs();

        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        animator.SetFloat("Speed", movementSpeed);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void GetDamage()
    {
        animator.SetTrigger("Damaged");
    }

    void Die()
    {
        animator.SetTrigger("Dead");
    }

    void ProcessInputs()
    {
        movementSpeed = Mathf.Clamp(velocity.magnitude, 0.0f, 1.0f);
        dir.Normalize();
        velocity.Normalize();
    }
}
