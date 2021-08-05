using System.Collections;
using UnityEngine;
using Pathfinding;

public class Kaban : EnemyAI
{
    public float rageSpeed;
    public float stunTime = 2.5f;
    public float knockBackForce = 18f;
    public float RageLength = 2.2f;
    private Vector2 _position;
    private Vector3 _finalPos;
    private bool _isRage;

    private bool _preparing = false;
    private bool _hit = false;
    private bool _hitPlayer = false;

    [SerializeField]
    private CapsuleCollider2D _damageAreaCollider1;
    [SerializeField]
    private KabanArea kabanArea1;
    // Cache
    private Animator _animator;
    private Collider2D targetCollider;
    private Collider2D ogreCollider;

    protected override void Start()
    {
        base.Start();
        
        _animator = GetComponent<Animator>();
        targetCollider = target.GetComponent<Collider2D>();
        ogreCollider = GetComponent<Collider2D>();
    }

    protected override void Update()
    {
        base.Update();
       
        if (_isRage)
            rb.MovePosition(rb.position + _position* rageSpeed * Time.deltaTime);
        if (_hit)
        {
            _damageAreaCollider1.enabled = false;
            _isRage = false;
            _hit = false;
            if (!_hitPlayer)
                StartCoroutine(Stun());
            else
                isAttack = false;
        }
    }

    protected override void Attack()
    {
        if (_isRage) return;
        if (_preparing) return;

        _preparing = true;
        _finalPos = targetCollider.bounds.center;
        _position = (_finalPos - ogreCollider.bounds.center).normalized;
        _damageAreaCollider1.enabled = true;
        kabanArea1.IgnoreWallForASecond();
        StartCoroutine(RageTime());
        _preparing = false;
    }
    protected IEnumerator RageTime()
    {
        if (!_isRage)
        {
            _isRage = true;
            yield return new WaitForSeconds(RageLength);
            _animator.SetTrigger("Crash");
            _animator.SetBool("HitPlayer", false);
            SetHit(false);
        }
    }
    protected override IEnumerator AttackWait()
    {
        isAttack = true;
        StopMoving();
        yield return new WaitForSeconds(attackCoolDown);
        OnAttacked?.Invoke();
        yield return new WaitForSeconds(attackDuration);
        Attack();
        StartMoving();
    }
    public override Vector2 GetDirection()
    {
        return _isRage ? _position : direction;
    }

    public void SetHit(bool hitPlayer)
    {
        _hit = true;
        _hitPlayer = hitPlayer;
        isAttack = true;
        state = NPCstate.Chasing;
        StopAllCoroutines();
    }

    protected override void Die()
    {
        _damageAreaCollider1.gameObject.SetActive(false);
        DestroyAllComponents();
        Destroy(this);
    }
    
    IEnumerator Stun()
    {
        _animator.SetBool("Stunned", true);
        StopMoving();
        yield return new WaitForSeconds(stunTime);
        isAttack = false;
        _animator.SetBool("Stunned", false);
        StartMoving();
        state = NPCstate.Chasing;
    }

    private void DestroyAllComponents()
    {
        Destroy(GetComponent<FloatingNumber>());
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<Seeker>());
        Destroy(GetComponent<NPCAnimationHandler>());
        Destroy(GetComponent<DynamicLayerRenderer>());
        Destroy(GetComponent<AudioSource>());
    }
}
