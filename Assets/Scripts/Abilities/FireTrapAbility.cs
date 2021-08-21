using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FireTrap")]
public class FireTrapAbility : ActiveAbility
{
    [SerializeField] private GameObject _fireTrap;

    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;
        var trap = Instantiate(_fireTrap, player.gameObject.transform.position, Quaternion.identity);

        Destroy(trap, 3f);
    }

    protected override void Update()
    {
        base.Update();
    }
}
