using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Abilities/Dash")] 
public class Dash : ActiveAbility
{
    public Transform DamageArea;
    public float bounce;
    private float _weaponDamage;

    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;
        var equipment = CharacterManager.Instance.Equipment;
        var rb2D = player.GetComponent<Rigidbody2D>();


        player.rb.AddForce(player.playerMovement.GetDirection().normalized * (rb2D.mass * bounce));
        player.playerMovement.StartCoroutine(player.playerMovement.DisablePlayerControll(0.4f));
        var trail = Instantiate(player.playerMovement.trailRenderer, player.GetComponent<Transform>());

        EquipmentItem equipmentWeapon = equipment.equipmentSlots[5].Item as EquipmentItem;
        if (equipmentWeapon == null) _weaponDamage = 0;
        else _weaponDamage = equipmentWeapon.GetDamageBonus();

        var _damageArea = Instantiate(DamageArea);
        var _area = _damageArea.GetComponent<DamageArea>();
        _area.target = player.gameObject;
        _area.damage = player.stats.Strength.GetBaseValue() + (int)(0.75 *_weaponDamage);

        Destroy(trail, 1f);
    }
        
    
}