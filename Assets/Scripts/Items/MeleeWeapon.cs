using UnityEngine;
using Random = UnityEngine.Random;


public enum AttackType 
{ 
    Melee = 0, 
    Range = 1, 
    Magic = 2, 
    None = 3,
    
    Windblow = 4,
}


[CreateAssetMenu(menuName = "Items/MeleeWeapon")]
public class MeleeWeapon : EquipmentItem
{
    [Space]
    public float attackCoolDown;
    public float attackRange;
    public float knockBack;
    public float pushForce;
    [Space]
    [SerializeField] private int requiredStamina;
    [SerializeField] private float attackDuration = 0.5f;

    private LayerMask _whatIsEnemy;
    private Vector2 _attackPosition;
    

    public override void Attack(float physicalDamage, float magicDamage)
    {
        var player = PlayerOnScene.Instance;
        
        // Checks if current stamina is less than required. If not - continues attack.
        if (CharacterManager.Instance.Stats.ModifyStamina(requiredStamina) == false)
            return;
        
        _whatIsEnemy = LayerMask.GetMask("Enemy");
        Vector3 direction = player.playerMovement.GetDirection();
        _attackPosition = player.transform.position + direction;

        if (player.playerAttack.GetAttackCD() <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, attackRange, _whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            { 
                enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(physicalDamage, magicDamage);
            }

            //Send mesage to isAttack animation handler that we use Melee Weapon
            player.playerAttack.onAttacked?.Invoke(AttackType.Melee, Random.Range(0, 2));
            player.playerAttack.SetRange(attackRange);
        }
        
    }
    
    public override AttackType Echo()
    {
        return AttackType.Melee;
    }

    public override float GetAttackCD()
    {
        return attackCoolDown;
    }
    
}
