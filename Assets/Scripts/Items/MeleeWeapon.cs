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
    public float attackSpeedMofifier;
    public float attackRangeMofifier;
    public float knockBackModifier;
    public float pushForceMofifier;
    [Space]
    [SerializeField] private int requiredStamina;
    [SerializeField] private int requiredMana;
    [SerializeField] private int requiredHealth;

    private LayerMask _whatIsEnemy;
    private Vector2 _attackPosition;

    public int RequiredMana => requiredMana;
    public int RequiredStamina => requiredStamina;
    public int RequiredHealth => requiredHealth;

    public override void Equip(PlayerStat stats)
    {
        base.Equip(stats);

        if (attackSpeedMofifier != 0)
            stats.AttackSpeed.AddModifier(new StatModifier(attackSpeedMofifier, StatModifierType.Flat, this));
        if (attackRangeMofifier != 0)
            stats.AttackRange.AddModifier(new StatModifier(attackRangeMofifier, StatModifierType.Flat, this));
        if (knockBackModifier != 0)
            stats.KnockBack.AddModifier(new StatModifier(knockBackModifier, StatModifierType.Flat, this));
        if (pushForceMofifier != 0)
            stats.PushForce.AddModifier(new StatModifier(pushForceMofifier, StatModifierType.Flat, this));

    }

    public override void Unequip(PlayerStat stats)
    {
        base.Unequip(stats);

        stats.AttackSpeed.RemoveAllModifiersFromSource(this);
        stats.AttackRange.RemoveAllModifiersFromSource(this);
        stats.KnockBack.RemoveAllModifiersFromSource(this);
        stats.PushForce.RemoveAllModifiersFromSource(this);
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        PlayerStat playerStat = CharacterManager.Instance.Stats;
        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        var player = PlayerOnScene.Instance;
        
        _whatIsEnemy = LayerMask.GetMask("Enemy");
        Vector3 direction = player.playerMovement.GetDirection();
        _attackPosition = player.transform.position + direction;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, CharacterManager.Instance.Stats.AttackRange.Value, _whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyStat>().TakeDamage(physicalDamage, magicDamage);
            enemiesToDamage[i].GetComponent<Rigidbody2D>().AddForce(direction * 100 * playerStat.KnockBack.Value);
        }
        if(enemiesToDamage.Length > 0)
            ScreenShakeController.Instance.StartShake(.09f, .05f + playerStat.PushForce.Value / 1000);
        
    }
    
    public override AttackType Echo()
    {
        return AttackType.Melee;
    }

}
