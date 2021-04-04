using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traits/CantShoot")]
public class CantShoot : Trait,ITraitReqired
{
    public bool ISAppropriate(EquipmentItem equipmentItem)
    {
        if (equipmentItem)
        {
            if (equipmentItem.Echo() == AttackType.Range)
            {
                return false;
            }
            return true;
        }
        return true;
    }

    public override void ApplyTrait()
    {
        CharacterManager.Instance.Stats.EquipmentTraitReq = this;
    }
}
