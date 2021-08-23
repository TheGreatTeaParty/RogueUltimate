using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Poisoned stab")]
public class PoisonedStabAbility : ActiveAbility
{
    [SerializeField]
    private Effect _effect;
    [SerializeField]
    private Effect _poisonEffect;

    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;

        player.stats.EffectController.
            AddEffect(Instantiate(_effect), PlayerOnScene.Instance.stats);

        _enemyMask = LayerMask.GetMask("Enemy");
        Collider2D enemyToDamage = Physics2D.
            OverlapCircle(player.gameObject.transform.position, 1, _enemyMask);

        enemyToDamage.GetComponent<CharacterStat>().EffectController.
            AddEffect(Instantiate(_poisonEffect), enemyToDamage.GetComponent<CharacterStat>());
    }

    protected override void Update()
    {
        base.Update();
    }
}
