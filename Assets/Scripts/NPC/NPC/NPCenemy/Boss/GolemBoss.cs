using UnityEngine;


public enum BossStage
{
    First = 0,
    Second = 1
}


public class GolemBoss : EnemyAI
{
    public float speed = 6f;
    public float meleeRange = 0.5f;
    public float rockRange = 7f;
    [Space]
    public int stageTwoHealth = 180;
    public float stageCharacteristicMult = 1.4f;
    [Space]
    public float knockBack = 1f;
    public Transform rockPrefab;

    private BossStage _stage;
    private bool _stageChanged = false;

    // Cache
    private SpriteRenderer _spriteRenderer;
    
    
    protected override void Start()
    {
        base.Start();
        _stage = BossStage.First;

        target = GameObject.FindGameObjectWithTag("Player");
        BossFightPortal.Instance.HealthBar(true);
        state = NPCstate.Chasing;
        
        // Cache
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (stats.CurrentHealth <= stageTwoHealth && !_stageChanged)
            SwitchStage();
        
        //Should be changed in the inherited EnemyStat class
        BossFightPortal.Instance.SetBossHealth(stats.CurrentHealth);

        if (stats.CurrentHealth <= 0)
        {
            BossFightPortal.Instance.HealthBar(false);
            BossFightPortal.Instance.TurnThePortal();
        }
    }

    protected override void FixedUpdate()
    {
        if (_stage == BossStage.First)
            switch (state)
            {
                case NPCstate.Chasing:
                    StateChasing();
                    break;
                
                case NPCstate.Attacking:
                    StateAttack();
                    break;
                
                case NPCstate.Hanging:
                    break;
            }
        else
            switch (state)
            {
                case NPCstate.Chasing:
                    StateChasing();
                    break;
                
                case NPCstate.Attacking:
                    StateAttack();
                    break;
                
                case NPCstate.Hanging:
                    break;
            }
    }
    
    private void SwitchStage()
    {
        _stage = BossStage.Second;
        speed *= stageCharacteristicMult;
        attackCoolDown -= .2f;
        _spriteRenderer.color = Color.red;
        _stageChanged = true;
    }

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < rockRange &&
            Vector2.Distance(transform.position, target.transform.position) < meleeRange)
            MeleeAttack();
        
        else
            RangeAttack();
    }

    private void MeleeAttack()
    {
        var direction = (target.transform.position - transform.position).normalized;
        var attackPosition = transform.position + direction;
        var whatIsEnemy = LayerMask.GetMask("Player");
        
        Collider2D[] enemiesToDamage = 
            Physics2D.OverlapCircleAll(attackPosition, meleeRange, whatIsEnemy);
        
        for (int i = 0; i < enemiesToDamage.Length; i++)
            enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);
    }
    private void RangeAttack()
    {
        var position = transform.position;
        var targetPosition = target.transform.position;
        
        var rock = 
            Instantiate(rockPrefab, position + (targetPosition - position).normalized*2, Quaternion.identity);

      //  rock.GetComponent<FlyingObject>().
          //  SetData(stats.PhysicalDamage.Value, stats.MagicDamage.Value, (targetPosition - position).normalized);
    }
    
}
