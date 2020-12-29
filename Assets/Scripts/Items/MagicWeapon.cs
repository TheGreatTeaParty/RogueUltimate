using UnityEngine;

[CreateAssetMenu(menuName = "Items/MagicWeapon")]

public class MagicWeapon : EquipmentItem
{
    [Space]
    public float castSpeedMofifier;
    public float knockBackModifier;
    public float pushForceMofifier;
    [Space] 
    [SerializeField] private int requiredMana;
    [SerializeField] private int requiredStamina;
    [SerializeField] private int requiredHealth;
    [Space]
    public Transform prefab;
    [Space]
    public AudioClip prepareSound;

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
        if (!playerStat.ModifyMana(requiredMana) ||
          !playerStat.ModifyHealth(requiredHealth) ||
          !playerStat.ModifyStamina(requiredStamina))
            return;
        var player = PlayerOnScene.Instance;
       
        Vector3 direction = new Vector3(
            InterfaceManager.Instance.joystickAttack.GetDirection().x, 
            InterfaceManager.Instance.joystickAttack.GetDirection().y);
        
        Transform magic = Instantiate(prefab, 
            player.playerMovement.transform.position + direction, Quaternion.identity);
        magic.GetComponent<FlyingObject>().SetData(physicalDamage, magicDamage, direction,playerStat.KnockBack.Value);
    }
    
    public override AttackType Echo()
    {
        return AttackType.Magic;
    } 
}
