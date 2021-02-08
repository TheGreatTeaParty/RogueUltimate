using UnityEngine;

[CreateAssetMenu(menuName = "Items/RangeWeapon")]

public class RangeWeapon : EquipmentItem
{
    [Space]
    public float attackSpeedMofifier;
    public float knockBackModifier;
    public float pushForceMofifier;

    [Space]
    [SerializeField] private int requiredStamina;
    [SerializeField] private int requiredMana;
    [SerializeField] private int requiredHealth;

    [Space]
    public Transform arrowPrefab;
    [Space]
    public AudioClip prepareSound;
    public AudioClip ReleaseSound;

    public int RequiredMana => requiredMana;
    public int RequiredStamina => requiredStamina;
    public int RequiredHealth => requiredHealth;


    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }


    public override void Equip(PlayerStat stats)
    {
        base.Equip(stats);

        if (attackSpeedMofifier != 0)
            stats.AttackSpeed.AddModifier(new StatModifier(attackSpeedMofifier, StatModifierType.Flat, this));
        if (knockBackModifier != 0)
            stats.KnockBack.AddModifier(new StatModifier(knockBackModifier, StatModifierType.Flat, this));
        if (pushForceMofifier != 0)
            stats.PushForce.AddModifier(new StatModifier(pushForceMofifier, StatModifierType.Flat, this));

    }

    public override void Unequip(PlayerStat stats)
    {
        base.Unequip(stats);

        stats.AttackSpeed.RemoveAllModifiersFromSource(this);
        stats.KnockBack.RemoveAllModifiersFromSource(this);
        stats.PushForce.RemoveAllModifiersFromSource(this);
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        JoystickAttack _directionJoystick = InterfaceManager.Instance.joystickAttack;
        Vector3 direction = new Vector3(
            _directionJoystick.GetDirection().x,
            _directionJoystick.GetDirection().y);

        if(direction.magnitude == 0) { return; }

        PlayerStat playerStat = CharacterManager.Instance.Stats;
        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        Transform arrow = Instantiate(arrowPrefab,
            PlayerOnScene.Instance.playerMovement.transform.position + direction, Quaternion.identity);
        var crit = playerStat.GetPhysicalCritDamage();
        arrow.GetComponent<FlyingObject>().SetData(crit.Item1, magicDamage, direction,crit.Item2,playerStat.KnockBack.Value);
    }
    
    
    public override AttackType Echo()
    {
        return AttackType.Range;
    }
}
