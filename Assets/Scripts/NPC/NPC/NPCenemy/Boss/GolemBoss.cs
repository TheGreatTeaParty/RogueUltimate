using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemBoss : AI
{
    public float speed = 6f;
    public float meleRange = 0.5f;
    public float rockRange = 7f;
    [Space]
    public int StageTwoHealth = 180;
    public float StageCharacteristicMult = 1.4f;

    [Space]
    public float KnockBack = 1f;
    public Transform rockPrefab;

    private BossStage stage;
    private EnemyStat golemStat;

    private bool StageChanged = false;


    public enum BossStage
    {
        first = 0,
        second,
    };
    
    public override void Start()
    {
        base.Start();
        golemStat = GetComponent<EnemyStat>();
        stage = BossStage.first;

        target = GameObject.FindGameObjectWithTag("Player");
        BossFightPortal.Instance.HealthBar(true);
        state = NPCstate.chasing;
    }

    public override void Update()
    {
        base.Update();

        if (golemStat.currentHealth <= StageTwoHealth && !StageChanged)
        {
            SwitchStage();
        }

        //Should be changed in the inherited EnemyStat class
        BossFightPortal.Instance.SetBossHealth(golemStat.currentHealth);

        if(golemStat.currentHealth <=0)
        {
            BossFightPortal.Instance.HealthBar(false);
            BossFightPortal.Instance.TurnThePortal();
        }
    }

    public override void FixedUpdate()
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

    
    private void SwitchStage()
    {
        stage = BossStage.second;
        speed *= StageCharacteristicMult;
        attackCoolDown -= .2f;
        GetComponent<SpriteRenderer>().color = Color.red;
        StageChanged = true;
    }

    public override void Attack()
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
