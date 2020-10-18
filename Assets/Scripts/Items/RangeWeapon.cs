using UnityEngine;

[CreateAssetMenu(menuName = "Items/RangeWeapon")]

public class RangeWeapon : EquipmentItem
{
    [Space]
    public float attackCD;
    public float knockBack;
    [Space]
    [SerializeField] private int requiredStamina;
    [Space]
    public Transform arrowPrefab;
    [Space]
    public AudioClip prepareSound;
    
    
    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        // Checks if current stamina is less than required. If not - continues attack.
        if (CharacterManager.Instance.Stats.ModifyStamina(requiredStamina) == false)
            return;
            
        Vector3 direction = new Vector3(
            InterfaceManager.Instance.joystickAttack.GetDirection().x, 
            InterfaceManager.Instance.joystickAttack.GetDirection().y);
        Transform arrow = Instantiate(arrowPrefab,
            PlayerOnScene.Instance.playerMovement.transform.position + direction, Quaternion.identity);
        arrow.GetComponent<FlyingObject>().SetData(physicalDamage, magicDamage, direction);
        
    }
    
    
    public override AttackType Echo()
    {
        return AttackType.Range;
    }

    public override float GetAttackCD()
    {
        return attackCD;
    }

}
