using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBoss : MonoBehaviour
{
    public float speed = 6f;
    public float meleRange = 0.5f;
    public float rockRange = 7f;
    public float detectionRange = 7f;
    public float followRange = 10f;

    [Space]
    public int StageTwoHealth = 180;
    public float StageCharacteristicMult = 1.4f;

    [Space]
    public float attackCoolDown = 1.2f;
    public float KnockBack = 1f;
    public Transform rockPrefab;

    private float startAttackCoolDown;
    private NPCstate state;
    private BossStage stage;
    private EnemyStat golemStat;

    private NPCPathfindingMovement NPCmovement;
    private GameObject target;
    private bool StageChanged = false;


    public enum NPCstate
    {

        chasing = 0,
        attacking,
    }

    public enum BossStage
    {
        first = 0,
        second,
    };
    
    public virtual void Start()
    {
        NPCmovement = GetComponent<NPCPathfindingMovement>();
        NPCmovement.SetSpeed(speed);
        golemStat = GetComponent<EnemyStat>();
        stage = BossStage.first;

        target = GameObject.FindGameObjectWithTag("Player");
        startAttackCoolDown = attackCoolDown;
        BossFightPortal.Instance.HealthBar(true);
    }

    public virtual void Update()
    {
        if (startAttackCoolDown > 0)
            startAttackCoolDown -= Time.deltaTime;

        if (golemStat.CurrentHealth <= StageTwoHealth && !StageChanged)
        {
            SwitchStage();
        }
        //Should be changed in the inherited EnemyStat class
        BossFightPortal.Instance.SetBossHealth(golemStat.CurrentHealth);

        if(golemStat.CurrentHealth <=0)
        {
            BossFightPortal.Instance.HealthBar(false);
            BossFightPortal.Instance.TurnThePortal();
        }
    }

    void FixedUpdate()
    {
        if (stage == BossStage.first)
        {
            switch (state)
            {
                case NPCstate.chasing:
                    {
                        StateChasing();
                        break;
                    }

                case NPCstate.attacking:
                    {
                        StateAttack();
                        break;
                    }
            }
        }
        else
        {
            switch (state)
            {
                case NPCstate.chasing:
                    {
                        StateChasing();
                        break;
                    }

                case NPCstate.attacking:
                    {
                        StateAttack();
                        break;
                    }
            }
        }
    }

    public void StateAttack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) > rockRange)
            state = NPCstate.chasing;

        if (startAttackCoolDown <= 0)
        {
            Attack();
            startAttackCoolDown = attackCoolDown;
        }
    }


    public void StateChasing()
    {
        if (NPCmovement != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) <= rockRange)
            {
                NPCmovement.StopMoving();
                state = NPCstate.attacking;
            }
            else
            {
                NPCmovement.MoveTo(target.transform.position);
            }

        }
    }

    private void SwitchStage()
    {
        stage = BossStage.second;
        speed *= StageCharacteristicMult;
        attackCoolDown -= .2f;
        StageChanged = true;
    }

    public void Attack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < rockRange &&
            Vector2.Distance(transform.position, target.transform.position) < meleRange)
            MeleAttack();

        else
        {
            RangeAttack();
        }
    }

    private void MeleAttack()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 attackPosition = transform.position + direction;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition, meleRange, LayerMask.GetMask("Player"));
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
     
            enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(golemStat.physicalDamage.GetValue(), golemStat.magicDamage.GetValue());
        }
    }
    private void RangeAttack()
    {
        Transform rock = Instantiate(rockPrefab, transform.position + (target.transform.position - transform.position).normalized*2, Quaternion.identity);

        rock.GetComponent<FlyingObject>().SetData(golemStat.physicalDamage.GetValue(), golemStat.magicDamage.GetValue(), (target.transform.position - transform.position).normalized);
    }
}
