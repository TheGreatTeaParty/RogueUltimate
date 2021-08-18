using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D _damageArea;
    [SerializeField] private Effect _fireEffect;
    private float _baseDamagValue = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var stats = PlayerOnScene.Instance.stats;

        if (collision.CompareTag("Enemy"))
        {
            IDamaged damaged = collision.GetComponent<IDamaged>();
            CharacterStat character = collision.GetComponent<CharacterStat>();

            if (damaged != null)
            {
                // Calculating dmg formula (5 * Int. + Fire effect I)
                damaged.TakeDamage(0, stats.Intelligence.GetBaseValue());
                character.EffectController.AddEffect(Instantiate(_fireEffect), character);
            }

        }
    }
}
