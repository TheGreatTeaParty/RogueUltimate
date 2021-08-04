using UnityEngine;
using System.Collections;


[CreateAssetMenu(menuName = "Abilities/Dash")] 
public class Dash : ActiveAbility
{
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

        if (player.playerAttack.CurrentAttackCD <= 0 && !player.playerMovement.IsStopped())
        {
            player.rb.AddForce(player.playerMovement.GetDirection() * (rb2D.mass * bounce));
            player.playerMovement.StartCoroutine(player.playerMovement.DisablePlayerControll(0.4f));
            var trail = Instantiate(player.playerMovement.trailRenderer, player.GetComponent<Transform>());

            _enemyMask = LayerMask.GetMask("Enemy");
            Collider2D[] enemiesToDamage = Physics2D.
                OverlapCircleAll(player.gameObject.transform.position, 1, _enemyMask);

            EquipmentItem equipmentWeapon = equipment.equipmentSlots[5].Item as EquipmentItem;
            if (equipmentWeapon == null) _weaponDamage = 0;
            else _weaponDamage = equipmentWeapon.GetDamageBonus();
            
            
             // Damage near enamies
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                var vector = enemiesToDamage[i].gameObject.transform.position - player.transform.position;
                enemiesToDamage[i].GetComponent<EnemyStat>().
                    TakeDamage(player.stats.Strength.GetBaseValue() + _weaponDamage, 0f, vector.normalized, 100000);
            }


            Destroy(trail, 1f);
        }

            
    }
        
    
}