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
    [Space]
    public Transform HitEffect;

    private LayerMask _whatIsEnemy;
    private Vector2 _attackPosition;
    private WeaponRenderer _weaponRenderer;

    public int RequiredMana => requiredMana;
    public int RequiredStamina => requiredStamina;
    public int RequiredHealth => requiredHealth;
    [Space]
    public AudioClip StartAttackSound;
    public AudioClip EndAttackSound;

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

        _weaponRenderer = PlayerOnScene.Instance.GetComponentInChildren<WeaponRenderer>();

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
        
        _whatIsEnemy = LayerMask.GetMask("Enemy","EnvObjects");
        Vector3 direction = player.playerMovement.GetDirection();
        _attackPosition = player.transform.position + direction;


        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(_attackPosition, CharacterManager.Instance.Stats.AttackRange.Value, _whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            //If take damage returns true -> play hit effect:
            if (enemiesToDamage[i].gameObject != player.gameObject)
            {
                enemiesToDamage[i].GetComponent<IDamaged>().TakeDamage(physicalDamage, magicDamage);

                //Create Visual Effect:
                if (HitEffect)
                {
                    Transform Effect = Instantiate(HitEffect, enemiesToDamage[i].GetComponent<Collider2D>().bounds.center, Quaternion.identity);
                    if (_weaponRenderer.PrevIndex == 1)
                        Effect.rotation = Quaternion.Euler(0, 0, 90f);
                    Effect.GetComponent<SpriteRenderer>().sortingOrder = enemiesToDamage[i].GetComponent<SpriteRenderer>().sortingOrder + 1;
                }
                else
                    Debug.LogWarning("No Hit effect assigned to the weapon!");

                Rigidbody2D rigidbody = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                if (rigidbody)
                    rigidbody.AddForce(direction * 100 * playerStat.KnockBack.Value);
            }
        }
        if(enemiesToDamage.Length > 0)
            ScreenShakeController.Instance.StartShake(.09f, .05f + playerStat.PushForce.Value / 1000);
        
    }
    
    public override AttackType Echo()
    {
        return AttackType.Melee;
    }

}
