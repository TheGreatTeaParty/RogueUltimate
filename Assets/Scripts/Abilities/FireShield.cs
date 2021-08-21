using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] private Effect _fireEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // var stats = PlayerOnScene.Instance.stats;

        if (collision.CompareTag("Enemy"))
        {
            IDamaged damaged = collision.GetComponent<IDamaged>();
            CharacterStat character = collision.GetComponent<CharacterStat>();

            if (damaged != null)
            {
                // damaged.TakeDamage(0, 1 * stats.Intelligence.GetBaseValue());
                character.EffectController.AddEffect(Instantiate(_fireEffect), character);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            CharacterStat character = collision.GetComponent<CharacterStat>();
            character.EffectController.RemoveEffectsOfType(EffectType.Fire);

        }
    }
}
