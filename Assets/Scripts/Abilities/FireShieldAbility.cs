using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FireShield")]
public class FireShieldAbility : ActiveAbility
{
    [SerializeField] private GameObject _fireShield;

    public override void Activate()
    {
        base.Activate();

        var player = PlayerOnScene.Instance;
        var shield = Instantiate(_fireShield, player.gameObject.transform.position, 
                                    Quaternion.identity, player.gameObject.transform);

        Destroy(shield, 5f);
    }

    protected override void Update()
    {
        base.Update();
    }
}
