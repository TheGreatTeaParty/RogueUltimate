using UnityEngine;

[CreateAssetMenu(menuName = "Items/MagicWeapon")]

public class MagicWeapon : EquipmentItem
{
    public enum MagicType
    {
        Nature = 0,
        Fire,
        Death,
    }
    [Space]
    public MagicType WeaponType;
    [Space]
    public float castSpeedMofifier;
    public float knockBackModifier;
    public float pushForceMofifier;
    [Space] 
    [SerializeField] private int requiredMana;
    [SerializeField] private int requiredStamina;
    [SerializeField] private int requiredHealth;

    public int RequiredMana => requiredMana;
    public int RequiredStamina => requiredStamina;
    public int RequiredHealth => requiredHealth;

    [Space]
    public Transform prefab;
    [Space]
    public AudioClip prepareSound;
    public AudioClip ReleaseSound;

    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Equip(PlayerStat stats)
    {
        base.Equip(stats);

        if (castSpeedMofifier != 0)
            stats.CastSpeed.AddModifier(new StatModifier(castSpeedMofifier, StatModifierType.Flat, this));
        if (knockBackModifier != 0)
            stats.KnockBack.AddModifier(new StatModifier(knockBackModifier, StatModifierType.Flat, this));
        if (pushForceMofifier != 0)
            stats.PushForce.AddModifier(new StatModifier(pushForceMofifier, StatModifierType.Flat, this));

    }

    public override void Unequip(PlayerStat stats)
    {
        base.Unequip(stats);

        stats.CastSpeed.RemoveAllModifiersFromSource(this);
        stats.KnockBack.RemoveAllModifiersFromSource(this);
        stats.PushForce.RemoveAllModifiersFromSource(this);
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        PlayerStat playerStat = CharacterManager.Instance.Stats;

        Vector3 direction = playerStat.playerMovement.GetDirection().normalized;

        if (direction.magnitude == 0) { return; }

        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        var player = PlayerOnScene.Instance;
       
        Transform magic = Instantiate(prefab, 
            player.playerMovement.transform.position + direction, Quaternion.identity);
        var crit = playerStat.GetMagicalCritDamage();
        if(_effect)
            magic.GetComponent<FlyingObject>().SetData(magicDamage, crit.Item1, direction, crit.Item2, playerStat.KnockBack.Value,Instantiate(_effect));
        else
            magic.GetComponent<FlyingObject>().SetData(magicDamage, crit.Item1, direction, crit.Item2, playerStat.KnockBack.Value);
    }
    
    public override AttackType Echo()
    {
        return AttackType.Magic;
    } 
}
