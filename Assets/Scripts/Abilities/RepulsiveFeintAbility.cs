﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Repulsive feint")]
public class RepulsiveFeintAbility : ActiveAbility
{
    [SerializeField]
    private Effect _effect;

    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;


        _enemyMask = LayerMask.GetMask("Enemy");
        Collider2D[] enemiesToDamage = Physics2D.
            OverlapCircleAll(player.gameObject.transform.position, 2, _enemyMask);

        PlayerOnScene.Instance.stats.EffectController.
            AddEffect(Instantiate(_effect), PlayerOnScene.Instance.stats);

        // Damage near enamies
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            var vector = enemiesToDamage[i].gameObject.transform.position - player.transform.position;
            enemiesToDamage[i].GetComponent<EnemyStat>().
                TakeDamage(0f, 0f, player.playerMovement.GetDirection(), 750000);
        }
    }
}
