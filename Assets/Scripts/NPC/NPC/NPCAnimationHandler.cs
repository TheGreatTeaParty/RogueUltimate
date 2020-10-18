using UnityEngine;

public class NPCAnimationHandler : MonoBehaviour
{
    private AI _npc;
    private EnemyStat _stats;
    private Animator _animator;
    private Vector2 _dir;
    private Vector2 _velocity;
    private float _movementSpeed;
    [SerializeField] private bool isEnemy = true;
    

    // Start is called before the first frame update
    void Start()
    {
        _npc = GetComponent<AI>();

        //Only enemies has attack,die,damage animations, as well as stats
        if (isEnemy)
        {

            _npc.OnAttacked += Attack;

            _stats = GetComponent<EnemyStat>();
            _stats.onDamaged += GetDamage;
            _stats.onDie += Die;
        }

        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(UpdateDirection), 0f, 0.3f); //Call this method every 0.3 seconds
    }

    // Update is called once per frame
    void UpdateDirection()
    {
        _dir = _npc.GetDirection();
        _velocity = _npc.GetVelocity();
        ProcessInputs();

        _animator.SetFloat("Horizontal", _dir.x);
        _animator.SetFloat("Vertical", _dir.y);
        _animator.SetFloat("movementSpeed", _movementSpeed);
    }

    void Attack()
    {
        _animator.SetTrigger("isAttack");
    }

    void GetDamage()
    {
        _animator.SetTrigger("Damaged");
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
