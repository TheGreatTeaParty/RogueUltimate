using System.Collections;
using UnityEngine;

public class Kaban : EnemyAI
{
    public float rageSpeed;
    public float stunTime = 2.5f;

    private Vector2 _position;
    private Vector3 _finalPos;
    private bool _isRage;

    private bool _preparing = false;
    private bool _hit = false;
    private bool _hitPlayer = false;

    // Cache
    private CapsuleCollider2D _damageAreaCollider;
    private Animator _animator;


    protected override void Start()
    {
        base.Start();
        
        _damageAreaCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
       
        if (_isRage)
            rb.MovePosition(rb.position + _position* rageSpeed * Time.deltaTime);

        if (_hit)
        {
            _damageAreaCollider.enabled = false;
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
        _finalPos = target.transform.position;
        _position = (_finalPos - transform.position).normalized;
        _damageAreaCollider.enabled = true;
        _isRage = true;
        _preparing = false;
    }

    public override Vector2 GetDirection()
    {
        return _isRage ? _position : direction;
    }

    public void SetHit(bool hitPlayer)
    {
        _hit = true;
        _hitPlayer = hitPlayer;
    }

    protected override void Die()
    {
        _damageAreaCollider.gameObject.SetActive(false);
        Destroy(this);
    }
    
    IEnumerator Stun()
    {
        _animator.SetBool("Stunned", true);
        yield return new WaitForSeconds(stunTime);
        isAttack = false;
        _animator.SetBool("Stunned", false);
    }
    
}
