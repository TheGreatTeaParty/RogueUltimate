using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/MagicWeapon")]

public class MagicWeapon : EquipmentItem
{
    [Space]
    public float castTime;
    public float knockBack;
    [Space] 
    [SerializeField] private int requiredMana;
    [Space]
    public Transform prefab;
    [Space]
    public AudioClip prepareSound;
    

    private void Awake()
    {
        equipmentType = EquipmentType.Weapon;
    }

    public override void Attack(float physicalDamage, float magicDamage)
    {
        var player = PlayerOnScene.Instance;
        
        // Checks if current stamina is less than required. If not - continues attack.
        if (PlayerStat.Instance.ModifyMana(requiredMana) == false)
            return;
        
        Vector3 direction = new Vector3(
            InterfaceManager.Instance.joystickAttack.GetDirection().x, 
            InterfaceManager.Instance.joystickAttack.GetDirection().y);
        
        Transform magic = Instantiate(prefab, 
            player.playerMovement.transform.position + direction, Quaternion.identity);
        magic.GetComponent<FlyingObject>().SetData(physicalDamage, magicDamage, direction);
        //Send mesage to Attack animation handler that we use Melee Weapon
        player.playerAttack.onAttacked?.Invoke(AttackType.Magic,0);
    }
    
    public override AttackType Echo()
    {
        return AttackType.Magic;
    }

    public override float GetAttackCD()
    {
        return castTime;
    }
    
}
