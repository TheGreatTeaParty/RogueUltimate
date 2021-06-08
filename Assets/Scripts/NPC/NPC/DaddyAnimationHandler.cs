using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaddyAnimationHandler : MonoBehaviour
{
    private GolemBoss _npc;
    private EnemyStat _stats;
    private Animator _animator;
    private Vector2 _dir;
    private Vector2 _velocity;
    private float _movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        _npc = GetComponent<GolemBoss>();

        //Only enemies has attack,die,damage animations, as well as stats
   
        _npc.onAttack += Attack;
        _npc.onRangeAttack += RangeAttack;
        _npc.onStageChanged += ChangeToSecondStage;
        _npc.onLand += Land;

        _stats = GetComponent<EnemyStat>();
        _stats.onDamaged += GetDamage;
        _stats.onDie += Die;

        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(UpdateDirection), 0f, 0.3f); //Call this method every 0.3 seconds
    }

    // Update is called once per frame
    void UpdateDirection()
    {
        _dir = _npc.GetDirection();
        _velocity = _npc.GetVelocity();
        ProcessInputs();

        if (!_npc.IsStopped || _npc.IsRange)
        {
            _animator.SetFloat("Horizontal",_dir.x);
        }

        _animator.SetFloat("Speed", _movementSpeed);
    }

    void Attack(int index)
    {
        string name = "Attack" + index.ToString();
        _animator.SetTrigger(name);
    }

    void RangeAttack()
    {
        _animator.SetTrigger("AttackRange");
    }

    void GetDamage()
    {
      //  _animator.SetTrigger("Damaged");
    }
    void Land()
    {
        _animator.SetTrigger("Land");
    }
    void ChangeToSecondStage()
    {
        _animator.SetTrigger("ChangeStage");
    }
    void Die()
    {
        _animator.SetTrigger("Dead");
    }


    void ProcessInputs()
    {
        _movementSpeed = Mathf.Clamp(_velocity.magnitude, 0.0f, 1.0f);
        _dir.Normalize();
        _velocity.Normalize();
    }

}
