using UnityEngine;
using System.Collections;


public enum BossStage
{
    First = 0,
    Second = 1
}


public class GolemBoss : EnemyAI
{
    public GameObject Lamp;
    [Space]
    public float speed = 4f;
    public float meleRange = 1.4f;
    public float rockRange = 12f;
    [Space]
    public float restTime = 2f;
    public float RangeAttackCoolDown = 4f;
    [Space]
    public float stageTwoHealth = 0.7f;
    public float stageCharacteristicMult = 2f;
    [Space]
    public float knockBack = 1f;
    public Transform rockPrefab;

    private BossStage _stage;
    private bool _stageChanged = false;
    private LayerMask _whatIsEnemy;
    private int current_attack_index = 1;
    private float range_attack_time_left;
    private float rest_time_left = 0;
    private bool _isJump = false;
    public bool IsRange = false;
    Vector3 _dir;
    Vector2 endPoint;
    // Cache
    private SpriteRenderer _spriteRenderer;

    public delegate void OnAttack(int attack_index);
    public OnAttack onAttack;

    public delegate void OnTrigger();
    public OnTrigger onRangeAttack;
    public OnTrigger onStageChanged;
    public OnTrigger onLand;


    protected override void Start()
    {
        base.Start();
        _stage = BossStage.Second;

        target = GameObject.FindGameObjectWithTag("Player");
        state = NPCstate.Chasing;
        range_attack_time_left = RangeAttackCoolDown;

        // Cache
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _whatIsEnemy = LayerMask.GetMask("Player");
        BossFightPortal.Instance.HealthBar(true);
    }

    protected override void Update()
    {
        base.Update();

        if (stats.CurrentHealth <= stageTwoHealth * stats.MaxHealth && !_stageChanged)
            SwitchStage();

        if (range_attack_time_left > 0)
            range_attack_time_left -= Time.deltaTime;
        if (rest_time_left > 0)
            rest_time_left -= Time.deltaTime;

        if (_isJump)
        {
            Fly();
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
                    Attack();
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
                    Attack();
                    break;
                
                case NPCstate.Hanging:
                    break;
            }
    }
    
    private void SwitchStage()
    {
        StopMoving();
        _stage = BossStage.Second;
        movementSpeed *= stageCharacteristicMult;
        _stageChanged = true;
        onStageChanged?.Invoke();
    }

    public void EndTransformation()
    {
        StartMoving();
        state = NPCstate.Chasing;
        ScreenShakeController.Instance.StartShake(1f, 0.7f);
    }

    protected override void Attack()
    {
        if (Vector2.Distance(transform.position, target.transform.position) < rockRange &&
            Vector2.Distance(transform.position, target.transform.position) <= meleRange)
        {
            if (!isAttack && rest_time_left <= 0)
                MeleeAttack();
        }

        else if (Vector2.Distance(transform.position, target.transform.position) > meleRange + 2f &&
                Vector2.Distance(transform.position, target.transform.position) < rockRange  && range_attack_time_left <= 0)
        {
            if (!isAttack && rest_time_left <= 0)
                RangeAttack();
        }
        else
        {
            if (!isAttack)
            {
                state = NPCstate.Chasing;
                StartMoving();
            }
        }
    }

    private void MeleeAttack()
    {
        isAttack = true;
        StopMoving();
        endPoint = target.transform.position;
        onAttack?.Invoke(current_attack_index);
        current_attack_index++;
    }

    public void AttackLogic()
    {
        CreateDamageColider();
        if (current_attack_index == 3)
        {
            rest_time_left = restTime;
            current_attack_index = 1;
            state = NPCstate.Chasing;
        }
        ScreenShakeController.Instance.StartShake(0.8f, 0.5f);
    }

    public void AttackStage2()
    {
        CreateDamageColider();
        if (current_attack_index == 4)
        {
            rest_time_left = restTime;
            current_attack_index = 1;
            state = NPCstate.Chasing;
        }
        ScreenShakeController.Instance.StartShake(0.8f, 0.5f);
    }

    private void CreateDamageColider() {

        var direction = ((Vector3)endPoint - transform.position).normalized;
        var _attackPosition = transform.position + direction / 1.2f;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRadius, _whatIsEnemy);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != gameObject)
                enemiesToDamage[i].GetComponent<IDamaged>().
                    TakeDamage(stats.PhysicalDamage.Value, stats.MagicDamage.Value);
        }
    }

    private void RangeAttack()
    {
        isAttack = true;
        StopMoving();
        onRangeAttack?.Invoke();
        if (_stage == BossStage.First)
            IsRange = true;
    }


    public void CreateStone()
    {
        var position = transform.position;
        var targetPosition = target.transform.position;
        
        var rock = 
            Instantiate(rockPrefab, position + (targetPosition - position).normalized*2, Quaternion.identity);

        var data = rock.GetComponent<FlyingObject>();
        data.SetData(stats.PhysicalDamage.Value, stats.MagicDamage.Value, (targetPosition - position).normalized,false);

        isAttack = false;
        IsRange = false;
        state = NPCstate.Chasing;
        range_attack_time_left = RangeAttackCoolDown;
    }

    public void Jump()
    {
        range_attack_time_left = RangeAttackCoolDown;
        _isJump = true;
        isAttack = true;
        endPoint = target.transform.position;
        _dir = (target.transform.position - transform.position).normalized;
        state = NPCstate.IDLE;

        rb.AddForce(new Vector2(0, 50f));
        rb.gravityScale = 2;
    }

    private void Land()
    {
        StopMoving();
        _isJump = false;
        onLand?.Invoke();
    }

    public void EndAttack()
    {
        isAttack = false;
    }

    public void OnLandDamaged()
    {
        CreateDamageColider();
        ScreenShakeController.Instance.StartShake(0.9f, 0.7f);
    }

    void Fly()
    {
        if (Vector2.Distance(transform.position, endPoint) < 0.8f)
        {
            state = NPCstate.Attacking;
            Land();
            rb.gravityScale = 0;
        }
        else
            rb.MovePosition(transform.position + _dir * 10 * Time.deltaTime);
    }

    public void TurnONLamp()
    {
        Lamp.SetActive(true);
    }

    public void TurnOFFLamp()
    {
        Lamp.SetActive(false);
    }
}
